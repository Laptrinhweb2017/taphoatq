using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Application.Asset.Common
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string PropName = null!)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropName));
        }
        string errmess = "";
        public string ErrMess { get => errmess; set { errmess = value; OnPropertyChanged(); } }

        public bool IsValidResult(dynamic res, DataMode IsList)
        {
            if (res == null)
            {
                ErrMess = "Mất kết nối máy chủ";
                OnPropertyChanged();
                return false;
            }
            else
            {
                if (res.Message != "OK")
                {
                    ErrMess = res.Message;
                    OnPropertyChanged();
                    return false;
                }
                if ((IsList==DataMode.Single && res.Result == null) || (IsList == DataMode.Multi && res.ResultList==null))
                {
                    ErrMess = "Lỗi, mất kết nối với máy chủ";
                    OnPropertyChanged();
                    return false;
                }
                if(IsList==DataMode.Delete && res.Succeed == false)
                {
                    ErrMess = "Lỗi, Không xóa được dữ liệu";
                    OnPropertyChanged();
                    return false;
                }    

            }
            return true;
        }
    }
    public enum DataMode { Single, Multi, Delete }
}
