namespace ModelLib.Repositories{
	public class LoginInfoRepo : IRepository<DtoLoginInfo>
	{
		private static DtoLoginInfo MapReader(SqlDataReader reader) => new()
		{
			id = reader["id"] as int?,
			UserID = reader["UserID"] as string,
			PassWord = reader["PassWord"] as string
		};
		private async Task<List<DtoLoginInfo>> ExecuteReaderAsync(SqlCommand cmd)
		{
			var result = new List<DtoLoginInfo>();
			await using var reader = await cmd.ExecuteReaderAsync();
			while (await reader.ReadAsync())
			{
				result.Add(MapReader(reader));
			}
			return result;
		}
		private async Task<DtoResult<DtoLoginInfo>> GetLoginInfoAsync(DtoLoginInfo dto, bool exact = false)
		{
			var result = new DtoResult<DtoLoginInfo>();
			try
			{
				string condition = RepoBuildHelper<DtoLoginInfo>.BuildCondition(dto, exact);
				string sql = "SELECT * FROM LoginInfo" + condition;

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
		public Task<DtoResult<DtoLoginInfo>> GetAll() => GetLoginInfoAsync(new DtoLoginInfo());
		public Task<DtoResult<DtoLoginInfo>> GetOne(DtoLoginInfo entity) => GetLoginInfoAsync(entity, true);
		public Task<DtoResult<DtoLoginInfo>> Find(DtoLoginInfo entity) => GetLoginInfoAsync(entity);

		public async Task<DtoResult<DtoLoginInfo>> Add(DtoLoginInfo entity)
		{
			var result = new DtoResult<DtoLoginInfo>();
			try
			{
				await using SqlConnection conn = new(SqlHelper.SqlConn());
				await conn.OpenAsync();
				await using SqlCommand cmd = new("LoginInfoAdd", conn) { CommandType = CommandType.StoredProcedure };
				cmd.Parameters.AddWithValue("@UserID", entity.UserID ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@PassWord", entity.PassWord ?? (object)DBNull.Value);
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
		public async Task<DtoResult<DtoLoginInfo>> Update(DtoLoginInfo entity)
		{
			var result = new DtoResult<DtoLoginInfo>();
			try
			{
				await using SqlConnection conn = new(SqlHelper.SqlConn());
				await conn.OpenAsync();
				await using SqlCommand cmd = new("LoginInfoUpdate", conn) { CommandType = CommandType.StoredProcedure };
				cmd.Parameters.AddWithValue("@id", entity.id ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@UserID", entity.UserID ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@PassWord", entity.PassWord ?? (object)DBNull.Value);
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
		public Task<DtoResult<DtoLoginInfo>> Delete(DtoLoginInfo entity) => Delete(entity.id);
		public async Task<DtoResult<DtoLoginInfo>> Delete(object? entityId)
		{
			var result = new DtoResult<DtoLoginInfo>();
			try
			{
				await using SqlConnection conn = new(SqlHelper.SqlConn());
				await conn.OpenAsync();
				await using SqlCommand cmd = new("LoginInfoDelete", conn) { CommandType = CommandType.StoredProcedure };
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