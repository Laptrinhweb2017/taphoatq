namespace ModelLib.Repositories{
	public class PaymentRepo : IRepository<DtoPayment>
	{
		private static DtoPayment MapReader(SqlDataReader reader) => new()
		{
			id = reader["id"] as int?,
			OrderId = reader["OrderId"] as int?,
			TotalAmount = reader["TotalAmount"] as decimal?,
			RemainingDebt = reader["RemainingDebt"] as decimal?
		};
		private async Task<List<DtoPayment>> ExecuteReaderAsync(SqlCommand cmd)
		{
			var result = new List<DtoPayment>();
			await using var reader = await cmd.ExecuteReaderAsync();
			while (await reader.ReadAsync())
			{
				result.Add(MapReader(reader));
			}
			return result;
		}
		private async Task<DtoResult<DtoPayment>> GetPaymentAsync(DtoPayment dto, bool exact = false)
		{
			var result = new DtoResult<DtoPayment>();
			try
			{
				string condition = RepoBuildHelper<DtoPayment>.BuildCondition(dto, exact);
				string sql = "SELECT * FROM Payment" + condition;

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
		public Task<DtoResult<DtoPayment>> GetAll() => GetPaymentAsync(new DtoPayment());
		public Task<DtoResult<DtoPayment>> GetOne(DtoPayment entity) => GetPaymentAsync(entity, true);
		public Task<DtoResult<DtoPayment>> Find(DtoPayment entity) => GetPaymentAsync(entity);

		public async Task<DtoResult<DtoPayment>> Add(DtoPayment entity)
		{
			var result = new DtoResult<DtoPayment>();
			try
			{
				await using SqlConnection conn = new(SqlHelper.SqlConn());
				await conn.OpenAsync();
				await using SqlCommand cmd = new("PaymentAdd", conn) { CommandType = CommandType.StoredProcedure };
				cmd.Parameters.AddWithValue("@OrderId", entity.OrderId ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@TotalAmount", entity.TotalAmount ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@RemainingDebt", entity.RemainingDebt ?? (object)DBNull.Value);
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
		public async Task<DtoResult<DtoPayment>> Update(DtoPayment entity)
		{
			var result = new DtoResult<DtoPayment>();
			try
			{
				await using SqlConnection conn = new(SqlHelper.SqlConn());
				await conn.OpenAsync();
				await using SqlCommand cmd = new("PaymentUpdate", conn) { CommandType = CommandType.StoredProcedure };
				cmd.Parameters.AddWithValue("@id", entity.id ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@OrderId", entity.OrderId ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@TotalAmount", entity.TotalAmount ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@RemainingDebt", entity.RemainingDebt ?? (object)DBNull.Value);
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
		public Task<DtoResult<DtoPayment>> Delete(DtoPayment entity) => Delete(entity.id);
		public async Task<DtoResult<DtoPayment>> Delete(object? entityId)
		{
			var result = new DtoResult<DtoPayment>();
			try
			{
				await using SqlConnection conn = new(SqlHelper.SqlConn());
				await conn.OpenAsync();
				await using SqlCommand cmd = new("PaymentDelete", conn) { CommandType = CommandType.StoredProcedure };
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