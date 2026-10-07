using Application.Assets.Dtos;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DataLayer.Repository
{
    public class CustomerRepo
    {
        TextProcessing.Encryption encrypt = new();

        private DtoCustomer MapReader(SqlDataReader reader) => new()
        {
            Id = reader["Id"] as int?,
            CustomerCode = reader["CustomerCode"] as string,
            CustomerName = reader["CustomerName"] as string,
            TaxCode = reader["TaxCode"] as string,
            Phone = reader["Phone"] as string,
            Address = reader["Address"] as string
        };

        private async Task<List<DtoCustomer>> ExecuteReaderAync(SqlCommand cmd)
        {
            List<DtoCustomer> result = [];
            var reader = await cmd.ExecuteReaderAsync();
            while (reader.Read())
            {
                result.Add(MapReader(reader));
            }
            return result;
        }

        public async Task<DtoResult<DtoCustomer>> GetAllAsync()
        {
            DtoResult<DtoCustomer> result = new();

            try
            {
                string connStr = encrypt.Decrypt(SQLHelper.ConnString, "truonghoai");
                using SqlConnection conn = new(connStr);
                string sql = "SELECT * FROM Customer c";

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

        public async Task<DtoResult<DtoCustomer>> AddAsync(DtoCustomer dto)
        {
            DtoResult<DtoCustomer> result = new();
            try
            {
                string connStr = encrypt.Decrypt(SQLHelper.ConnString, "truonghoai");
                using SqlConnection conn = new(connStr);
                string sql = "CustomerAdd";

                using SqlCommand cmd = new(sql, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CustomerCode", dto.CustomerCode ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@CustomerName", dto.CustomerName ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@TaxCode", dto.TaxCode ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Phone", dto.Phone ?? (object)DBNull.Value);
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

        public async Task<DtoResult<DtoCustomer>> UpdateAsync(DtoCustomer dto)
        {
            DtoResult<DtoCustomer> result = new();
            try
            {
                string connStr = encrypt.Decrypt(SQLHelper.ConnString, "truonghoai");
                using SqlConnection conn = new(connStr);
                string sql = "CustomerUpdate";

                using SqlCommand cmd = new(sql, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", dto.Id ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@CustomerCode", dto.CustomerCode ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@CustomerName", dto.CustomerName ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@TaxCode", dto.TaxCode ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Phone", dto.Phone ?? (object)DBNull.Value);
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

        public async Task<DtoResult<DtoCustomer>> DeleteAsync(object? ID)
        {
            DtoResult<DtoCustomer> result = new();
            try
            {
                if (ID == null)
                    return new() { Succeed = false, Message = "Chưa có ID" };

                string connStr = encrypt.Decrypt(SQLHelper.ConnString, "truonghoai");
                using SqlConnection conn = new(connStr);
                string sql = "CustomerDelete";

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

        public async Task<DtoResult<DtoCustomer>> DeleteAsync(DtoCustomer dto) => await DeleteAsync(dto.Id);
    }
}