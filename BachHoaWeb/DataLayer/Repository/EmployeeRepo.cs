using Application.Assets.Dto;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DataLayer.Repository
{
    public class EmployeeRepo
    {
        TextProcessing.Encryption encrypt = new();

        private DtoEmployee MapReader(SqlDataReader reader) => new()
        {
            Id = reader["Id"] as int?,
            FullName = reader["FullName"] as string,
            Email = reader["Email"] as string,
            EmployeeCode = reader["EmployeeCode"] as string
        };

        private async Task<List<DtoEmployee>> ExecuteReaderAync(SqlCommand cmd)
        {
            List<DtoEmployee> result = [];
            var reader = await cmd.ExecuteReaderAsync();
            while (reader.Read())
            {
                result.Add(MapReader(reader));
            }
            return result;
        }

        public async Task<DtoResult<DtoEmployee>> GetAllAsync()
        {
            DtoResult<DtoEmployee> result = new();

            try
            {
                string connStr = encrypt.Decrypt(SQLHelper.ConnString, "truonghoai");
                using SqlConnection conn = new(connStr);
                string sql = "SELECT * FROM Employee e";

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

        public async Task<DtoResult<DtoEmployee>> AddAsync(DtoEmployee dto)
        {
            DtoResult<DtoEmployee> result = new();
            try
            {
                string connStr = encrypt.Decrypt(SQLHelper.ConnString, "truonghoai");
                using SqlConnection conn = new(connStr);
                string sql = "EmployeeAdd";

                using SqlCommand cmd = new(sql, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@FullName", dto.FullName ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Email", dto.Email ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@EmployeeCode", dto.EmployeeCode ?? (object)DBNull.Value);

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

        public async Task<DtoResult<DtoEmployee>> UpdateAsync(DtoEmployee dto)
        {
            DtoResult<DtoEmployee> result = new();
            try
            {
                string connStr = encrypt.Decrypt(SQLHelper.ConnString, "truonghoai");
                using SqlConnection conn = new(connStr);
                string sql = "EmployeeUpdate";

                using SqlCommand cmd = new(sql, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", dto.Id ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@FullName", dto.FullName ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Email", dto.Email ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@EmployeeCode", dto.EmployeeCode ?? (object)DBNull.Value);

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

        public async Task<DtoResult<DtoEmployee>> DeleteAsync(object? ID)
        {
            DtoResult<DtoEmployee> result = new();
            try
            {
                if (ID == null)
                    return new() { Succeed = false, Message = "Chưa có ID" };

                string connStr = encrypt.Decrypt(SQLHelper.ConnString, "truonghoai");
                using SqlConnection conn = new(connStr);
                string sql = "EmployeeDelete";

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

        public async Task<DtoResult<DtoEmployee>> DeleteAsync(DtoEmployee dto) => await DeleteAsync(dto.Id);
    }
}