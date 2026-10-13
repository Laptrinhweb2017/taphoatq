using Application.Assets.FullDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLib.Repositories
{
    public class LoginRepo
    {
        private static FullLogin MapReader(SqlDataReader reader) => new()
        {
            id = reader["id"] as int?,
            Password = reader["password"] as string,
            UID = reader["UID"] as int?,
            UserID = reader["UserID"] as string,
            UType = reader["UType"] as string,
            LoginName = reader["LoginName"] as string,
        };
        private async Task<List<FullLogin>> ExecuteReaderAsync(SqlCommand cmd)
        {
            var result = new List<FullLogin>();
            await using var reader = await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                result.Add(MapReader(reader));
            }
            return result;
        }
        private async Task<DtoResult<FullLogin>> GetLoginAsync()
        {
            var result = new DtoResult<FullLogin>();
            try
            {
                string sql = "GetLogin";

                await using SqlConnection conn = new(SqlHelper.SqlConn());
                await conn.OpenAsync();
                await using SqlCommand cmd = new(sql, conn) { CommandType = CommandType.StoredProcedure };
                var data = await ExecuteReaderAsync(cmd);

                result.Message = "OK";
                result.ResultList = data;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }
        public async Task<DtoResult<FullLogin>> GetAllLogin() => await GetLoginAsync();
    }
}
