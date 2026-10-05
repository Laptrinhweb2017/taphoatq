using Application.Assets.Dto;
using Application.Assets.FullDto;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DataLayer.Repository
{
    public class LoginInfoRepo
    {
        TextProcessing.Encryption encrypt = new();

        private DtoLoginInfo MapReader(SqlDataReader reader) => new()
        {
            Id = reader["Id"] as int?,
            UserID = reader["UserID"] as string,
            Password = reader["Password"] as string
        };

        private async Task<List<DtoLoginInfo>> ExecuteReaderAync(SqlCommand cmd)
        {
            List<DtoLoginInfo> result = [];
            var reader = await cmd.ExecuteReaderAsync();
            while (reader.Read())
            {
                result.Add(MapReader(reader));
            }
            return result;
        }

        private FullLogin MapFullReader(SqlDataReader reader) => new()
        {
            id = reader["id"] as int?,
            UserID = reader["UserID"] as string,
            Password = reader["Password"] as string,
            UID = reader["UID"] as int?,
            UType = reader["UType"] as string
        };

        private async Task<List<FullLogin>> ExecuteFullReaderAync(SqlCommand cmd)
        {
            List<FullLogin> result = [];
            var reader = await cmd.ExecuteReaderAsync();
            while (reader.Read())
            {
                result.Add(MapFullReader(reader));
            }
            return result;
        }
        public async Task<DtoResult<FullLogin>> GetLogin()
        {
            DtoResult<FullLogin> result = new();
            try
            {
                string connStr = encrypt.Decrypt(SQLHelper.ConnString, "truonghoai");
                using SqlConnection conn = new(connStr);
                string sql = "GetLogin";

                using SqlCommand cmd = new(sql, conn) { CommandType = CommandType.StoredProcedure };

                await conn.OpenAsync();
                var data = await ExecuteFullReaderAync(cmd);
                result.ResultList = data;
            }
            catch (Exception)
            {
                throw;
            }
            result.Succeed = result.ResultList != null;
            return result;
        }
        public async Task<DtoResult<DtoLoginInfo>> AddAsync(DtoLoginInfo dto)
        {
            DtoResult<DtoLoginInfo> result = new();
            try
            {
                string connStr = encrypt.Decrypt(SQLHelper.ConnString, "truonghoai");
                using SqlConnection conn = new(connStr);
                string sql = "LoginInfoAdd";

                using SqlCommand cmd = new(sql, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@UserID", dto.UserID ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Password", dto.Password ?? (object)DBNull.Value);

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

        public async Task<DtoResult<DtoLoginInfo>> UpdateAsync(DtoLoginInfo dto)
        {
            DtoResult<DtoLoginInfo> result = new();
            try
            {
                string connStr = encrypt.Decrypt(SQLHelper.ConnString, "truonghoai");
                using SqlConnection conn = new(connStr);
                string sql = "LoginInfoUpdate";

                using SqlCommand cmd = new(sql, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", dto.Id ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Password", dto.Password ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@UserID", dto.UserID ?? (object)DBNull.Value);

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
    }
}
