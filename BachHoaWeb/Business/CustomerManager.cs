using Application.Assets.Dto;
using Services;

namespace Business
{
    public class CustomerManager(CustomerService cust, LoginInfoService login)
    {
        //trong này sẽ xử lý dữ liệu trước khi đưa về db

        //trường hợp Get thì ko cần xử lý do dto và model đã gọn từ lớp trước đó
        public async Task<DtoResult<DtoCustomer>> GetAllAsync()
        {
            DtoResult<DtoCustomer> result = new();
            try
            {
                result = await cust.GetAllAsync();
            }
            catch (Exception)
            {

                throw;
            }
            result.Succeed = result.ResultList != null;
            return result;
        }
        public async Task<DtoResult<DtoCustomer>> AddAsync(DtoCustomer dto, DtoLoginInfo loginDto)
        {
            //kiểm tra dữ liệu đầu vào
            if (dto == null)
            {
                return new() { Message = "Lỗi khởi tạo dữ liệu" };
            }
            if (string.IsNullOrEmpty(dto.CustomerCode))
                return new() { Message = "Chưa có Tên đăng nhập" };
            if (string.IsNullOrEmpty(dto.CustomerName))
                return new() { Message = "Cần nhập đủ họ tên" };
            if (string.IsNullOrEmpty(dto.Phone))
                return new() { Message = "Chưa có Số điện thoại" };
            if (string.IsNullOrEmpty(dto.Address))
                return new() { Message = "Chưa có Địa chỉ" };

            DtoResult<DtoCustomer> result = new();
            try
            {
                result = await cust.AddAsync(dto);//?????
                if (result.Succeed) //khi thành công, lấy dữ liệu của customer tạo thành login
                {
                    DtoResult<DtoLoginInfo> loginRS = await login.AddAsync(loginDto);//?????
                    result.Succeed = loginRS.Succeed;
                }

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
            //kiểm tra dữ liệu đầu vào
            if (dto == null)
            {
                return new() { Message = "Lỗi khởi tạo dữ liệu" };
            }
            if (string.IsNullOrEmpty(dto.CustomerCode))//
                return new() { Message = "Chưa có Tên đăng nhập" };
            if (string.IsNullOrEmpty(dto.CustomerName))
                return new() { Message = "Cần nhập đủ họ tên" };
            if (string.IsNullOrEmpty(dto.Phone))
                return new() { Message = "Chưa có Số điện thoại" };
            if (string.IsNullOrEmpty(dto.Address))
                return new() { Message = "Chưa có Địa chỉ" };

            DtoResult<DtoCustomer> result = new();
            try
            {
                result = await cust.UpdateAsync(dto);
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
                result = await cust.DeleteAsync(ID);
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
