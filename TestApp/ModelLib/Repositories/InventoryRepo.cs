namespace ModelLib.Repositories{
	public class InventoryRepo : IRepository<DtoInventory>
	{
		private static DtoInventory MapReader(SqlDataReader reader) => new()
		{
			id = reader["id"] as int?,
			WarehouseId = reader["WarehouseId"] as int?,
			ProductId = reader["ProductId"] as int?,
			Quantity = reader["Quantity"] as int?
		};
		private async Task<List<DtoInventory>> ExecuteReaderAsync(SqlCommand cmd)
		{
			var result = new List<DtoInventory>();
			await using var reader = await cmd.ExecuteReaderAsync();
			while (await reader.ReadAsync())
			{
				result.Add(MapReader(reader));
			}
			return result;
		}
		private async Task<DtoResult<DtoInventory>> GetInventoryAsync(DtoInventory dto, bool exact = false)
		{
			var result = new DtoResult<DtoInventory>();
			try
			{
				string condition = RepoBuildHelper<DtoInventory>.BuildCondition(dto, exact);
				string sql = "SELECT * FROM Inventory" + condition;

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
		public Task<DtoResult<DtoInventory>> GetAll() => GetInventoryAsync(new DtoInventory());
		public Task<DtoResult<DtoInventory>> GetOne(DtoInventory entity) => GetInventoryAsync(entity, true);
		public Task<DtoResult<DtoInventory>> Find(DtoInventory entity) => GetInventoryAsync(entity);

		public async Task<DtoResult<DtoInventory>> Add(DtoInventory entity)
		{
			var result = new DtoResult<DtoInventory>();
			try
			{
				await using SqlConnection conn = new(SqlHelper.SqlConn());
				await conn.OpenAsync();
				await using SqlCommand cmd = new("InventoryAdd", conn) { CommandType = CommandType.StoredProcedure };
				cmd.Parameters.AddWithValue("@WarehouseId", entity.WarehouseId ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@ProductId", entity.ProductId ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@Quantity", entity.Quantity ?? (object)DBNull.Value);
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
		public async Task<DtoResult<DtoInventory>> Update(DtoInventory entity)
		{
			var result = new DtoResult<DtoInventory>();
			try
			{
				await using SqlConnection conn = new(SqlHelper.SqlConn());
				await conn.OpenAsync();
				await using SqlCommand cmd = new("InventoryUpdate", conn) { CommandType = CommandType.StoredProcedure };
				cmd.Parameters.AddWithValue("@id", entity.id ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@WarehouseId", entity.WarehouseId ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@ProductId", entity.ProductId ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@Quantity", entity.Quantity ?? (object)DBNull.Value);
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
		public Task<DtoResult<DtoInventory>> Delete(DtoInventory entity) => Delete(entity.id);
		public async Task<DtoResult<DtoInventory>> Delete(object? entityId)
		{
			var result = new DtoResult<DtoInventory>();
			try
			{
				await using SqlConnection conn = new(SqlHelper.SqlConn());
				await conn.OpenAsync();
				await using SqlCommand cmd = new("InventoryDelete", conn) { CommandType = CommandType.StoredProcedure };
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