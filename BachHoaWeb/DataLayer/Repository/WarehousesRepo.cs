using Application.Assets.Dto;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DataLayer.Repository
{
    public class WarehousesRepo
    {
        TextProcessing.Encryption encrypt = new();

        private DtoWarehouses MapReader(SqlDataReader reader) => new()
        {
            Id = reader["Id"] as int?,
            WarehouseName = reader["WarehouseName"] as string,
            Address = reader["Address"] as string
        };

        private async Task<List<DtoWarehouses>> ExecuteReaderAync(SqlCommand cmd)
        {
            List<DtoWarehouses> result = [];
            var reader = await cmd.ExecuteReaderAsync();
            while (reader.Read())
            {
                result.Add(MapReader(reader));
            }
            return result;
        }

        public async Task<DtoResult<DtoWarehouses>> GetAllAsync()
        {
            DtoResult<DtoWarehouses> result = new();

            try
            {
                string connStr = encrypt.Decrypt(SQLHelper.ConnString, "truonghoai");
                using SqlConnection conn = new(connStr);
                string sql = "SELECT * FROM Warehouses w";

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

        public async Task<DtoResult<DtoWarehouses>> AddAsync(DtoWarehouses dto)
        {
            DtoResult<DtoWarehouses> result = new();
            try
            {
                string connStr = encrypt.Decrypt(SQLHelper.ConnString, "truonghoai");
                using SqlConnection conn = new(connStr);
                string sql = "WarehousesAdd";

                using SqlCommand cmd = new(sql, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@WarehouseName", dto.WarehouseName ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Address", dto.Address ?? (object)DBNull.Value);

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

        public async Task<DtoResult<DtoWarehouses>> UpdateAsync(DtoWarehouses dto)
        {
            DtoResult<DtoWarehouses> result = new();
            try
            {
                string connStr = encrypt.Decrypt(SQLHelper.ConnString, "truonghoai");
                using SqlConnection conn = new(connStr);
                string sql = "WarehousesUpdate";

                using SqlCommand cmd = new(sql, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", dto.Id ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@WarehouseName", dto.WarehouseName ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Address", dto.Address ?? (object)DBNull.Value);

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

        public async Task<DtoResult<DtoWarehouses>> DeleteAsync(object? ID)
        {
            DtoResult<DtoWarehouses> result = new();
            try
            {
                if (ID == null)
                    return new() { Succeed = false, Message = "Chưa có ID" };

                string connStr = encrypt.Decrypt(SQLHelper.ConnString, "truonghoai");
                using SqlConnection conn = new(connStr);
                string sql = "WarehousesDelete";

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

        public async Task<DtoResult<DtoWarehouses>> DeleteAsync(DtoWarehouses dto) => await DeleteAsync(dto.Id);
    }
}