namespace ModelLib.Repositories{
	public class OrdersRepo : IRepository<DtoOrders>
	{
		private static DtoOrders MapReader(SqlDataReader reader) => new()
		{
			id = reader["id"] as int?,
			OrderCode = reader["OrderCode"] as string,
			CustomerId = reader["CustomerId"] as int?,
			OrderDate = reader["OrderDate"] as DateTime?,
			Status = reader["Status"] as int?
		};
		private async Task<List<DtoOrders>> ExecuteReaderAsync(SqlCommand cmd)
		{
			var result = new List<DtoOrders>();
			await using var reader = await cmd.ExecuteReaderAsync();
			while (await reader.ReadAsync())
			{
				result.Add(MapReader(reader));
			}
			return result;
		}
		private async Task<DtoResult<DtoOrders>> GetOrdersAsync(DtoOrders dto, bool exact = false)
		{
			var result = new DtoResult<DtoOrders>();
			try
			{
				string condition = RepoBuildHelper<DtoOrders>.BuildCondition(dto, exact);
				string sql = "SELECT * FROM Orders" + condition;

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
		public Task<DtoResult<DtoOrders>> GetAll() => GetOrdersAsync(new DtoOrders());
		public Task<DtoResult<DtoOrders>> GetOne(DtoOrders entity) => GetOrdersAsync(entity, true);
		public Task<DtoResult<DtoOrders>> Find(DtoOrders entity) => GetOrdersAsync(entity);

		public async Task<DtoResult<DtoOrders>> Add(DtoOrders entity)
		{
			var result = new DtoResult<DtoOrders>();
			try
			{
				await using SqlConnection conn = new(SqlHelper.SqlConn());
				await conn.OpenAsync();
				await using SqlCommand cmd = new("OrdersAdd", conn) { CommandType = CommandType.StoredProcedure };
				cmd.Parameters.AddWithValue("@OrderCode", entity.OrderCode ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@CustomerId", entity.CustomerId ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@OrderDate", entity.OrderDate ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@Status", entity.Status ?? (object)DBNull.Value);
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
		public async Task<DtoResult<DtoOrders>> Update(DtoOrders entity)
		{
			var result = new DtoResult<DtoOrders>();
			try
			{
				await using SqlConnection conn = new(SqlHelper.SqlConn());
				await conn.OpenAsync();
				await using SqlCommand cmd = new("OrdersUpdate", conn) { CommandType = CommandType.StoredProcedure };
				cmd.Parameters.AddWithValue("@id", entity.id ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@OrderCode", entity.OrderCode ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@CustomerId", entity.CustomerId ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@OrderDate", entity.OrderDate ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@Status", entity.Status ?? (object)DBNull.Value);
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
		public Task<DtoResult<DtoOrders>> Delete(DtoOrders entity) => Delete(entity.id);
		public async Task<DtoResult<DtoOrders>> Delete(object? entityId)
		{
			var result = new DtoResult<DtoOrders>();
			try
			{
				await using SqlConnection conn = new(SqlHelper.SqlConn());
				await conn.OpenAsync();
				await using SqlCommand cmd = new("OrdersDelete", conn) { CommandType = CommandType.StoredProcedure };
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