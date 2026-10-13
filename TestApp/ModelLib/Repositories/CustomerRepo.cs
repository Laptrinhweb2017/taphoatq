namespace ModelLib.Repositories{
	public class CustomerRepo : IRepository<DtoCustomer>
	{
		private static DtoCustomer MapReader(SqlDataReader reader) => new()
		{
			id = reader["id"] as int?,
			CustomerCode = reader["CustomerCode"] as string,
			CustomerName = reader["CustomerName"] as string,
			TaxCode = reader["TaxCode"] as string,
			Phone = reader["Phone"] as string,
			Address = reader["Address"] as string
		};
		private async Task<List<DtoCustomer>> ExecuteReaderAsync(SqlCommand cmd)
		{
			var result = new List<DtoCustomer>();
			await using var reader = await cmd.ExecuteReaderAsync();
			while (await reader.ReadAsync())
			{
				result.Add(MapReader(reader));
			}
			return result;
		}
		private async Task<DtoResult<DtoCustomer>> GetCustomerAsync(DtoCustomer dto, bool exact = false)
		{
			var result = new DtoResult<DtoCustomer>();
			try
			{
				string condition = RepoBuildHelper<DtoCustomer>.BuildCondition(dto, exact);
				string sql = "SELECT * FROM Customer" + condition;

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
		public Task<DtoResult<DtoCustomer>> GetAll() => GetCustomerAsync(new DtoCustomer());
		public Task<DtoResult<DtoCustomer>> GetOne(DtoCustomer entity) => GetCustomerAsync(entity, true);
		public Task<DtoResult<DtoCustomer>> Find(DtoCustomer entity) => GetCustomerAsync(entity);

		public async Task<DtoResult<DtoCustomer>> Add(DtoCustomer entity)
		{
			var result = new DtoResult<DtoCustomer>() { Succeed=true};
			try
			{
				await using SqlConnection conn = new(SqlHelper.SqlConn());
				await conn.OpenAsync();
				await using SqlCommand cmd = new("CustomerAdd", conn) { CommandType = CommandType.StoredProcedure };
				cmd.Parameters.AddWithValue("@CustomerCode", entity.CustomerCode ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@CustomerName", entity.CustomerName ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@TaxCode", entity.TaxCode ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@Phone", entity.Phone ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@Address", entity.Address ?? (object)DBNull.Value);
				var data = await ExecuteReaderAsync(cmd);
				result.Result = data.FirstOrDefault();
				result.Message = result.Result != null ? "OK" : "Không thêm được dữ liệu.";
			}
			catch (Exception ex)
			{
				result.Message = ex.Message;
				result.Succeed = false;
			}
			return result;
		}
		public async Task<DtoResult<DtoCustomer>> Update(DtoCustomer entity)
		{
			var result = new DtoResult<DtoCustomer>() { Succeed = true};
			try
			{
				await using SqlConnection conn = new(SqlHelper.SqlConn());
				await conn.OpenAsync();
				await using SqlCommand cmd = new("CustomerUpdate", conn) { CommandType = CommandType.StoredProcedure };
				cmd.Parameters.AddWithValue("@id", entity.id ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@CustomerCode", entity.CustomerCode ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@CustomerName", entity.CustomerName ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@TaxCode", entity.TaxCode ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@Phone", entity.Phone ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@Address", entity.Address ?? (object)DBNull.Value);
				var data = await ExecuteReaderAsync(cmd);
				result.Result = data.FirstOrDefault();
				result.Message = result.Result != null ? "OK" : "Không cập nhật được dữ liệu.";
			}
			catch (Exception ex)
			{
				result.Message = ex.Message;
				result.Succeed = false;
			}
			return result;
		}
		public Task<DtoResult<DtoCustomer>> Delete(DtoCustomer entity) => Delete(entity.id);
		public async Task<DtoResult<DtoCustomer>> Delete(object? entityId)
		{
			var result = new DtoResult<DtoCustomer>();
			try
			{
				await using SqlConnection conn = new(SqlHelper.SqlConn());
				await conn.OpenAsync();
				await using SqlCommand cmd = new("CustomerDelete", conn) { CommandType = CommandType.StoredProcedure };
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