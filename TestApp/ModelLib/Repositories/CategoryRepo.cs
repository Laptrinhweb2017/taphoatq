namespace ModelLib.Repositories{
	public class CategoryRepo : IRepository<DtoCategory>
	{
		private static DtoCategory MapReader(SqlDataReader reader) => new()
		{
			id = reader["id"] as int?,
			CateName = reader["CateName"] as string
		};
		private async Task<List<DtoCategory>> ExecuteReaderAsync(SqlCommand cmd)
		{
			var result = new List<DtoCategory>();
			await using var reader = await cmd.ExecuteReaderAsync();
			while (await reader.ReadAsync())
			{
				result.Add(MapReader(reader));
			}
			return result;
		}
		private async Task<DtoResult<DtoCategory>> GetCategoryAsync(DtoCategory dto, bool exact = false)
		{
			var result = new DtoResult<DtoCategory>();
			try
			{
				string condition = RepoBuildHelper<DtoCategory>.BuildCondition(dto, exact);
				string sql = "SELECT * FROM Category" + condition;

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
		public Task<DtoResult<DtoCategory>> GetAll() => GetCategoryAsync(new DtoCategory());
		public Task<DtoResult<DtoCategory>> GetOne(DtoCategory entity) => GetCategoryAsync(entity, true);
		public Task<DtoResult<DtoCategory>> Find(DtoCategory entity) => GetCategoryAsync(entity);

		public async Task<DtoResult<DtoCategory>> Add(DtoCategory entity)
		{
			var result = new DtoResult<DtoCategory>();
			try
			{
				await using SqlConnection conn = new(SqlHelper.SqlConn());
				await conn.OpenAsync();
				await using SqlCommand cmd = new("CategoryAdd", conn) { CommandType = CommandType.StoredProcedure };
				cmd.Parameters.AddWithValue("@CateName", entity.CateName ?? (object)DBNull.Value);
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
		public async Task<DtoResult<DtoCategory>> Update(DtoCategory entity)
		{
			var result = new DtoResult<DtoCategory>();
			try
			{
				await using SqlConnection conn = new(SqlHelper.SqlConn());
				await conn.OpenAsync();
				await using SqlCommand cmd = new("CategoryUpdate", conn) { CommandType = CommandType.StoredProcedure };
				cmd.Parameters.AddWithValue("@id", entity.id ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@CateName", entity.CateName ?? (object)DBNull.Value);
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
		public Task<DtoResult<DtoCategory>> Delete(DtoCategory entity) => Delete(entity.id);
		public async Task<DtoResult<DtoCategory>> Delete(object? entityId)
		{
			var result = new DtoResult<DtoCategory>();
			try
			{
				await using SqlConnection conn = new(SqlHelper.SqlConn());
				await conn.OpenAsync();
				await using SqlCommand cmd = new("CategoryDelete", conn) { CommandType = CommandType.StoredProcedure };
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