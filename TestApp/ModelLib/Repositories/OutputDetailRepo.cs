namespace ModelLib.Repositories{
	public class OutputDetailRepo : IRepository<DtoOutputDetail>
	{
		private static DtoOutputDetail MapReader(SqlDataReader reader) => new()
		{
			id = reader["id"] as int?,
			OutputId = reader["OutputId"] as int?,
			ProductId = reader["ProductId"] as int?,
			Quantity = reader["Quantity"] as int?,
			UnitPrice = reader["UnitPrice"] as decimal?
		};
		private async Task<List<DtoOutputDetail>> ExecuteReaderAsync(SqlCommand cmd)
		{
			var result = new List<DtoOutputDetail>();
			await using var reader = await cmd.ExecuteReaderAsync();
			while (await reader.ReadAsync())
			{
				result.Add(MapReader(reader));
			}
			return result;
		}
		private async Task<DtoResult<DtoOutputDetail>> GetOutputDetailAsync(DtoOutputDetail dto, bool exact = false)
		{
			var result = new DtoResult<DtoOutputDetail>();
			try
			{
				string condition = RepoBuildHelper<DtoOutputDetail>.BuildCondition(dto, exact);
				string sql = "SELECT * FROM OutputDetail" + condition;

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
		public Task<DtoResult<DtoOutputDetail>> GetAll() => GetOutputDetailAsync(new DtoOutputDetail());
		public Task<DtoResult<DtoOutputDetail>> GetOne(DtoOutputDetail entity) => GetOutputDetailAsync(entity, true);
		public Task<DtoResult<DtoOutputDetail>> Find(DtoOutputDetail entity) => GetOutputDetailAsync(entity);

		public async Task<DtoResult<DtoOutputDetail>> Add(DtoOutputDetail entity)
		{
			var result = new DtoResult<DtoOutputDetail>();
			try
			{
				await using SqlConnection conn = new(SqlHelper.SqlConn());
				await conn.OpenAsync();
				await using SqlCommand cmd = new("OutputDetailAdd", conn) { CommandType = CommandType.StoredProcedure };
				cmd.Parameters.AddWithValue("@OutputId", entity.OutputId ?? (object)DBNull.Value);
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
		public async Task<DtoResult<DtoOutputDetail>> Update(DtoOutputDetail entity)
		{
			var result = new DtoResult<DtoOutputDetail>();
			try
			{
				await using SqlConnection conn = new(SqlHelper.SqlConn());
				await conn.OpenAsync();
				await using SqlCommand cmd = new("OutputDetailUpdate", conn) { CommandType = CommandType.StoredProcedure };
				cmd.Parameters.AddWithValue("@id", entity.id ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@OutputId", entity.OutputId ?? (object)DBNull.Value);
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
		public Task<DtoResult<DtoOutputDetail>> Delete(DtoOutputDetail entity) => Delete(entity.id);
		public async Task<DtoResult<DtoOutputDetail>> Delete(object? entityId)
		{
			var result = new DtoResult<DtoOutputDetail>();
			try
			{
				await using SqlConnection conn = new(SqlHelper.SqlConn());
				await conn.OpenAsync();
				await using SqlCommand cmd = new("OutputDetailDelete", conn) { CommandType = CommandType.StoredProcedure };
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