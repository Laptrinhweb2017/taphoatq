namespace ModelLib.Repositories{
	public class StockInputRepo : IRepository<DtoStockInput>
	{
		private static DtoStockInput MapReader(SqlDataReader reader) => new()
		{
			id = reader["id"] as int?,
			ReceiptCode = reader["ReceiptCode"] as string,
			ReceivedDate = reader["ReceivedDate"] as DateTime?,
			SupplierId = reader["SupplierId"] as int?,
			EmployeeId = reader["EmployeeId"] as int?
		};
		private async Task<List<DtoStockInput>> ExecuteReaderAsync(SqlCommand cmd)
		{
			var result = new List<DtoStockInput>();
			await using var reader = await cmd.ExecuteReaderAsync();
			while (await reader.ReadAsync())
			{
				result.Add(MapReader(reader));
			}
			return result;
		}
		private async Task<DtoResult<DtoStockInput>> GetStockInputAsync(DtoStockInput dto, bool exact = false)
		{
			var result = new DtoResult<DtoStockInput>();
			try
			{
				string condition = RepoBuildHelper<DtoStockInput>.BuildCondition(dto, exact);
				string sql = "SELECT * FROM StockInput" + condition;

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
		public Task<DtoResult<DtoStockInput>> GetAll() => GetStockInputAsync(new DtoStockInput());
		public Task<DtoResult<DtoStockInput>> GetOne(DtoStockInput entity) => GetStockInputAsync(entity, true);
		public Task<DtoResult<DtoStockInput>> Find(DtoStockInput entity) => GetStockInputAsync(entity);

		public async Task<DtoResult<DtoStockInput>> Add(DtoStockInput entity)
		{
			var result = new DtoResult<DtoStockInput>();
			try
			{
				await using SqlConnection conn = new(SqlHelper.SqlConn());
				await conn.OpenAsync();
				await using SqlCommand cmd = new("StockInputAdd", conn) { CommandType = CommandType.StoredProcedure };
				cmd.Parameters.AddWithValue("@ReceiptCode", entity.ReceiptCode ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@ReceivedDate", entity.ReceivedDate ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@SupplierId", entity.SupplierId ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@EmployeeId", entity.EmployeeId ?? (object)DBNull.Value);
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
		public async Task<DtoResult<DtoStockInput>> Update(DtoStockInput entity)
		{
			var result = new DtoResult<DtoStockInput>();
			try
			{
				await using SqlConnection conn = new(SqlHelper.SqlConn());
				await conn.OpenAsync();
				await using SqlCommand cmd = new("StockInputUpdate", conn) { CommandType = CommandType.StoredProcedure };
				cmd.Parameters.AddWithValue("@id", entity.id ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@ReceiptCode", entity.ReceiptCode ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@ReceivedDate", entity.ReceivedDate ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@SupplierId", entity.SupplierId ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@EmployeeId", entity.EmployeeId ?? (object)DBNull.Value);
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
		public Task<DtoResult<DtoStockInput>> Delete(DtoStockInput entity) => Delete(entity.id);
		public async Task<DtoResult<DtoStockInput>> Delete(object? entityId)
		{
			var result = new DtoResult<DtoStockInput>();
			try
			{
				await using SqlConnection conn = new(SqlHelper.SqlConn());
				await conn.OpenAsync();
				await using SqlCommand cmd = new("StockInputDelete", conn) { CommandType = CommandType.StoredProcedure };
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