namespace ModelLib.Repositories{
	public class SupplierRepo : IRepository<DtoSupplier>
	{
		private static DtoSupplier MapReader(SqlDataReader reader) => new()
		{
			id = reader["id"] as int?,
			SupplierCode = reader["SupplierCode"] as string,
			SupplierName = reader["SupplierName"] as string,
			Phone = reader["Phone"] as string,
			Address = reader["Address"] as string
		};
		private async Task<List<DtoSupplier>> ExecuteReaderAsync(SqlCommand cmd)
		{
			var result = new List<DtoSupplier>();
			await using var reader = await cmd.ExecuteReaderAsync();
			while (await reader.ReadAsync())
			{
				result.Add(MapReader(reader));
			}
			return result;
		}
		private async Task<DtoResult<DtoSupplier>> GetSupplierAsync(DtoSupplier dto, bool exact = false)
		{
			var result = new DtoResult<DtoSupplier>();
			try
			{
				string condition = RepoBuildHelper<DtoSupplier>.BuildCondition(dto, exact);
				string sql = "SELECT * FROM Supplier" + condition;

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
		public Task<DtoResult<DtoSupplier>> GetAll() => GetSupplierAsync(new DtoSupplier());
		public Task<DtoResult<DtoSupplier>> GetOne(DtoSupplier entity) => GetSupplierAsync(entity, true);
		public Task<DtoResult<DtoSupplier>> Find(DtoSupplier entity) => GetSupplierAsync(entity);

		public async Task<DtoResult<DtoSupplier>> Add(DtoSupplier entity)
		{
			var result = new DtoResult<DtoSupplier>();
			try
			{
				await using SqlConnection conn = new(SqlHelper.SqlConn());
				await conn.OpenAsync();
				await using SqlCommand cmd = new("SupplierAdd", conn) { CommandType = CommandType.StoredProcedure };
				cmd.Parameters.AddWithValue("@SupplierCode", entity.SupplierCode ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@SupplierName", entity.SupplierName ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@Phone", entity.Phone ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@Address", entity.Address ?? (object)DBNull.Value);
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
		public async Task<DtoResult<DtoSupplier>> Update(DtoSupplier entity)
		{
			var result = new DtoResult<DtoSupplier>();
			try
			{
				await using SqlConnection conn = new(SqlHelper.SqlConn());
				await conn.OpenAsync();
				await using SqlCommand cmd = new("SupplierUpdate", conn) { CommandType = CommandType.StoredProcedure };
				cmd.Parameters.AddWithValue("@id", entity.id ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@SupplierCode", entity.SupplierCode ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@SupplierName", entity.SupplierName ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@Phone", entity.Phone ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@Address", entity.Address ?? (object)DBNull.Value);
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
		public Task<DtoResult<DtoSupplier>> Delete(DtoSupplier entity) => Delete(entity.id);
		public async Task<DtoResult<DtoSupplier>> Delete(object? entityId)
		{
			var result = new DtoResult<DtoSupplier>();
			try
			{
				await using SqlConnection conn = new(SqlHelper.SqlConn());
				await conn.OpenAsync();
				await using SqlCommand cmd = new("SupplierDelete", conn) { CommandType = CommandType.StoredProcedure };
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