namespace ModelLib.Repositories{
	public class PriceRepo : IRepository<DtoPrice>
	{
		private static DtoPrice MapReader(SqlDataReader reader) => new()
		{
			id = reader["id"] as int?,
			ProdID = reader["ProdID"] as int?,
			Price = reader["Price"] as decimal?,
			AppliedDate = reader["AppliedDate"] as DateTime?
		};
		private async Task<List<DtoPrice>> ExecuteReaderAsync(SqlCommand cmd)
		{
			var result = new List<DtoPrice>();
			await using var reader = await cmd.ExecuteReaderAsync();
			while (await reader.ReadAsync())
			{
				result.Add(MapReader(reader));
			}
			return result;
		}
		private async Task<DtoResult<DtoPrice>> GetPriceAsync(DtoPrice dto, bool exact = false)
		{
			var result = new DtoResult<DtoPrice>();
			try
			{
				string condition = RepoBuildHelper<DtoPrice>.BuildCondition(dto, exact);
				string sql = "SELECT * FROM Price" + condition;

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
		public Task<DtoResult<DtoPrice>> GetAll() => GetPriceAsync(new DtoPrice());
		public Task<DtoResult<DtoPrice>> GetOne(DtoPrice entity) => GetPriceAsync(entity, true);
		public Task<DtoResult<DtoPrice>> Find(DtoPrice entity) => GetPriceAsync(entity);

		public async Task<DtoResult<DtoPrice>> Add(DtoPrice entity)
		{
			var result = new DtoResult<DtoPrice>();
			try
			{
				await using SqlConnection conn = new(SqlHelper.SqlConn());
				await conn.OpenAsync();
				await using SqlCommand cmd = new("PriceAdd", conn) { CommandType = CommandType.StoredProcedure };
				cmd.Parameters.AddWithValue("@ProdID", entity.ProdID ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@Price", entity.Price ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@AppliedDate", entity.AppliedDate ?? (object)DBNull.Value);
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
		public async Task<DtoResult<DtoPrice>> Update(DtoPrice entity)
		{
			var result = new DtoResult<DtoPrice>();
			try
			{
				await using SqlConnection conn = new(SqlHelper.SqlConn());
				await conn.OpenAsync();
				await using SqlCommand cmd = new("PriceUpdate", conn) { CommandType = CommandType.StoredProcedure };
				cmd.Parameters.AddWithValue("@id", entity.id ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@ProdID", entity.ProdID ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@Price", entity.Price ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@AppliedDate", entity.AppliedDate ?? (object)DBNull.Value);
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
		public Task<DtoResult<DtoPrice>> Delete(DtoPrice entity) => Delete(entity.id);
		public async Task<DtoResult<DtoPrice>> Delete(object? entityId)
		{
			var result = new DtoResult<DtoPrice>();
			try
			{
				await using SqlConnection conn = new(SqlHelper.SqlConn());
				await conn.OpenAsync();
				await using SqlCommand cmd = new("PriceDelete", conn) { CommandType = CommandType.StoredProcedure };
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