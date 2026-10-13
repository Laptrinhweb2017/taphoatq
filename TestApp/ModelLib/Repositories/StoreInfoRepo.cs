namespace ModelLib.Repositories{
	public class StoreInfoRepo : IRepository<DtoStoreInfo>
	{
		private static DtoStoreInfo MapReader(SqlDataReader reader) => new()
		{
			id = reader["id"] as int?,
			StoreName = reader["StoreName"] as string,
			Address = reader["Address"] as string,
			TaxCode = reader["TaxCode"] as string,
			Phone = reader["Phone"] as string
		};
		private async Task<List<DtoStoreInfo>> ExecuteReaderAsync(SqlCommand cmd)
		{
			var result = new List<DtoStoreInfo>();
			await using var reader = await cmd.ExecuteReaderAsync();
			while (await reader.ReadAsync())
			{
				result.Add(MapReader(reader));
			}
			return result;
		}
		private async Task<DtoResult<DtoStoreInfo>> GetStoreInfoAsync(DtoStoreInfo dto, bool exact = false)
		{
			var result = new DtoResult<DtoStoreInfo>();
			try
			{
				string condition = RepoBuildHelper<DtoStoreInfo>.BuildCondition(dto, exact);
				string sql = "SELECT * FROM StoreInfo" + condition;

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
		public Task<DtoResult<DtoStoreInfo>> GetAll() => GetStoreInfoAsync(new DtoStoreInfo());
		public Task<DtoResult<DtoStoreInfo>> GetOne(DtoStoreInfo entity) => GetStoreInfoAsync(entity, true);
		public Task<DtoResult<DtoStoreInfo>> Find(DtoStoreInfo entity) => GetStoreInfoAsync(entity);

		public async Task<DtoResult<DtoStoreInfo>> Add(DtoStoreInfo entity)
		{
			var result = new DtoResult<DtoStoreInfo>();
			try
			{
				await using SqlConnection conn = new(SqlHelper.SqlConn());
				await conn.OpenAsync();
				await using SqlCommand cmd = new("StoreInfoAdd", conn) { CommandType = CommandType.StoredProcedure };
				cmd.Parameters.AddWithValue("@StoreName", entity.StoreName ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@Address", entity.Address ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@TaxCode", entity.TaxCode ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@Phone", entity.Phone ?? (object)DBNull.Value);
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
		public async Task<DtoResult<DtoStoreInfo>> Update(DtoStoreInfo entity)
		{
			var result = new DtoResult<DtoStoreInfo>();
			try
			{
				await using SqlConnection conn = new(SqlHelper.SqlConn());
				await conn.OpenAsync();
				await using SqlCommand cmd = new("StoreInfoUpdate", conn) { CommandType = CommandType.StoredProcedure };
				cmd.Parameters.AddWithValue("@id", entity.id ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@StoreName", entity.StoreName ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@Address", entity.Address ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@TaxCode", entity.TaxCode ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@Phone", entity.Phone ?? (object)DBNull.Value);
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
		public Task<DtoResult<DtoStoreInfo>> Delete(DtoStoreInfo entity) => Delete(entity.id);
		public async Task<DtoResult<DtoStoreInfo>> Delete(object? entityId)
		{
			var result = new DtoResult<DtoStoreInfo>();
			try
			{
				await using SqlConnection conn = new(SqlHelper.SqlConn());
				await conn.OpenAsync();
				await using SqlCommand cmd = new("StoreInfoDelete", conn) { CommandType = CommandType.StoredProcedure };
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