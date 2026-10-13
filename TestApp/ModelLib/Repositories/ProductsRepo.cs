namespace ModelLib.Repositories{
	public class ProductsRepo : IRepository<DtoProducts>
	{
		private static DtoProducts MapReader(SqlDataReader reader) => new()
		{
			id = reader["id"] as int?,
			ProductCode = reader["ProductCode"] as string,
			ProductName = reader["ProductName"] as string,
			Image = reader["Image"] as string,
			Descript = reader["Descript"] as string,
			CreatedAt = reader["CreatedAt"] as DateTime?,
			BrandID = reader["BrandID"] as int?,
			CateID = reader["CateID"] as int?
		};
		private async Task<List<DtoProducts>> ExecuteReaderAsync(SqlCommand cmd)
		{
			var result = new List<DtoProducts>();
			await using var reader = await cmd.ExecuteReaderAsync();
			while (await reader.ReadAsync())
			{
				result.Add(MapReader(reader));
			}
			return result;
		}
		private async Task<DtoResult<DtoProducts>> GetProductsAsync(DtoProducts dto, bool exact = false)
		{
			var result = new DtoResult<DtoProducts>();
			try
			{
				string condition = RepoBuildHelper<DtoProducts>.BuildCondition(dto, exact);
				string sql = "SELECT * FROM Products" + condition;

				await using SqlConnection conn = new(SqlHelper.SqlConn());
				await conn.OpenAsync();
				await using SqlCommand cmd = new(sql, conn);
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
		public Task<DtoResult<DtoProducts>> GetAll() => GetProductsAsync(new DtoProducts());
		public Task<DtoResult<DtoProducts>> GetOne(DtoProducts entity) => GetProductsAsync(entity, true);
		public Task<DtoResult<DtoProducts>> Find(DtoProducts entity) => GetProductsAsync(entity);

		public async Task<DtoResult<DtoProducts>> Add(DtoProducts entity)
		{
			var result = new DtoResult<DtoProducts>();
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
		public async Task<DtoResult<DtoProducts>> Update(DtoProducts entity)
		{
			var result = new DtoResult<DtoProducts>();
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
		public Task<DtoResult<DtoProducts>> Delete(DtoProducts entity) => Delete(entity.id);
		public async Task<DtoResult<DtoProducts>> Delete(object? entityId)
		{
			var result = new DtoResult<DtoProducts>();
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