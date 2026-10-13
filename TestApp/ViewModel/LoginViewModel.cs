using Application.Asset.Common;
using Application.Asset.Dtos;
//using Application.Asset.FullDto;
using Application.Asset.Interfaces;
using Application.Assets.FullDto;
using ServiceLib.Services;

namespace ViewModel
{
    public class LoginViewModel(LoginService staffService):ViewModelBase
    {
        public async Task InitializeAsync()
        {
            DtoResult<FullLogin> rs = await staffService.GetAllLogin();
            if (rs == null || rs.ResultList == null || rs.Message!="OK")
            {
                ErrMess = "Lỗi, Mất kết nối máy chủ, không thể đăng nhập";
                if (rs != null && rs.Message != "OK")
                    ErrMess = rs.Message;
                OnPropertyChanged();
                return;
            }
            else
                UserList = rs.ResultList;
        }
        public bool LoggedIn { get; set; }
        public List<FullLogin> UserList { get; set; } = [];
        FullLogin? loggedUser = new();
        public FullLogin? LoggedUser 
        { 
            get => loggedUser;
            set
            {
                loggedUser = value;
                NotifyStateChanged();
            }
        }
        public async Task<FullLogin?> GetLogin(string StaffID) => UserList.FirstOrDefault(x=>x.UserID.Equals(StaffID, StringComparison.OrdinalIgnoreCase));
        
        public async Task<string> Login(string StaffID, string Pass)
        {
            LoggedUser = await GetLogin(StaffID);
            if (LoggedUser == null)
                return "Tài khoản không tồn tại";
            else
            {
                if (LoggedUser.Password != Pass)
                    return "Mật khẩu không đúng";
                else
                    return "Đăng nhập thành công";
            }    

        }

        public void Logout()
        {
            LoggedIn = false;
            LoggedUser = new();
            NotifyStateChanged();
        }

        public event Action? OnChange;
        private void NotifyStateChanged() => OnChange?.Invoke();

    }
}
