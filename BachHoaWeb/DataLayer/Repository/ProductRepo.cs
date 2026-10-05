using Application.Assets.Dto;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DataLayer.Repository
{
    public class ProductRepo
    {
        TextProcessing.Encryption encrypt = new();

        private DtoProduct MapReader(SqlDataReader reader) => new()
        {
            BrandId = reader["BrandId"] as int?,
            CateId = reader["CateId"] as int?,
            CreatedAt = reader["CreatedAt"] as DateTime?,
            Descript = reader["Descript"] as string,
            Id = reader["Id"] as int?,
            Image = reader["Image"] as string,
            ProductCode = reader["ProductCode"] as string,
            ProductName = reader["ProductName"] as string,
        };
        private async Task<List<DtoProduct>> ExecuteReaderAync(SqlCommand cmd)
        {
            List<DtoProduct> result = [];
            var reader = await cmd.ExecuteReaderAsync();
            while (reader.Read())
            {
                //scope
                //DtoProduct dto = MapReader(reader);
                //result.Add(dto);
                result.Add(MapReader(reader));
            }
            return result;
        }
        public async Task<DtoResult<DtoProduct>> GetAllAsync()
        {
            DtoResult<DtoProduct> result = new();

            try
            {
                string connStr = encrypt.Decrypt(SQLHelper.ConnString, "truonghoai");
                using SqlConnection conn = new(connStr);
                string sql = "SELECT * FROM Products p";
                //tạo command để chuẩn bị thực thi query
                using SqlCommand cmd = new(sql,conn);
                //Mở kết nối
                await conn.OpenAsync();
                //Thực hiện query thông qua reader
                //SqlDataReader reader = await cmd.ExecuteReaderAsync();
                //Thực hiện đọc lần lượt các dòng dữ liệu và chuyển thành DTO
                result.ResultList = await ExecuteReaderAync(cmd);
                
            }
            catch (Exception)
            {
                throw;
            }

            result.Succeed = result.ResultList!=null;
            return result;
        }
        public async Task<DtoResult<DtoProduct>> AddAsync(DtoProduct dto)
        {
            DtoResult<DtoProduct> result = new();
            try
            {
                string connStr = encrypt.Decrypt(SQLHelper.ConnString, "truonghoai");
                using SqlConnection conn = new(connStr);
                string sql = "ProductAdd";
                //tạo command để chuẩn bị thực thi query
                using SqlCommand cmd = new(sql, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProductCode", dto.ProductCode ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@ProductName", dto.ProductName ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Image", dto.Image ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Descript", dto.Descript ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@CreatedAt", dto.CreatedAt ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@BrandId", dto.BrandId ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@CateId", dto.CateId ?? (object)DBNull.Value);
                //Mở kết nối
                await conn.OpenAsync();
                var data = await ExecuteReaderAync(cmd);
                result.Result = data[0];
            }
            catch (Exception)
            {
                throw;
            }
            result.Succeed = result.Result!=null;
            return result;
        }
        public async Task<DtoResult<DtoProduct>> UpdateAsync(DtoProduct dto)
        {
            DtoResult<DtoProduct> result = new();
            try
            {
                string connStr = encrypt.Decrypt(SQLHelper.ConnString, "truonghoai");
                using SqlConnection conn = new(connStr);
                string sql = "ProductUpdate";
                //tạo command để chuẩn bị thực thi query
                using SqlCommand cmd = new(sql, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", dto.Id ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@ProductCode", dto.ProductCode ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@ProductName", dto.ProductName ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Image", dto.Image ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@Descript", dto.Descript ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@CreatedAt", dto.CreatedAt ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@BrandId", dto.BrandId ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@CateId", dto.CateId ?? (object)DBNull.Value);
                //Mở kết nối
                await conn.OpenAsync();
                var data = await ExecuteReaderAync(cmd);
                result.Result = data[0];
            }
            catch (Exception)
            {
                throw;
            }
            result.Succeed = result.Result != null;
            return result;
        }
        public async Task<DtoResult<DtoProduct>> DeleteAsync(object? ID)
        {
            DtoResult<DtoProduct> result = new();
            try
            {
                if (ID == null)
                    return new() { Succeed = false, Message = "Chưa có ID" };
                string connStr = encrypt.Decrypt(SQLHelper.ConnString, "truonghoai");
                using SqlConnection conn = new(connStr);
                string sql = "ProductDelete";
                //tạo command để chuẩn bị thực thi query
                using SqlCommand cmd = new(sql, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@id", ID);
                //Mở kết nối
                await conn.OpenAsync();
                int count = await cmd.ExecuteNonQueryAsync();
                result.Succeed = count>0;
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }
        public async Task<DtoResult<DtoProduct>> DeleteAsync(DtoProduct dto) => await DeleteAsync(dto.Id);
    }
}
