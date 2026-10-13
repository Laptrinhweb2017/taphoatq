namespace ModelLib.Repositories{
	public class TrackingStatusRepo : IRepository<DtoTrackingStatus>
	{
		private static DtoTrackingStatus MapReader(SqlDataReader reader) => new()
		{
			id = reader["id"] as int?,
			StatusName = reader["StatusName"] as string
		};
		private async Task<List<DtoTrackingStatus>> ExecuteReaderAsync(SqlCommand cmd)
		{
			var result = new List<DtoTrackingStatus>();
			await using var reader = await cmd.ExecuteReaderAsync();
			while (await reader.ReadAsync())
			{
				result.Add(MapReader(reader));
			}
			return result;
		}
		private async Task<DtoResult<DtoTrackingStatus>> GetTrackingStatusAsync(DtoTrackingStatus dto, bool exact = false)
		{
			var result = new DtoResult<DtoTrackingStatus>();
			try
			{
				string condition = RepoBuildHelper<DtoTrackingStatus>.BuildCondition(dto, exact);
				string sql = "SELECT * FROM TrackingStatus" + condition;

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
		public Task<DtoResult<DtoTrackingStatus>> GetAll() => GetTrackingStatusAsync(new DtoTrackingStatus());
		public Task<DtoResult<DtoTrackingStatus>> GetOne(DtoTrackingStatus entity) => GetTrackingStatusAsync(entity, true);
		public Task<DtoResult<DtoTrackingStatus>> Find(DtoTrackingStatus entity) => GetTrackingStatusAsync(entity);

		public async Task<DtoResult<DtoTrackingStatus>> Add(DtoTrackingStatus entity)
		{
			var result = new DtoResult<DtoTrackingStatus>();
			try
			{
				await using SqlConnection conn = new(SqlHelper.SqlConn());
				await conn.OpenAsync();
				await using SqlCommand cmd = new("TrackingStatusAdd", conn) { CommandType = CommandType.StoredProcedure };
				cmd.Parameters.AddWithValue("@StatusName", entity.StatusName ?? (object)DBNull.Value);
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
		public async Task<DtoResult<DtoTrackingStatus>> Update(DtoTrackingStatus entity)
		{
			var result = new DtoResult<DtoTrackingStatus>();
			try
			{
				await using SqlConnection conn = new(SqlHelper.SqlConn());
				await conn.OpenAsync();
				await using SqlCommand cmd = new("TrackingStatusUpdate", conn) { CommandType = CommandType.StoredProcedure };
				cmd.Parameters.AddWithValue("@id", entity.id ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@StatusName", entity.StatusName ?? (object)DBNull.Value);
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
		public Task<DtoResult<DtoTrackingStatus>> Delete(DtoTrackingStatus entity) => Delete(entity.id);
		public async Task<DtoResult<DtoTrackingStatus>> Delete(object? entityId)
		{
			var result = new DtoResult<DtoTrackingStatus>();
			try
			{
				await using SqlConnection conn = new(SqlHelper.SqlConn());
				await conn.OpenAsync();
				await using SqlCommand cmd = new("TrackingStatusDelete", conn) { CommandType = CommandType.StoredProcedure };
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