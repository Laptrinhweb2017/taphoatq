using Application.Assets.Common;
using Application.Assets.Dto;
using Application.Assets.FullDto;
using Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModal
{
    public class LoginViewModel(LoginInfoService service):ViewModelBase
    {
        bool _inited = false;
        public async Task InitializeAsync()
        {
            //if (_inited)
            //    return;
            //DtoResult<FullLogin> result = await service.GetLogin();// Lấy danh sách người dùng từ service
            //if (result.Succeed && result.ResultList != null)// Kiểm tra kết quả trả về từ service
            //{
            //    LoginList = result.ResultList;// Lưu danh sách người dùng hiện tại
            //    _inited = true;
            //    NotifyStateChanged();
            //}
            //else
            //{
            //    LoginList = [];// Nếu không có kết quả, khởi tạo danh sách người dùng rỗng
            //    ErrMess = "Có lỗi xảy ra. Không lấy được dữ liệu";
            //    NotifyStateChanged();
            //    return;
            //}
        }
        public bool LoggedIn { get; set; }
        public string? ErrMess { get; set; }
        public List<FullLogin> LoginList { get; set; } = [];
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
        private string CanView = "";
        //public async Task GetLogin(string StaffID)
        //{
            
            
        //    NotifyStateChanged();
        //}
        

        
        public async Task<bool> Login(FullLogin user)// Phương thức đăng nhập
        {
            if (user == null || string.IsNullOrEmpty(user.UserID))
            {
                ErrMess = "Chưa nhập tài khoản";
                //LoggedIn = false;
                NotifyStateChanged();
                return false;
            }
            if (string.IsNullOrEmpty(user.Password))
            {
                ErrMess = "Chưa nhập mật khẩu";
                NotifyStateChanged();
                return false;
            }
            DtoResult<FullLogin> result = await service.GetLogin();
            if (result.Succeed && result.ResultList != null)// Kiểm tra kết quả trả về từ service
            {
                LoginList = result.ResultList;// Lưu danh sách người dùng hiện tại
                _inited = true;
                NotifyStateChanged();
            }
            else
            {
                LoginList = [];// Nếu không có kết quả, khởi tạo danh sách người dùng rỗng
                ErrMess = "Có lỗi xảy ra. Không lấy được dữ liệu";
                NotifyStateChanged();
                return false;
            }
            LoggedUser = LoginList.FirstOrDefault(x => x.UserID!.Equals(user.UserID, StringComparison.OrdinalIgnoreCase)); // Tìm kiếm người dùng theo UsersID
            if (!string.IsNullOrEmpty(ErrMess))
            {
                return false;
            }
            if (LoggedUser == null)
            {
                ErrMess = "Tài khoản không tồn tại";
                NotifyStateChanged();
                return false;
            }
            else
            {
                if (LoggedUser.Password != user.Password)
                {
                    ErrMess = "Sai mật khẩu";
                    NotifyStateChanged();
                    return false;
                }
            }
            NotifyStateChanged();
            return true;
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
