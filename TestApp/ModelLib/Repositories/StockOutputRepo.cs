namespace ModelLib.Repositories{
	public class StockOutputRepo : IRepository<DtoStockOutput>
	{
		private static DtoStockOutput MapReader(SqlDataReader reader) => new()
		{
			id = reader["id"] as int?,
			OutputCode = reader["OutputCode"] as string,
			OutputDate = reader["OutputDate"] as DateTime?,
			EmployeeId = reader["EmployeeId"] as int?,
			OrderId = reader["OrderId"] as int?
		};
		private async Task<List<DtoStockOutput>> ExecuteReaderAsync(SqlCommand cmd)
		{
			var result = new List<DtoStockOutput>();
			await using var reader = await cmd.ExecuteReaderAsync();
			while (await reader.ReadAsync())
			{
				result.Add(MapReader(reader));
			}
			return result;
		}
		private async Task<DtoResult<DtoStockOutput>> GetStockOutputAsync(DtoStockOutput dto, bool exact = false)
		{
			var result = new DtoResult<DtoStockOutput>();
			try
			{
				string condition = RepoBuildHelper<DtoStockOutput>.BuildCondition(dto, exact);
				string sql = "SELECT * FROM StockOutput" + condition;

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
		public Task<DtoResult<DtoStockOutput>> GetAll() => GetStockOutputAsync(new DtoStockOutput());
		public Task<DtoResult<DtoStockOutput>> GetOne(DtoStockOutput entity) => GetStockOutputAsync(entity, true);
		public Task<DtoResult<DtoStockOutput>> Find(DtoStockOutput entity) => GetStockOutputAsync(entity);

		public async Task<DtoResult<DtoStockOutput>> Add(DtoStockOutput entity)
		{
			var result = new DtoResult<DtoStockOutput>();
			try
			{
				await using SqlConnection conn = new(SqlHelper.SqlConn());
				await conn.OpenAsync();
				await using SqlCommand cmd = new("StockOutputAdd", conn) { CommandType = CommandType.StoredProcedure };
				cmd.Parameters.AddWithValue("@OutputCode", entity.OutputCode ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@OutputDate", entity.OutputDate ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@EmployeeId", entity.EmployeeId ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@OrderId", entity.OrderId ?? (object)DBNull.Value);
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
		public async Task<DtoResult<DtoStockOutput>> Update(DtoStockOutput entity)
		{
			var result = new DtoResult<DtoStockOutput>();
			try
			{
				await using SqlConnection conn = new(SqlHelper.SqlConn());
				await conn.OpenAsync();
				await using SqlCommand cmd = new("StockOutputUpdate", conn) { CommandType = CommandType.StoredProcedure };
				cmd.Parameters.AddWithValue("@id", entity.id ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@OutputCode", entity.OutputCode ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@OutputDate", entity.OutputDate ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@EmployeeId", entity.EmployeeId ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@OrderId", entity.OrderId ?? (object)DBNull.Value);
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
		public Task<DtoResult<DtoStockOutput>> Delete(DtoStockOutput entity) => Delete(entity.id);
		public async Task<DtoResult<DtoStockOutput>> Delete(object? entityId)
		{
			var result = new DtoResult<DtoStockOutput>();
			try
			{
				await using SqlConnection conn = new(SqlHelper.SqlConn());
				await conn.OpenAsync();
				await using SqlCommand cmd = new("StockOutputDelete", conn) { CommandType = CommandType.StoredProcedure };
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