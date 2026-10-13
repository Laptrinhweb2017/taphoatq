namespace ModelLib.Repositories{
	public class PaymentMethodRepo : IRepository<DtoPaymentMethod>
	{
		private static DtoPaymentMethod MapReader(SqlDataReader reader) => new()
		{
			id = reader["id"] as int?,
			PayMethod = reader["PayMethod"] as string
		};
		private async Task<List<DtoPaymentMethod>> ExecuteReaderAsync(SqlCommand cmd)
		{
			var result = new List<DtoPaymentMethod>();
			await using var reader = await cmd.ExecuteReaderAsync();
			while (await reader.ReadAsync())
			{
				result.Add(MapReader(reader));
			}
			return result;
		}
		private async Task<DtoResult<DtoPaymentMethod>> GetPaymentMethodAsync(DtoPaymentMethod dto, bool exact = false)
		{
			var result = new DtoResult<DtoPaymentMethod>();
			try
			{
				string condition = RepoBuildHelper<DtoPaymentMethod>.BuildCondition(dto, exact);
				string sql = "SELECT * FROM PaymentMethod" + condition;

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
		public Task<DtoResult<DtoPaymentMethod>> GetAll() => GetPaymentMethodAsync(new DtoPaymentMethod());
		public Task<DtoResult<DtoPaymentMethod>> GetOne(DtoPaymentMethod entity) => GetPaymentMethodAsync(entity, true);
		public Task<DtoResult<DtoPaymentMethod>> Find(DtoPaymentMethod entity) => GetPaymentMethodAsync(entity);

		public async Task<DtoResult<DtoPaymentMethod>> Add(DtoPaymentMethod entity)
		{
			var result = new DtoResult<DtoPaymentMethod>();
			try
			{
				await using SqlConnection conn = new(SqlHelper.SqlConn());
				await conn.OpenAsync();
				await using SqlCommand cmd = new("PaymentMethodAdd", conn) { CommandType = CommandType.StoredProcedure };
				cmd.Parameters.AddWithValue("@PayMethod", entity.PayMethod ?? (object)DBNull.Value);
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
		public async Task<DtoResult<DtoPaymentMethod>> Update(DtoPaymentMethod entity)
		{
			var result = new DtoResult<DtoPaymentMethod>();
			try
			{
				await using SqlConnection conn = new(SqlHelper.SqlConn());
				await conn.OpenAsync();
				await using SqlCommand cmd = new("PaymentMethodUpdate", conn) { CommandType = CommandType.StoredProcedure };
				cmd.Parameters.AddWithValue("@id", entity.id ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@PayMethod", entity.PayMethod ?? (object)DBNull.Value);
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
		public Task<DtoResult<DtoPaymentMethod>> Delete(DtoPaymentMethod entity) => Delete(entity.id);
		public async Task<DtoResult<DtoPaymentMethod>> Delete(object? entityId)
		{
			var result = new DtoResult<DtoPaymentMethod>();
			try
			{
				await using SqlConnection conn = new(SqlHelper.SqlConn());
				await conn.OpenAsync();
				await using SqlCommand cmd = new("PaymentMethodDelete", conn) { CommandType = CommandType.StoredProcedure };
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