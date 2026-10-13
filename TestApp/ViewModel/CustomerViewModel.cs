using Application.Asset.Common;
using Application.Asset.Dtos;
using Application.Asset.Interfaces;



namespace ViewModel
{
    public class CustomerViewModel(IService<DtoCustomer> cust,  IService<DtoLoginInfo> login):ViewModelBase
    {

        public async Task InitializeAsync()
        {
            await LoadUserList();
            DtoResult<DtoCustomer> rs = await cust.GetAllAsync();
            if (!IsValidResult(rs,DataMode.Multi))
                return;
            RootList = [..rs.ResultList.Select(x=> new DtoCustomer {
                    Address = x.Address,
                    CustomerCode = x.CustomerCode,
                    CustomerName = x.CustomerName,
                    id = x.id,
                    Phone = x.Phone,
                    TaxCode = x.TaxCode,
                })];
            CustList = [..rs.ResultList.Select(x=> new DtoCustomer {
                    Address = x.Address,
                    CustomerCode = x.CustomerCode,
                    CustomerName = x.CustomerName,
                    id = x.id,
                    Phone = x.Phone,
                    TaxCode = x.TaxCode,
                })];
            OnPropertyChanged();
        }

        private async Task LoadUserList()
        {
            DtoResult<DtoLoginInfo> rsLogin = await login.GetAllAsync();
            if (!IsValidResult(rsLogin, DataMode.Multi))
            {
                return;
            }
            UserList = rsLogin.ResultList!;
        }

        public List<DtoLoginInfo> UserList { get; set; } = [];
        private List<DtoCustomer> RootList = [];
        public List<DtoCustomer> CustList { get; set; } = [];

        string searchText = "";
        public string SearchText 
        {
            get => searchText;
            set
            {
                searchText = value;
                if (!string.IsNullOrEmpty(searchText))
                {
                    CustList = [.. RootList.Where(x =>
                    x.CustomerCode!.Contains(searchText, StringComparison.CurrentCultureIgnoreCase) ||
                    x.CustomerName!.Contains(searchText, StringComparison.CurrentCultureIgnoreCase) ||
                    x.Phone!.Contains(searchText, StringComparison.CurrentCultureIgnoreCase)
                    )];
                }
                else
                    CustList = [..RootList.Select(x=> new DtoCustomer {
                    Address = x.Address,
                    CustomerCode = x.CustomerCode,
                    CustomerName = x.CustomerName,
                    id = x.id,
                    Phone = x.Phone,
                    TaxCode = x.TaxCode,
                })];
                OnPropertyChanged();
            }
        }


        public DtoCustomer SelectedCust { get; set; } = new();
        public DtoLoginInfo SelectedLogin { get; set; } = new();

        //string mess = "";

        public async Task SaveCust()
        {

            DtoResult<DtoCustomer> ketQua = new();
            //kiêm tra trùng tài khoản login
            DtoLoginInfo? check = UserList.FirstOrDefault(x=>x.UserID == SelectedCust.CustomerCode);
            if (SelectedCust.id == null) //thêm mới
            {
                if (check != null) //kiểm tra trùng mã đăng nhập
                {
                    ErrMess = $"Tên đăng nhập {SelectedCust.CustomerCode} đã có";
                    OnPropertyChanged();
                    return;
                }
                SelectedLogin.UserID = SelectedCust.CustomerCode;
                ketQua = await cust.AddAsync(SelectedCust);
            }
            else //cập nhật
            {
                if (check == null) //khi cập nhật thì bắt buộc dò tìm ra kết quả
                {
                    ErrMess = $"Tên đăng nhập {SelectedCust.CustomerCode} không tồn tại";
                    OnPropertyChanged();
                    return;
                }
                //nếu là cập nhật thì không cần chính xác login info
                ketQua = await cust.UpdateAsync(SelectedCust);
            }    

            if (!IsValidResult(ketQua, DataMode.Single))
                return;
            DtoResult<DtoLoginInfo> rs;
            SelectedLogin.UserID = SelectedCust.CustomerCode;
            if (SelectedCust.id == null)
            {
                rs = await login.AddAsync(SelectedLogin);
            }    
            else
            {
                rs=await login.UpdateAsync(SelectedLogin);
            }    
            if (!IsValidResult(rs, DataMode.Single))
            {
                return;
            }

            //await LoadUserList();
            if (SelectedCust.id == null) //thêm mới => thêm dữ liệu vào danh sách
            {
                CustList.Add(ketQua.Result!);
                UserList.Add(rs.Result!);
            }
            else
            {
                //dùng linq để cập nhật lại giá trị của cust
                DtoCustomer? update = CustList.FirstOrDefault(x => x.id == ketQua.Result.id);
                if (update != null)
                {
                    update.Address = ketQua.Result.Address;
                    update.CustomerName = ketQua.Result.CustomerName;
                    update.CustomerCode = ketQua.Result.CustomerCode;
                    update.Phone = ketQua.Result.Phone;
                    update.TaxCode = ketQua.Result.TaxCode;
                }
                DtoLoginInfo? info = UserList.FirstOrDefault(x => x.id == rs.Result.id);
                if (info != null)
                {
                    info.PassWord = rs.Result.PassWord;

                }
            }                
            SelectedCust = new();
            SelectedLogin = new();

            ErrMess = ketQua.Succeed ? "Lưu khách hàng thành công!" : "Lưu khách hàng thất bại!";

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
