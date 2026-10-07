using Application.Assets.Dtos;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DataLayer.Repository
{
    public class SupplierRepo
    {
        TextProcessing.Encryption encrypt = new();

        private DtoSupplier MapReader(SqlDataReader reader) => new()
        {
            Id = reader["Id"] as int?,
            SupplierCode = reader["SupplierCode"] as string,
            SupplierName = reader["SupplierName"] as string,
            Phone = reader["Phone"] as string,
            Address = reader["Address"] as string
        };

        private async Task<List<DtoSupplier>> ExecuteReaderAync(SqlCommand cmd)
        {
            List<DtoSupplier> result = [];
            var reader = await cmd.ExecuteReaderAsync();
            while (reader.Read())
            {
                result.Add(MapReader(reader));
            }
            return result;
        }

        public async Task<DtoResult<DtoSupplier>> GetAllAsync()
        {
            DtoResult<DtoSupplier> result = new();

            try
            {
                string connStr = encrypt.Decrypt(SQLHelper.ConnString, "truonghoai");
                using SqlConnection conn = new(connStr);
                string sql = "SELECT * FROM Supplier s";

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

        public async Task<DtoResult<DtoSupplier>> AddAsync(DtoSupplier dto)
        {
            DtoResult<DtoSupplier> result = new();
            try
            {
                string connStr = encrypt.Decrypt(SQLHelper.ConnString, "truonghoai");
                using SqlConnection conn = new(connStr);
                string sql = "SupplierAdd";

                using SqlCommand cmd = new(sql, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SupplierCode", dto.SupplierCode ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@SupplierName", dto.SupplierName ?? (object)DBNull.Value);
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

        public async Task<DtoResult<DtoSupplier>> UpdateAsync(DtoSupplier dto)
        {
            DtoResult<DtoSupplier> result = new();
            try
            {
                string connStr = encrypt.Decrypt(SQLHelper.ConnString, "truonghoai");
                using SqlConnection conn = new(connStr);
                string sql = "SupplierUpdate";

                using SqlCommand cmd = new(sql, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", dto.Id ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@SupplierCode", dto.SupplierCode ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@SupplierName", dto.SupplierName ?? (object)DBNull.Value);
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

        public async Task<DtoResult<DtoSupplier>> DeleteAsync(object? ID)
        {
            DtoResult<DtoSupplier> result = new();
            try
            {
                if (ID == null)
                    return new() { Succeed = false, Message = "Chưa có ID" };

                string connStr = encrypt.Decrypt(SQLHelper.ConnString, "truonghoai");
                using SqlConnection conn = new(connStr);
                string sql = "SupplierDelete";

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

        public async Task<DtoResult<DtoSupplier>> DeleteAsync(DtoSupplier dto) => await DeleteAsync(dto.Id);
    }
}