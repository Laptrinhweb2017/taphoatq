namespace ModelLib.Repositories{
	public class WarehousesRepo : IRepository<DtoWarehouses>
	{
		private static DtoWarehouses MapReader(SqlDataReader reader) => new()
		{
			id = reader["id"] as int?,
			WarehouseName = reader["WarehouseName"] as string,
			Address = reader["Address"] as string
		};
		private async Task<List<DtoWarehouses>> ExecuteReaderAsync(SqlCommand cmd)
		{
			var result = new List<DtoWarehouses>();
			await using var reader = await cmd.ExecuteReaderAsync();
			while (await reader.ReadAsync())
			{
				result.Add(MapReader(reader));
			}
			return result;
		}
		private async Task<DtoResult<DtoWarehouses>> GetWarehousesAsync(DtoWarehouses dto, bool exact = false)
		{
			var result = new DtoResult<DtoWarehouses>();
			try
			{
				string condition = RepoBuildHelper<DtoWarehouses>.BuildCondition(dto, exact);
				string sql = "SELECT * FROM Warehouses" + condition;

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
		public Task<DtoResult<DtoWarehouses>> GetAll() => GetWarehousesAsync(new DtoWarehouses());
		public Task<DtoResult<DtoWarehouses>> GetOne(DtoWarehouses entity) => GetWarehousesAsync(entity, true);
		public Task<DtoResult<DtoWarehouses>> Find(DtoWarehouses entity) => GetWarehousesAsync(entity);

		public async Task<DtoResult<DtoWarehouses>> Add(DtoWarehouses entity)
		{
			var result = new DtoResult<DtoWarehouses>();
			try
			{
				await using SqlConnection conn = new(SqlHelper.SqlConn());
				await conn.OpenAsync();
				await using SqlCommand cmd = new("WarehousesAdd", conn) { CommandType = CommandType.StoredProcedure };
				cmd.Parameters.AddWithValue("@WarehouseName", entity.WarehouseName ?? (object)DBNull.Value);
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
		public async Task<DtoResult<DtoWarehouses>> Update(DtoWarehouses entity)
		{
			var result = new DtoResult<DtoWarehouses>();
			try
			{
				await using SqlConnection conn = new(SqlHelper.SqlConn());
				await conn.OpenAsync();
				await using SqlCommand cmd = new("WarehousesUpdate", conn) { CommandType = CommandType.StoredProcedure };
				cmd.Parameters.AddWithValue("@id", entity.id ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@WarehouseName", entity.WarehouseName ?? (object)DBNull.Value);
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
		public Task<DtoResult<DtoWarehouses>> Delete(DtoWarehouses entity) => Delete(entity.id);
		public async Task<DtoResult<DtoWarehouses>> Delete(object? entityId)
		{
			var result = new DtoResult<DtoWarehouses>();
			try
			{
				await using SqlConnection conn = new(SqlHelper.SqlConn());
				await conn.OpenAsync();
				await using SqlCommand cmd = new("WarehousesDelete", conn) { CommandType = CommandType.StoredProcedure };
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