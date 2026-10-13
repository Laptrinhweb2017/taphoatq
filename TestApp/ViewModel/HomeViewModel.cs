using Application.Asset.Common;
using Application.Asset.ViewDtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModel
{
    public class HomeViewModel:ViewModelBase
    {

        public async Task InitializeAsync()
        {

        }

        public string ErrMess { get; set; }

        public List<PendingItems> PendingItems { get; set; }

        public List<RecentActivities> RecentActivities { get; set; }

    }
}
