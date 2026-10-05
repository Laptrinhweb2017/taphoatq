using Application.Assets.Common;
using Application.Assets.Dto;
using Application.Assets.FullDto;
using Business;
using Services;


namespace ViewModal
{
    public class EmployeeViewModel(EmployeeManager empl, LoginViewModel login) : ViewModelBase
    {

        public async Task InitializeAsync()
        {
            await login.InitializeAsync();
            DtoResult<DtoEmployee> rs = await empl.GetAllAsync();
            if (rs.Succeed && rs.ResultList !=null  ) 
            {
                EmplList = rs.ResultList;
            }
        }
        public List<DtoEmployee> EmplList { get; set; } = [];//danh sách đối tượng

        public DtoEmployee SelectedEmpl { get; set; } = new();//đối tượng đang xử lý
        public DtoLoginInfo SelectedLogin { get; set; } = new();
        public string Message { get; set; } = ""; //thông báo
        public async Task SaveCust()
        {

            DtoResult<DtoEmployee> ketQua = new();
            //kiêm tra trùng tài khoản login
            FullLogin? check = login.LoginList.FirstOrDefault(x => x.UserID == SelectedEmpl.EmployeeCode);
            if (SelectedEmpl.Id == null) //thêm mới
            {
                if (check != null) //kiểm tra trùng mã đăng nhập
                {
                    Message = $"Tên đăng nhập {SelectedEmpl.EmployeeCode} đã có";
                    OnPropertyChanged();
                    return;
                }
                SelectedLogin.UserID = SelectedEmpl.EmployeeCode;
                ketQua = await empl.AddAsync(SelectedEmpl, SelectedLogin);
            }
            else //cập nhật
            {
                if (check == null) //khi cập nhật thì bắt buộc dò tìm ra kết quả
                {
                    Message = $"Tên đăng nhập {SelectedEmpl.EmployeeCode} không tồn tại";
                    OnPropertyChanged();
                    return;
                }
                //nếu là cập nhật thì không cần chính xác login info
                ketQua = await empl.UpdateAsync(SelectedEmpl);
            }
            if (!ketQua.Succeed && ketQua.Message != null)
            {
                Message = ketQua.Message;
                return;
            }
            if (ketQua.Succeed && ketQua.Result != null)
            {
                await login.InitializeAsync();
                if (SelectedEmpl.Id == null) //thêm mới => thêm dữ liệu vào danh sách
                    EmplList.Add(SelectedEmpl);
                else
                {
                    //dùng linq để cập nhật lại giá trị của cust
                    DtoEmployee? update = EmplList.FirstOrDefault(x => x.Id == ketQua.Result.Id);
                    if (update != null)
                    {
                        update.FullName = ketQua.Result.FullName;
                        update.Email = ketQua.Result.Email;
                        update.EmployeeCode = ketQua.Result.EmployeeCode;

                    }
                }
                SelectedEmpl = new();
                SelectedLogin = new();
            }

            Message = ketQua.Succeed ? "Lưu khách hàng thành công!" : "Lưu khách hàng thất bại!";

            OnPropertyChanged();
        }

    }
}