using Application.Assets.Dtos;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DataLayer.Repository
{
    public class InventoryRepo
    {
          
        TextProcessing.Encryption encrypt = new();

        private DtoInventory MapReader(SqlDataReader reader) => new()
        {
            Id = reader["Id"] as int?,
            WarehouseId = reader["WarehouseId"] as int?,
            ProductId = reader["ProductId"] as int?,
            Quantity = reader["Quantity"] as int?
        };

        private async Task<List<DtoInventory>> ExecuteReaderAync(SqlCommand cmd)
        {
            List<DtoInventory> result = [];
            var reader = await cmd.ExecuteReaderAsync();
            while (reader.Read())
            {
                result.Add(MapReader(reader));
            }
            return result;
        }

        public async Task<DtoResult<DtoInventory>> GetAllAsync()
        {
            DtoResult<DtoInventory> result = new();

            try
            {
                string connStr = encrypt.Decrypt(SQLHelper.ConnString, "truonghoai");
                using SqlConnection conn = new(connStr);
                string sql = "SELECT * FROM Inventory i";

                using SqlCommand cmd = new(sql, conn);
                await conn.OpenAsync();
                result.ResultList = await ExecuteReaderAync(cmd);
            }
            catch (Exception)
            {
                throw;
            }

            result.Succeed = result.ResultList != null;
            return result;
        }

        public async Task<DtoResult<DtoInventory>> AddAsync(DtoInventory dto)
        {
            DtoResult<DtoInventory> result = new();
            try
            {
                string connStr = encrypt.Decrypt(SQLHelper.ConnString, "truonghoai");
                using SqlConnection conn = new(connStr);
                string sql = "InventoryAdd";

                using SqlCommand cmd = new(sql, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@WarehouseId", dto.WarehouseId ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@ProductId", dto.ProductId ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Quantity", dto.Quantity ?? (object)DBNull.Value);

                await conn.OpenAsync();
                var data = await ExecuteReaderAync(cmd);
                result.Result = data.Count > 0 ? data[0] : null;
            }
            catch (Exception)
            {
                throw;
            }
            result.Succeed = result.Result != null;
            return result;
        }

        public async Task<DtoResult<DtoInventory>> UpdateAsync(DtoInventory dto)
        {
            DtoResult<DtoInventory> result = new();
            try
            {
                string connStr = encrypt.Decrypt(SQLHelper.ConnString, "truonghoai");
                using SqlConnection conn = new(connStr);
                string sql = "InventoryUpdate";

                using SqlCommand cmd = new(sql, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", dto.Id ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@WarehouseId", dto.WarehouseId ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@ProductId", dto.ProductId ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Quantity", dto.Quantity ?? (object)DBNull.Value);

                await conn.OpenAsync();
                var data = await ExecuteReaderAync(cmd);
                result.Result = data.Count > 0 ? data[0] : null;
            }
            catch (Exception)
            {
                throw;
            }
            result.Succeed = result.Result != null;
            return result;
        }

        public async Task<DtoResult<DtoInventory>> DeleteAsync(object? ID)
        {
            DtoResult<DtoInventory> result = new();
            try
            {
                if (ID == null)
                    return new() { Succeed = false, Message = "Chưa có ID" };

                string connStr = encrypt.Decrypt(SQLHelper.ConnString, "truonghoai");
                using SqlConnection conn = new(connStr);
                string sql = "InventoryDelete";

                using SqlCommand cmd = new(sql, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", ID);

                await conn.OpenAsync();
                int count = await cmd.ExecuteNonQueryAsync();
                result.Succeed = count > 0;
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        public async Task<DtoResult<DtoInventory>> DeleteAsync(DtoInventory dto) => await DeleteAsync(dto.Id);
    }
}

