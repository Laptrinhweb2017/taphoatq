namespace ModelLib.Repositories{
	public class POTrackingRepo : IRepository<DtoPOTracking>
	{
		private static DtoPOTracking MapReader(SqlDataReader reader) => new()
		{
			id = reader["id"] as int?,
			OrderId = reader["OrderId"] as int?,
			StatusId = reader["StatusId"] as int?,
			TrackedAt = reader["TrackedAt"] as DateTime?
		};
		private async Task<List<DtoPOTracking>> ExecuteReaderAsync(SqlCommand cmd)
		{
			var result = new List<DtoPOTracking>();
			await using var reader = await cmd.ExecuteReaderAsync();
			while (await reader.ReadAsync())
			{
				result.Add(MapReader(reader));
			}
			return result;
		}
		private async Task<DtoResult<DtoPOTracking>> GetPOTrackingAsync(DtoPOTracking dto, bool exact = false)
		{
			var result = new DtoResult<DtoPOTracking>();
			try
			{
				string condition = RepoBuildHelper<DtoPOTracking>.BuildCondition(dto, exact);
				string sql = "SELECT * FROM POTracking" + condition;

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
		public Task<DtoResult<DtoPOTracking>> GetAll() => GetPOTrackingAsync(new DtoPOTracking());
		public Task<DtoResult<DtoPOTracking>> GetOne(DtoPOTracking entity) => GetPOTrackingAsync(entity, true);
		public Task<DtoResult<DtoPOTracking>> Find(DtoPOTracking entity) => GetPOTrackingAsync(entity);

		public async Task<DtoResult<DtoPOTracking>> Add(DtoPOTracking entity)
		{
			var result = new DtoResult<DtoPOTracking>();
			try
			{
				await using SqlConnection conn = new(SqlHelper.SqlConn());
				await conn.OpenAsync();
				await using SqlCommand cmd = new("POTrackingAdd", conn) { CommandType = CommandType.StoredProcedure };
				cmd.Parameters.AddWithValue("@OrderId", entity.OrderId ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@StatusId", entity.StatusId ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@TrackedAt", entity.TrackedAt ?? (object)DBNull.Value);
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
		public async Task<DtoResult<DtoPOTracking>> Update(DtoPOTracking entity)
		{
			var result = new DtoResult<DtoPOTracking>();
			try
			{
				await using SqlConnection conn = new(SqlHelper.SqlConn());
				await conn.OpenAsync();
				await using SqlCommand cmd = new("POTrackingUpdate", conn) { CommandType = CommandType.StoredProcedure };
				cmd.Parameters.AddWithValue("@id", entity.id ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@OrderId", entity.OrderId ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@StatusId", entity.StatusId ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@TrackedAt", entity.TrackedAt ?? (object)DBNull.Value);
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
		public Task<DtoResult<DtoPOTracking>> Delete(DtoPOTracking entity) => Delete(entity.id);
		public async Task<DtoResult<DtoPOTracking>> Delete(object? entityId)
		{
			var result = new DtoResult<DtoPOTracking>();
			try
			{
				await using SqlConnection conn = new(SqlHelper.SqlConn());
				await conn.OpenAsync();
				await using SqlCommand cmd = new("POTrackingDelete", conn) { CommandType = CommandType.StoredProcedure };
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