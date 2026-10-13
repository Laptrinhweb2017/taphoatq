using Application.Asset.Common;
using Application.Assets.FullDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel
{
    public class UserInfoViewModel : ViewModelBase
    {
        public FullLogin ChangUPass { get; set; }
        public string? OldPass { get; set; }
        public string? NewPass { get; set; }
        public string? ConfirmPass { get; set; }
        public FullLogin SelectedStaff { get; set; }

        public async Task UpdatePass()
        {
           // throw new NotImplementedException();
        }
    }
}
