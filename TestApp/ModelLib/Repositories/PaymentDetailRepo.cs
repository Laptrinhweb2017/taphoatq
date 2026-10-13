namespace ModelLib.Repositories{
	public class PaymentDetailRepo : IRepository<DtoPaymentDetail>
	{
		private static DtoPaymentDetail MapReader(SqlDataReader reader) => new()
		{
			id = reader["id"] as int?,
			PaymentId = reader["PaymentId"] as int?,
			PaymentDate = reader["PaymentDate"] as DateTime?,
			PaymentMethod = reader["PaymentMethod"] as int?,
			Amount = reader["Amount"] as decimal?
		};
		private async Task<List<DtoPaymentDetail>> ExecuteReaderAsync(SqlCommand cmd)
		{
			var result = new List<DtoPaymentDetail>();
			await using var reader = await cmd.ExecuteReaderAsync();
			while (await reader.ReadAsync())
			{
				result.Add(MapReader(reader));
			}
			return result;
		}
		private async Task<DtoResult<DtoPaymentDetail>> GetPaymentDetailAsync(DtoPaymentDetail dto, bool exact = false)
		{
			var result = new DtoResult<DtoPaymentDetail>();
			try
			{
				string condition = RepoBuildHelper<DtoPaymentDetail>.BuildCondition(dto, exact);
				string sql = "SELECT * FROM PaymentDetail" + condition;

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
		public Task<DtoResult<DtoPaymentDetail>> GetAll() => GetPaymentDetailAsync(new DtoPaymentDetail());
		public Task<DtoResult<DtoPaymentDetail>> GetOne(DtoPaymentDetail entity) => GetPaymentDetailAsync(entity, true);
		public Task<DtoResult<DtoPaymentDetail>> Find(DtoPaymentDetail entity) => GetPaymentDetailAsync(entity);

		public async Task<DtoResult<DtoPaymentDetail>> Add(DtoPaymentDetail entity)
		{
			var result = new DtoResult<DtoPaymentDetail>();
			try
			{
				await using SqlConnection conn = new(SqlHelper.SqlConn());
				await conn.OpenAsync();
				await using SqlCommand cmd = new("PaymentDetailAdd", conn) { CommandType = CommandType.StoredProcedure };
				cmd.Parameters.AddWithValue("@PaymentId", entity.PaymentId ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@PaymentDate", entity.PaymentDate ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@PaymentMethod", entity.PaymentMethod ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@Amount", entity.Amount ?? (object)DBNull.Value);
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
		public async Task<DtoResult<DtoPaymentDetail>> Update(DtoPaymentDetail entity)
		{
			var result = new DtoResult<DtoPaymentDetail>();
			try
			{
				await using SqlConnection conn = new(SqlHelper.SqlConn());
				await conn.OpenAsync();
				await using SqlCommand cmd = new("PaymentDetailUpdate", conn) { CommandType = CommandType.StoredProcedure };
				cmd.Parameters.AddWithValue("@id", entity.id ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@PaymentId", entity.PaymentId ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@PaymentDate", entity.PaymentDate ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@PaymentMethod", entity.PaymentMethod ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@Amount", entity.Amount ?? (object)DBNull.Value);
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
		public Task<DtoResult<DtoPaymentDetail>> Delete(DtoPaymentDetail entity) => Delete(entity.id);
		public async Task<DtoResult<DtoPaymentDetail>> Delete(object? entityId)
		{
			var result = new DtoResult<DtoPaymentDetail>();
			try
			{
				await using SqlConnection conn = new(SqlHelper.SqlConn());
				await conn.OpenAsync();
				await using SqlCommand cmd = new("PaymentDetailDelete", conn) { CommandType = CommandType.StoredProcedure };
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