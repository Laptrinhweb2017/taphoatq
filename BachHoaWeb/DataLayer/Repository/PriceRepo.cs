using Application.Assets.Dtos;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DataLayer.Repository
{
    public class PriceRepo
    {
        TextProcessing.Encryption encrypt = new();

        private DtoPrice MapReader(SqlDataReader reader) => new()
        {
            Id = reader["Id"] as int?,
            ProdId = reader["ProdId"] as int?,
            Price = reader["Price"] as decimal?,
            AppliedDate = reader["AppliedDate"] as DateTime?

        };

        private async Task<List<DtoPrice>> ExecuteReaderAync(SqlCommand cmd)
        {
            List<DtoPrice> result = [];
            var reader = await cmd.ExecuteReaderAsync();
            while (reader.Read())
            {
                result.Add(MapReader(reader));
            }
            return result;
        }

        public async Task<DtoResult<DtoPrice>> GetAllAsync()
        {
            DtoResult<DtoPrice> result = new();

            try
            {
                string connStr = encrypt.Decrypt(SQLHelper.ConnString, "truonghoai");
                using SqlConnection conn = new(connStr);
                string sql = "SELECT * FROM Price p";

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

        public async Task<DtoResult<DtoPrice>> AddAsync(DtoPrice dto)
        {
            DtoResult<DtoPrice> result = new();
            try
            {
                string connStr = encrypt.Decrypt(SQLHelper.ConnString, "truonghoai");
                using SqlConnection conn = new(connStr);
                string sql = "PriceAdd";

                using SqlCommand cmd = new(sql, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProdId", dto.ProdId ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Price", dto.Price ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@AppliedDate", dto.AppliedDate ?? (object)DBNull.Value);

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

        public async Task<DtoResult<DtoPrice>> UpdateAsync(DtoPrice dto)
        {
            DtoResult<DtoPrice> result = new();
            try
            {
                string connStr = encrypt.Decrypt(SQLHelper.ConnString, "truonghoai");
                using SqlConnection conn = new(connStr);
                string sql = "PriceUpdate";

                using SqlCommand cmd = new(sql, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", dto.Id ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@ProdId", dto.ProdId ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Price", dto.Price ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@AppliedDate", dto.AppliedDate ?? (object)DBNull.Value);

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

        public async Task<DtoResult<DtoPrice>> DeleteAsync(object? ID)
        {
            DtoResult<DtoPrice> result = new();
            try
            {
                if (ID == null)
                    return new() { Succeed = false, Message = "Chưa có ID" };

                string connStr = encrypt.Decrypt(SQLHelper.ConnString, "truonghoai");
                using SqlConnection conn = new(connStr);
                string sql = "PriceDelete";

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

        public async Task<DtoResult<DtoPrice>> DeleteAsync(DtoPrice dto) => await DeleteAsync(dto.Id);
    }
}
