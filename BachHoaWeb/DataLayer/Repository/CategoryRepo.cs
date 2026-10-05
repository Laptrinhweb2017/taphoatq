using Application.Assets.Dto;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DataLayer.Repository
{
    public class CategoryRepo
    {
        TextProcessing.Encryption encrypt = new();

        private DtoCategory MapReader(SqlDataReader reader) => new()
        {
            Id = reader["Id"] as int?,
            CateName = reader["CateName"] as string
        };

        private async Task<List<DtoCategory>> ExecuteReaderAync(SqlCommand cmd)
        {
            List<DtoCategory> result = [];
            var reader = await cmd.ExecuteReaderAsync();
            while (reader.Read())
            {
                result.Add(MapReader(reader));
            }
            return result;
        }

        public async Task<DtoResult<DtoCategory>> GetAllAsync()
        {
            DtoResult<DtoCategory> result = new();

            try
            {
                string connStr = encrypt.Decrypt(SQLHelper.ConnString, "truonghoai");
                using SqlConnection conn = new(connStr);
                string sql = "SELECT * FROM Category c";

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

        public async Task<DtoResult<DtoCategory>> AddAsync(DtoCategory dto)
        {
            DtoResult<DtoCategory> result = new();
            try
            {
                string connStr = encrypt.Decrypt(SQLHelper.ConnString, "truonghoai");
                using SqlConnection conn = new(connStr);
                string sql = "CategoryAdd";

                using SqlCommand cmd = new(sql, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CateName", dto.CateName ?? (object)DBNull.Value);

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

        public async Task<DtoResult<DtoCategory>> UpdateAsync(DtoCategory dto)
        {
            DtoResult<DtoCategory> result = new();
            try
            {
                string connStr = encrypt.Decrypt(SQLHelper.ConnString, "truonghoai");
                using SqlConnection conn = new(connStr);
                string sql = "CategoryUpdate";

                using SqlCommand cmd = new(sql, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", dto.Id ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@CateName", dto.CateName ?? (object)DBNull.Value);

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

        public async Task<DtoResult<DtoCategory>> DeleteAsync(object? ID)
        {
            DtoResult<DtoCategory> result = new();
            try
            {
                if (ID == null)
                    return new() { Succeed = false, Message = "Chưa có ID" };

                string connStr = encrypt.Decrypt(SQLHelper.ConnString, "truonghoai");
                using SqlConnection conn = new(connStr);
                string sql = "CategoryDelete";

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

        public async Task<DtoResult<DtoCategory>> DeleteAsync(DtoCategory dto) => await DeleteAsync(dto.Id);
    }
}