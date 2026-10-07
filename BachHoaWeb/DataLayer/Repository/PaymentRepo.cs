using Application.Assets.Dtos;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DataLayer.Repository
{
    public class PaymentRepo
    {
        TextProcessing.Encryption encrypt = new();

        private DtoPayment MapReader(SqlDataReader reader) => new()
        {
            Id = reader["Id"] as int?,
            OrderId = reader["OrderId"] as int?,
            TotalAmount = reader["TotalAmount"] as decimal?,
            RemainingDebt = reader["RemainingDebt"] as decimal?
            
        };

        private async Task<List<DtoPayment>> ExecuteReaderAync(SqlCommand cmd)
        {
            List<DtoPayment> result = [];
            var reader = await cmd.ExecuteReaderAsync();
            while (reader.Read())
            {
                result.Add(MapReader(reader));
            }
            return result;
        }

        public async Task<DtoResult<DtoPayment>> GetAllAsync()
        {
            DtoResult<DtoPayment> result = new();

            try
            {
                string connStr = encrypt.Decrypt(SQLHelper.ConnString, "truonghoai");
                using SqlConnection conn = new(connStr);
                string sql = "SELECT * FROM Payment p";

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

        public async Task<DtoResult<DtoPayment>> AddAsync(DtoPayment dto)
        {
            DtoResult<DtoPayment> result = new();
            try
            {
                string connStr = encrypt.Decrypt(SQLHelper.ConnString, "truonghoai");
                using SqlConnection conn = new(connStr);
                string sql = "PaymentAdd";

                using SqlCommand cmd = new(sql, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@OrderId", dto.OrderId ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@TotalAmount", dto.TotalAmount ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@RemainingDebt", dto.RemainingDebt ?? (object)DBNull.Value);

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

        public async Task<DtoResult<DtoPayment>> UpdateAsync(DtoPayment dto)
        {
            DtoResult<DtoPayment> result = new();
            try
            {
                string connStr = encrypt.Decrypt(SQLHelper.ConnString, "truonghoai");
                using SqlConnection conn = new(connStr);
                string sql = "PaymentUpdate";

                using SqlCommand cmd = new(sql, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", dto.Id ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@OrderId", dto.OrderId ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@TotalAmount", dto.TotalAmount ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@RemainingDebt", dto.RemainingDebt ?? (object)DBNull.Value);

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

        public async Task<DtoResult<DtoPayment>> DeleteAsync(object? ID)
        {
            DtoResult<DtoPayment> result = new();
            try
            {
                if (ID == null)
                    return new() { Succeed = false, Message = "Chưa có ID" };

                string connStr = encrypt.Decrypt(SQLHelper.ConnString, "truonghoai");
                using SqlConnection conn = new(connStr);
                string sql = "PaymentDelete";

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

        public async Task<DtoResult<DtoPayment>> DeleteAsync(DtoPayment dto) => await DeleteAsync(dto.Id);
    }
}
