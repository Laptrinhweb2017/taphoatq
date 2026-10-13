namespace ModelLib.Repositories{
	public class OrderDetailRepo : IRepository<DtoOrderDetail>
	{
		private static DtoOrderDetail MapReader(SqlDataReader reader) => new()
		{
			id = reader["id"] as int?,
			OrderId = reader["OrderId"] as int?,
			ProductId = reader["ProductId"] as int?,
			Quantity = reader["Quantity"] as int?,
			UnitPrice = reader["UnitPrice"] as decimal?
		};
		private async Task<List<DtoOrderDetail>> ExecuteReaderAsync(SqlCommand cmd)
		{
			var result = new List<DtoOrderDetail>();
			await using var reader = await cmd.ExecuteReaderAsync();
			while (await reader.ReadAsync())
			{
				result.Add(MapReader(reader));
			}
			return result;
		}
		private async Task<DtoResult<DtoOrderDetail>> GetOrderDetailAsync(DtoOrderDetail dto, bool exact = false)
		{
			var result = new DtoResult<DtoOrderDetail>();
			try
			{
				string condition = RepoBuildHelper<DtoOrderDetail>.BuildCondition(dto, exact);
				string sql = "SELECT * FROM OrderDetail" + condition;

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
		public Task<DtoResult<DtoOrderDetail>> GetAll() => GetOrderDetailAsync(new DtoOrderDetail());
		public Task<DtoResult<DtoOrderDetail>> GetOne(DtoOrderDetail entity) => GetOrderDetailAsync(entity, true);
		public Task<DtoResult<DtoOrderDetail>> Find(DtoOrderDetail entity) => GetOrderDetailAsync(entity);

		public async Task<DtoResult<DtoOrderDetail>> Add(DtoOrderDetail entity)
		{
			var result = new DtoResult<DtoOrderDetail>();
			try
			{
				await using SqlConnection conn = new(SqlHelper.SqlConn());
				await conn.OpenAsync();
				await using SqlCommand cmd = new("OrderDetailAdd", conn) { CommandType = CommandType.StoredProcedure };
				cmd.Parameters.AddWithValue("@OrderId", entity.OrderId ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@ProductId", entity.ProductId ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@Quantity", entity.Quantity ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@UnitPrice", entity.UnitPrice ?? (object)DBNull.Value);
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
		public async Task<DtoResult<DtoOrderDetail>> Update(DtoOrderDetail entity)
		{
			var result = new DtoResult<DtoOrderDetail>();
			try
			{
				await using SqlConnection conn = new(SqlHelper.SqlConn());
				await conn.OpenAsync();
				await using SqlCommand cmd = new("OrderDetailUpdate", conn) { CommandType = CommandType.StoredProcedure };
				cmd.Parameters.AddWithValue("@id", entity.id ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@OrderId", entity.OrderId ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@ProductId", entity.ProductId ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@Quantity", entity.Quantity ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@UnitPrice", entity.UnitPrice ?? (object)DBNull.Value);
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
		public Task<DtoResult<DtoOrderDetail>> Delete(DtoOrderDetail entity) => Delete(entity.id);
		public async Task<DtoResult<DtoOrderDetail>> Delete(object? entityId)
		{
			var result = new DtoResult<DtoOrderDetail>();
			try
			{
				await using SqlConnection conn = new(SqlHelper.SqlConn());
				await conn.OpenAsync();
				await using SqlCommand cmd = new("OrderDetailDelete", conn) { CommandType = CommandType.StoredProcedure };
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