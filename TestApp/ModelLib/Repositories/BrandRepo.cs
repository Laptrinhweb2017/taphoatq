namespace ModelLib.Repositories{
	public class BrandRepo : IRepository<DtoBrand>
	{
		private static DtoBrand MapReader(SqlDataReader reader) => new()
		{
			id = reader["id"] as int?,
			BrandName = reader["BrandName"] as string
		};
		private async Task<List<DtoBrand>> ExecuteReaderAsync(SqlCommand cmd)
		{
			var result = new List<DtoBrand>();
			await using var reader = await cmd.ExecuteReaderAsync();
			while (await reader.ReadAsync())
			{
				result.Add(MapReader(reader));
			}
			return result;
		}
		private async Task<DtoResult<DtoBrand>> GetBrandAsync(DtoBrand dto, bool exact = false)
		{
			var result = new DtoResult<DtoBrand>();
			try
			{
				string condition = RepoBuildHelper<DtoBrand>.BuildCondition(dto, exact);
				string sql = "SELECT * FROM Brand" + condition;

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
		public Task<DtoResult<DtoBrand>> GetAll() => GetBrandAsync(new DtoBrand());
		public Task<DtoResult<DtoBrand>> GetOne(DtoBrand entity) => GetBrandAsync(entity, true);
		public Task<DtoResult<DtoBrand>> Find(DtoBrand entity) => GetBrandAsync(entity);

		public async Task<DtoResult<DtoBrand>> Add(DtoBrand entity)
		{
			var result = new DtoResult<DtoBrand>();
			try
			{
				await using SqlConnection conn = new(SqlHelper.SqlConn());
				await conn.OpenAsync();
				await using SqlCommand cmd = new("BrandAdd", conn) { CommandType = CommandType.StoredProcedure };
				cmd.Parameters.AddWithValue("@BrandName", entity.BrandName ?? (object)DBNull.Value);
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
		public async Task<DtoResult<DtoBrand>> Update(DtoBrand entity)
		{
			var result = new DtoResult<DtoBrand>();
			try
			{
				await using SqlConnection conn = new(SqlHelper.SqlConn());
				await conn.OpenAsync();
				await using SqlCommand cmd = new("BrandUpdate", conn) { CommandType = CommandType.StoredProcedure };
				cmd.Parameters.AddWithValue("@id", entity.id ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@BrandName", entity.BrandName ?? (object)DBNull.Value);
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
		public Task<DtoResult<DtoBrand>> Delete(DtoBrand entity) => Delete(entity.id);
		public async Task<DtoResult<DtoBrand>> Delete(object? entityId)
		{
			var result = new DtoResult<DtoBrand>();
			try
			{
				await using SqlConnection conn = new(SqlHelper.SqlConn());
				await conn.OpenAsync();
				await using SqlCommand cmd = new("BrandDelete", conn) { CommandType = CommandType.StoredProcedure };
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