namespace ModelLib.Repositories //Khai báo Class & Interface
{ 
	public class CustomerRepo : IRepository<DtoCustomer> //IRepository<DtoCustomer>: Đảm bảo CustomerRepo tuân thủ khuôn mẫu Repository chuẩn (bắt buộc phải có các hàm CRUD: GetAll, GetOne, Find, Add, Update, Delete).
    {
        //Hàm ánh xạ dữ liệu (MapReader)
        private static DtoCustomer MapReader(SqlDataReader reader) => new() 
        {  //Đọc từng dòng dữ liệu thô từ SQL (dạng SqlDataReader), chuyển đổi (map) thành đối tượng DtoCustomer của C#.
            id = reader["id"] as int?,
			CustomerCode = reader["CustomerCode"] as string,
			CustomerName = reader["CustomerName"] as string,
			TaxCode = reader["TaxCode"] as string,
			Phone = reader["Phone"] as string,
			Address = reader["Address"] as string
		};	 
		private async Task<List<DtoCustomer>> ExecuteReaderAsync(SqlCommand cmd) //Hàm đọc danh sách bất đồng bộ (ExecuteReaderAsync)
        {   //Thực thi lệnh SQL bất đồng bộ (async/await) để duyệt qua all các dòng kết quả -> trả về List<DtoCustomer>
            var result = new List<DtoCustomer>();
			await using var reader = await cmd.ExecuteReaderAsync();
			while (await reader.ReadAsync())
			{
				result.Add(MapReader(reader));
			}
			return result; //trả về kết quả
		}
		private async Task<DtoResult<DtoCustomer>> GetCustomerAsync(DtoCustomer dto, bool exact = false) //Hàm tìm kiếm & Lấy dữ liệu dùng chung (GetCustomerAsync)
        {
            //Là hàm "gốc" chuyên dùng cho việc truy vấn SELECT từ bảng Customer.	
            var result = new DtoResult<DtoCustomer>();
			try
			{
                //Tự động ghép chuỗi WHERE ... dựa trên các thuộc tính có dữ liệu trong dto.
                string condition = RepoBuildHelper<DtoCustomer>.BuildCondition(dto, exact);
				string sql = "SELECT * FROM Customer" + condition;// câu lệnh truy vấn SQL

				await using SqlConnection conn = new(SqlHelper.SqlConn());//Lấy chuỗi kết nối Database từ SqlHelper và mở kết nối bất đồng bộ conn.OpenAsync()
                await conn.OpenAsync();
				await using SqlCommand cmd = new(sql, conn);
				var data = await ExecuteReaderAsync(cmd);

				result.Message = "OK";
				if (exact)			//exact = true: Tìm chính xác 1 đối tượng(trả về trong result.Result).
					result.Result = data.FirstOrDefault();
                else				//exact = false: Tìm danh sách nhiều đối tượng (trả về trong result.ResultList).
                    result.ResultList = data;
			}
			catch (Exception ex) //Bắt lỗi DB nếu có (rớt mạng, sai tên bảng) và trả về thông báo lỗi ở result.Message
            {
				result.Message = ex.Message; // sql bị lỗi mạng thì bắt lại và thông báo 
            }
			return result; // trả về kết quả
        }

        //Các hàm gọi tắt cho thao tác SELECT (GetAll, GetOne, Find)
        public Task<DtoResult<DtoCustomer>> GetAll() => GetCustomerAsync(new DtoCustomer()); //Truyền DTO rỗng=>Tạo câu SQL SELECT * FROM Customer => Trả về toàn bộ khách hàng.

        public Task<DtoResult<DtoCustomer>> GetOne(DtoCustomer entity) => GetCustomerAsync(entity, true); // Truyền DTO chứa ID/Mã => exact = true => Trả về 1 khách hàng duy nhất.

        public Task<DtoResult<DtoCustomer>> Find(DtoCustomer entity) => GetCustomerAsync(entity); //Truyền DTO chứa từ khóa tìm kiếm => Trả về danh sách khách hàng khớp điều kiện.

        public async Task<DtoResult<DtoCustomer>> Add(DtoCustomer entity) //Thêm một khách hàng mới vào Database thông qua Stored Procedure tên CustomerAdd.
        {
			var result = new DtoResult<DtoCustomer>() { Succeed=true};
			try
			{
				await using SqlConnection conn = new(SqlHelper.SqlConn()); //Tạo đối tượng kết nối SQL Server, lấy chuỗi Connection String từ SqlHelper
                await conn.OpenAsync(); //Mở kết nối đến Database bất đồng bộ (không làm đơ/block giao diện)
                await using SqlCommand cmd = new("CustomerAdd", conn) { CommandType = CommandType.StoredProcedure }; 
				//Tạo đối tượng SqlCommand để gửi lệnh chạy Stored Procedure tên "CustomerAdd" qua kết nối conn
                cmd.Parameters.AddWithValue("@CustomerCode", entity.CustomerCode ?? (object)DBNull.Value); //Truyền tham số @CustomerCode vào Stored Procedure
                cmd.Parameters.AddWithValue("@CustomerName", entity.CustomerName ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@TaxCode", entity.TaxCode ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@Phone", entity.Phone ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@Address", entity.Address ?? (object)DBNull.Value);
				var data = await ExecuteReaderAsync(cmd);
				result.Result = data.FirstOrDefault();
				result.Message = result.Result != null ? "OK" : "Không thêm được dữ liệu.";
			}
			catch (Exception ex)
			{
				result.Message = ex.Message;
				result.Succeed = false;
			}
			return result;
		}
		public async Task<DtoResult<DtoCustomer>> Update(DtoCustomer entity)
		{
			var result = new DtoResult<DtoCustomer>() { Succeed = true};
			try
			{
				await using SqlConnection conn = new(SqlHelper.SqlConn());
				await conn.OpenAsync();
				await using SqlCommand cmd = new("CustomerUpdate", conn) { CommandType = CommandType.StoredProcedure };
				cmd.Parameters.AddWithValue("@id", entity.id ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@CustomerCode", entity.CustomerCode ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@CustomerName", entity.CustomerName ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@TaxCode", entity.TaxCode ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@Phone", entity.Phone ?? (object)DBNull.Value);
				cmd.Parameters.AddWithValue("@Address", entity.Address ?? (object)DBNull.Value);
				var data = await ExecuteReaderAsync(cmd);
				result.Result = data.FirstOrDefault();
				result.Message = result.Result != null ? "OK" : "Không cập nhật được dữ liệu.";
			}
			catch (Exception ex)
			{
				result.Message = ex.Message;
				result.Succeed = false;
			}
			return result;
		}
		public Task<DtoResult<DtoCustomer>> Delete(DtoCustomer entity) => Delete(entity.id);
		public async Task<DtoResult<DtoCustomer>> Delete(object? entityId)
		{
			var result = new DtoResult<DtoCustomer>();
			try
			{
				await using SqlConnection conn = new(SqlHelper.SqlConn());
				await conn.OpenAsync();
				await using SqlCommand cmd = new("CustomerDelete", conn) { CommandType = CommandType.StoredProcedure };
				cmd.Parameters.AddWithValue("@id", entityId ?? (object)DBNull.Value);
				int count = await cmd.ExecuteNonQueryAsync();
				result.Message = count > 0 ? "OK" : "Không xóa được dữ liệu";
			}
			catch (Exception ex)
			{
				result.Message = ex.Message;
			}
			return result;
		}
	}
}