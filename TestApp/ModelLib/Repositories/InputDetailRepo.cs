namespace ModelLib.Repositories{
	public class InputDetailRepo : IRepository<DtoInputDetail>
	{
		private static DtoInputDetail MapReader(SqlDataReader reader) => new()
		{
			id = reader["id"] as int?,
			InputId = reader["InputId"] as int?,
			ProductId = reader["ProductId"] as int?,
			Quantity = reader["Quantity"] as int?,
			UnitPrice = reader["UnitPrice"] as decimal?
		};
		private async Task<List<DtoInputDetail>> ExecuteReaderAsync(SqlCommand cmd)
		{
			var result = new List<DtoInputDetail>();
			await using var reader = await cmd.ExecuteReaderAsync();
			while (await reader.ReadAsync())
			{
				result.Add(MapReader(reader));
			}
			return result;
		}
		private async Task<DtoResult<DtoInputDetail>> GetInputDetailAsync(DtoInputDetail dto, bool exact = false)
		{
			var result = new DtoResult<DtoInputDetail>();
			try
			{
				string condition = RepoBuildHelper<DtoInputDetail>.BuildCondition(dto, exact);
				string sql = "SELECT * FROM InputDetail" + condition;

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
		public Task<DtoResult<DtoInputDetail>> GetAll() => GetInputDetailAsync(new DtoInputDetail());
		public Task<DtoResult<DtoInputDetail>> GetOne(DtoInputDetail entity) => GetInputDetailAsync(entity, true);
		public Task<DtoResult<DtoInputDetail>> Find(DtoInputDetail entity) => GetInputDetailAsync(entity);

		public async Task<DtoResult<DtoInputDetail>> Add(DtoInputDetail entity)
		{
			var result = new DtoResult<DtoInputDetail>();
			try
			{
				await using SqlConnection conn = new(SqlHelper.SqlConn());
				await conn.OpenAsync();
				await using SqlCommand cmd = new("InputDetailAdd", conn) { CommandType = CommandType.StoredProcedure };
				cmd.Parameters.AddWithValue("@InputId", entity.InputId ?? (object)DBNull.Value);
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
		public async Task<DtoResult<DtoInputDetail>> Update(DtoInputDetail entity)
		{
			var result = new DtoResult<DtoInputDetail>();
			try
			{
				await using SqlConnection conn = new(SqlHelper.SqlConn());
				await conn.OpenAsync();
				await using SqlCommand cmd = new("InputDetailUpdate", conn) { CommandType = CommandType.StoredProcedure };
				cmd.Parameters.AddWithValue("@id", entity.id ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@InputId", entity.InputId ?? (object)DBNull.Value);
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
		public Task<DtoResult<DtoInputDetail>> Delete(DtoInputDetail entity) => Delete(entity.id);
		public async Task<DtoResult<DtoInputDetail>> Delete(object? entityId)
		{
			var result = new DtoResult<DtoInputDetail>();
			try
			{
				await using SqlConnection conn = new(SqlHelper.SqlConn());
				await conn.OpenAsync();
				await using SqlCommand cmd = new("InputDetailDelete", conn) { CommandType = CommandType.StoredProcedure };
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