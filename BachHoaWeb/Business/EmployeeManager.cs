using Application.Assets.Dto;
using Services;

namespace Business
{
    public class EmployeeManager(EmployeeService empl, LoginInfoService login)
    {
        //trong này sẽ xử lý dữ liệu trước khi đưa về db

        //trường hợp Get thì ko cần xử lý do dto và model đã gọn từ lớp trước đó
        public async Task<DtoResult<DtoEmployee>> GetAllAsync()
        {
            DtoResult<DtoEmployee> result = new();
            try
            {
                result = await empl.GetAllAsync();
            }
            catch (Exception)
            {

                throw;
            }
            result.Succeed = result.ResultList != null;
            return result;
        }
        public async Task<DtoResult<DtoEmployee>> AddAsync(DtoEmployee dto, DtoLoginInfo loginDto)
        {
            //kiểm tra dữ liệu đầu vào
            if (dto == null)
            {
                return new() { Message = "Lỗi khởi tạo dữ liệu" };
            }
            if (string.IsNullOrEmpty(dto.FullName))
                return new() { Message = "Chưa có Tên đăng nhập" };
            if (string.IsNullOrEmpty(dto.Email))
                return new() { Message = "Chưa có Email" };
            if (string.IsNullOrEmpty(dto.EmployeeCode))
                return new() { Message = "Chưa có Mã nhân viên" };

            DtoResult<DtoEmployee> result = new();
            try
            {
                result = await empl.AddAsync(dto);
                if (result.Succeed) //khi thành công, lấy dữ liệu của customer tạo thành login
                {
                    DtoResult<DtoLoginInfo> loginRS = await login.AddAsync(loginDto);
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
        public async Task<DtoResult<DtoEmployee>> UpdateAsync(DtoEmployee dto, DtoLoginInfo loginDto)
        {
            //kiểm tra dữ liệu đầu vào
            if (dto == null)
            {
                return new() { Message = "Lỗi khởi tạo dữ liệu" };
            }
            if (string.IsNullOrEmpty(dto.FullName))
                return new() { Message = "Chưa có Tên đăng nhập" };
            if (string.IsNullOrEmpty(dto.Email))
                return new() { Message = "Chưa có Email" };
            if (string.IsNullOrEmpty(dto.EmployeeCode))
                return new() { Message = "Chưa có Mã nhân viên" };

            DtoResult<DtoEmployee> result = new();
            try
            {
                result = await empl.UpdateAsync(dto);
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
                result = await empl.DeleteAsync(ID);
            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }
        public async Task<DtoResult<DtoEmployee>> DeleteAsync(DtoEmployee dto) => await DeleteAsync(dto.Id);

        public async Task<DtoResult<DtoEmployee>> UpdateAsync(DtoEmployee selectedEmpl)
        {
            throw new NotImplementedException();
        }
    }
}
