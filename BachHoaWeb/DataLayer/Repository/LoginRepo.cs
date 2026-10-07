using Application.Asset.Dtos;
using Application.Assets.Dtos;
using Application.Assets.FullDto;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelLib.Repositories
{
    public class LoginRepo()
    {
        private static FullLogin MapReader(SqlDataReader reader) => new()
        {
            id = reader["id"] as int?,
            Password = reader["password"] as string,
            UID = reader["uid"] as int?,
            UserID = reader["UserID"] as string,
            UType = reader["UType"] as string,
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
        public async Task<DtoResult<FullLogin>> GetLogin(string StaffID)
        {
            DtoResult<FullLogin> result = new() { ResultList = [] };
            try
            {
                await using SqlConnection conn = new(SqlHelper.SqlConn());
                await conn.OpenAsync();
                await using SqlCommand cmd = new("GetLogin", conn) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.AddWithValue("@StaffCode", StaffID);
                
                var data = await ExecuteReaderAsync(cmd);
                result.ResultList = data;
                result.Result = data.Count > 0 ? data[0] : null;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }
        public async Task<DtoResult<FullLogin>> Login(int StaffID)
        {
            DtoResult<FullLogin> result = new() { ResultList = [] };
            try
            {
                await using SqlConnection conn = new(SqlHelper.SqlConn());
                await conn.OpenAsync();
                await using SqlCommand cmd = new("LoginByID", conn) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.AddWithValue("@id", StaffID);

                var data = await ExecuteReaderAsync(cmd);
                result.ResultList = data;
                result.Result = data.Count > 0 ? data[0] : null;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return result;
        }
    }
}
