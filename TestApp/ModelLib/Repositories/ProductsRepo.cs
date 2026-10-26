using Application.Asset.FullDto;

namespace ModelLib.Repositories{
	public class ProductsRepo : IRepository<FullProduct>
	{
		private static FullProduct MapReader(SqlDataReader reader) => new()
		{
			id = reader["id"] as int?,
			ProductCode = reader["ProductCode"] as string,
			ProductName = reader["ProductName"] as string,
			Image = reader["Image"] as string,
			Descript = reader["Descript"] as string,
			CreatedAt = reader["CreatedAt"] as DateTime?,
			BrandID = reader["BrandID"] as int?,
			CateID = reader["CateID"] as int?,
			BrandName =  reader["BrandName"] as string,
			CateName = reader["CateName"] as string,
		};
		private async Task<List<FullProduct>> ExecuteReaderAsync(SqlCommand cmd)
		{
			var result = new List<FullProduct>();
			await using var reader = await cmd.ExecuteReaderAsync();
			while (await reader.ReadAsync())
			{
				result.Add(MapReader(reader));
			}
			return result;
		}
		private async Task<DtoResult<FullProduct>> GetProductsAsync(FullProduct dto, bool exact = false)
		{
			var result = new DtoResult<FullProduct>();
			try
			{
				string sql = "GetFullProduct"; //gọi về sp

				await using SqlConnection conn = new(SqlHelper.SqlConn());
				await conn.OpenAsync();
				await using SqlCommand cmd = new(sql, conn);
				cmd.CommandType = CommandType.StoredProcedure;
				var data = await ExecuteReaderAsync(cmd);

				result.Message = "OK";
				if (exact)
					result.Result = data.FirstOrDefault();
				else
					result.ResultList = data;
			}
			catch (Exception ex)
			{
				result.Message = ex.Message;
			}
			return result;
		}
		public Task<DtoResult<FullProduct>> GetAll() => GetProductsAsync(new FullProduct());
		public Task<DtoResult<FullProduct>> GetOne(FullProduct entity) => GetProductsAsync(entity, true);
		public Task<DtoResult<FullProduct>> Find(FullProduct entity) => GetProductsAsync(entity);

		public async Task<DtoResult<FullProduct>> Add(FullProduct entity)
		{
			var result = new DtoResult<FullProduct>();
			try
			{
				await using SqlConnection conn = new(SqlHelper.SqlConn());
				await conn.OpenAsync();
				await using SqlCommand cmd = new("ProductsAdd", conn) { CommandType = CommandType.StoredProcedure };
				cmd.Parameters.AddWithValue("@ProductCode", entity.ProductCode ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@ProductName", entity.ProductName ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@Image", entity.Image ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@Descript", entity.Descript ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@CreatedAt", entity.CreatedAt ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@BrandID", entity.BrandID ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@CateID", entity.CateID ?? (object)DBNull.Value);
				var data = await ExecuteReaderAsync(cmd);
				result.Result = data.FirstOrDefault();
				result.Message = result.Result != null ? "OK" : "Không thêm được dữ liệu.";
			}
			catch (Exception ex)
			{
				result.Message = ex.Message;
			}
			return result;
		}
		public async Task<DtoResult<FullProduct>> Update(FullProduct entity)
		{
			var result = new DtoResult<FullProduct>();
			try
			{
				await using SqlConnection conn = new(SqlHelper.SqlConn());
				await conn.OpenAsync();
				await using SqlCommand cmd = new("ProductsUpdate", conn) { CommandType = CommandType.StoredProcedure };
				cmd.Parameters.AddWithValue("@id", entity.id ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@ProductCode", entity.ProductCode ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@ProductName", entity.ProductName ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@Image", entity.Image ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@Descript", entity.Descript ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@CreatedAt", entity.CreatedAt ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@BrandID", entity.BrandID ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@CateID", entity.CateID ?? (object)DBNull.Value);
				var data = await ExecuteReaderAsync(cmd);
				result.Result = data.FirstOrDefault();
				result.Message = result.Result != null ? "OK" : "Không cập nhật được dữ liệu.";
			}
			catch (Exception ex)
			{
				result.Message = ex.Message;
			}
			return result;
		}
		public Task<DtoResult<FullProduct>> Delete(FullProduct entity) => Delete(entity.id);
		public async Task<DtoResult<FullProduct>> Delete(object? entityId)
		{
			var result = new DtoResult<FullProduct>();
			try
			{
				await using SqlConnection conn = new(SqlHelper.SqlConn());
				await conn.OpenAsync();
				await using SqlCommand cmd = new("ProductsDelete", conn) { CommandType = CommandType.StoredProcedure };
				cmd.Parameters.AddWithValue("@id", entityId ?? (object)DBNull.Value);
				int count = await cmd.ExecuteNonQueryAsync();
				result.Message = count > 0 ? "OK" : "Không xóa được dữ liệu";
			}
			catch (Exception ex)
			{
				result.Message = ex.Message;
			}
			return result;
		}
	}
}