using Application.Assets.Common;
using Application.Assets.Dto;
using Application.Assets.FullDto;
using Business;
using Services;


namespace ViewModal
{
    public class CustomerViewModel(CustomerManager cust, LoginViewModel login):ViewModelBase
    {

        public async Task InitializeAsync()
        {
            await login.InitializeAsync();
            DtoResult<DtoCustomer> rs = await cust.GetAllAsync();
            if (rs.Succeed && rs.ResultList != null)
            {
                CustList = rs.ResultList;
            }
        }

        public List<DtoCustomer> CustList { get; set; } = [];


        public DtoCustomer SelectedCust { get; set; } = new();
        public DtoLoginInfo SelectedLogin { get; set; } = new();

        //string mess = "";
        public string Message { get; set; } = "";

        public async Task SaveCust()
        {

            DtoResult<DtoCustomer> ketQua = new();
            //kiêm tra trùng tài khoản login
            FullLogin? check = login.LoginList.FirstOrDefault(x=>x.UserID == SelectedCust.CustomerCode);
            if (SelectedCust.Id == null) //thêm mới
            {
                if (check != null) //kiểm tra trùng mã đăng nhập
                {
                    Message = $"Tên đăng nhập {SelectedCust.CustomerCode} đã có";
                    OnPropertyChanged();
                    return;
                }
                SelectedLogin.UserID = SelectedCust.CustomerCode;
                ketQua = await cust.AddAsync(SelectedCust, SelectedLogin);
            }
            else //cập nhật
            {
                if (check == null) //khi cập nhật thì bắt buộc dò tìm ra kết quả
                {
                    Message = $"Tên đăng nhập {SelectedCust.CustomerCode} không tồn tại";
                    OnPropertyChanged();
                    return;
                }
                //nếu là cập nhật thì không cần chính xác login info
                ketQua = await cust.UpdateAsync(SelectedCust);
            }    
            if (!ketQua.Succeed && ketQua.Message != null)
            {
                Message = ketQua.Message;
                return;
            }
            if (ketQua.Succeed && ketQua.Result!=null)
            {
                await login.InitializeAsync();
                if(SelectedCust.Id==null) //thêm mới => thêm dữ liệu vào danh sách
                    CustList.Add(SelectedCust);
                else
                {
                    //dùng linq để cập nhật lại giá trị của cust
                    DtoCustomer? update = CustList.FirstOrDefault(x => x.Id == ketQua.Result.Id);
                    if(update != null)
                    {
                        update.Address = ketQua.Result.Address;
                        update.CustomerName = ketQua.Result.CustomerName;
                        update.CustomerCode = ketQua.Result.CustomerCode;
                        update.Phone = ketQua.Result.Phone;
                        update.TaxCode = ketQua.Result.TaxCode;
                    }
                }                
                SelectedCust = new();
                SelectedLogin = new();
            }

            Message = ketQua.Succeed ? "Lưu khách hàng thành công!" : "Lưu khách hàng thất bại!";

            OnPropertyChanged();



        }

        //public async Task DeleteCust(object? id)
        //{
        //    if (id == null)
        //    {
        //        Message = "Không tìm thấy ID để xóa!";
        //        return;
        //    }

        //    // Gọi xuống CustomerManager để thực thi Delete theo ID
        //    DtoResult<DtoCustomer> ketQua = await cust.DeleteAsync(id);

        //    Message = ketQua.Succeed ? "Xóa khách hàng thành công!" : "Xóa khách hàng thất bại!";

        //    if (ketQua.Succeed)
        //    {
        //        await InitializeAsync();
        //    }
        //}
    }
}
