namespace ModelLib.Repositories{
	public class EmployeeRepo : IRepository<DtoEmployee>
	{
		private static DtoEmployee MapReader(SqlDataReader reader) => new()
		{
			id = reader["id"] as int?,
			FullName = reader["FullName"] as string,
			Email = reader["Email"] as string,
			EmployeeCode = reader["EmloyeeCode"] as string
		};
		private async Task<List<DtoEmployee>> ExecuteReaderAsync(SqlCommand cmd)
		{
			var result = new List<DtoEmployee>();
			await using var reader = await cmd.ExecuteReaderAsync();
			while (await reader.ReadAsync())
			{
				result.Add(MapReader(reader));
			}
			return result;
		}
		private async Task<DtoResult<DtoEmployee>> GetEmployeeAsync(DtoEmployee dto, bool exact = false)
		{
			var result = new DtoResult<DtoEmployee>();
			try
			{
				string condition = RepoBuildHelper<DtoEmployee>.BuildCondition(dto, exact);
				string sql = "SELECT * FROM Employee" + condition;

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
		public Task<DtoResult<DtoEmployee>> GetAll() => GetEmployeeAsync(new DtoEmployee());
		public Task<DtoResult<DtoEmployee>> GetOne(DtoEmployee entity) => GetEmployeeAsync(entity, true);
		public Task<DtoResult<DtoEmployee>> Find(DtoEmployee entity) => GetEmployeeAsync(entity);

		public async Task<DtoResult<DtoEmployee>> Add(DtoEmployee entity)
		{
			var result = new DtoResult<DtoEmployee>();
			try
			{
				await using SqlConnection conn = new(SqlHelper.SqlConn());
				await conn.OpenAsync();
				await using SqlCommand cmd = new("EmployeeAdd", conn) { CommandType = CommandType.StoredProcedure };
				cmd.Parameters.AddWithValue("@FullName", entity.FullName ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@Email", entity.Email ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@EmloyeeCode", entity.EmployeeCode ?? (object)DBNull.Value);
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
		public async Task<DtoResult<DtoEmployee>> Update(DtoEmployee entity)
		{
			var result = new DtoResult<DtoEmployee>();
			try
			{
				await using SqlConnection conn = new(SqlHelper.SqlConn());
				await conn.OpenAsync();
				await using SqlCommand cmd = new("EmployeeUpdate", conn) { CommandType = CommandType.StoredProcedure };
				cmd.Parameters.AddWithValue("@id", entity.id ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@FullName", entity.FullName ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@Email", entity.Email ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@EmloyeeCode", entity.EmployeeCode ?? (object)DBNull.Value);
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
		public Task<DtoResult<DtoEmployee>> Delete(DtoEmployee entity) => Delete(entity.id);
		public async Task<DtoResult<DtoEmployee>> Delete(object? entityId)
		{
			var result = new DtoResult<DtoEmployee>();
			try
			{
				await using SqlConnection conn = new(SqlHelper.SqlConn());
				await conn.OpenAsync();
				await using SqlCommand cmd = new("EmployeeDelete", conn) { CommandType = CommandType.StoredProcedure };
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