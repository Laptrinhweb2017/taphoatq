namespace ModelLib.Repositories{
	public class OrderStatusRepo : IRepository<DtoOrderStatus>
	{
		private static DtoOrderStatus MapReader(SqlDataReader reader) => new()
		{
			id = reader["id"] as int?,
			Descript = reader["Descript"] as string
		};
		private async Task<List<DtoOrderStatus>> ExecuteReaderAsync(SqlCommand cmd)
		{
			var result = new List<DtoOrderStatus>();
			await using var reader = await cmd.ExecuteReaderAsync();
			while (await reader.ReadAsync())
			{
				result.Add(MapReader(reader));
			}
			return result;
		}
		private async Task<DtoResult<DtoOrderStatus>> GetOrderStatusAsync(DtoOrderStatus dto, bool exact = false)
		{
			var result = new DtoResult<DtoOrderStatus>();
			try
			{
				string condition = RepoBuildHelper<DtoOrderStatus>.BuildCondition(dto, exact);
				string sql = "SELECT * FROM OrderStatus" + condition;

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
		public Task<DtoResult<DtoOrderStatus>> GetAll() => GetOrderStatusAsync(new DtoOrderStatus());
		public Task<DtoResult<DtoOrderStatus>> GetOne(DtoOrderStatus entity) => GetOrderStatusAsync(entity, true);
		public Task<DtoResult<DtoOrderStatus>> Find(DtoOrderStatus entity) => GetOrderStatusAsync(entity);

		public async Task<DtoResult<DtoOrderStatus>> Add(DtoOrderStatus entity)
		{
			var result = new DtoResult<DtoOrderStatus>();
			try
			{
				await using SqlConnection conn = new(SqlHelper.SqlConn());
				await conn.OpenAsync();
				await using SqlCommand cmd = new("OrderStatusAdd", conn) { CommandType = CommandType.StoredProcedure };
				cmd.Parameters.AddWithValue("@Descript", entity.Descript ?? (object)DBNull.Value);
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
		public async Task<DtoResult<DtoOrderStatus>> Update(DtoOrderStatus entity)
		{
			var result = new DtoResult<DtoOrderStatus>();
			try
			{
				await using SqlConnection conn = new(SqlHelper.SqlConn());
				await conn.OpenAsync();
				await using SqlCommand cmd = new("OrderStatusUpdate", conn) { CommandType = CommandType.StoredProcedure };
				cmd.Parameters.AddWithValue("@id", entity.id ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@Descript", entity.Descript ?? (object)DBNull.Value);
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
		public Task<DtoResult<DtoOrderStatus>> Delete(DtoOrderStatus entity) => Delete(entity.id);
		public async Task<DtoResult<DtoOrderStatus>> Delete(object? entityId)
		{
			var result = new DtoResult<DtoOrderStatus>();
			try
			{
				await using SqlConnection conn = new(SqlHelper.SqlConn());
				await conn.OpenAsync();
				await using SqlCommand cmd = new("OrderStatusDelete", conn) { CommandType = CommandType.StoredProcedure };
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