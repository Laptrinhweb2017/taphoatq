using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Asset.ViewDtos
{
    public class HeaderInfo
    {
        public string HeadTitle { get; set; } = "";
        public string LogingName { get; set; } = "";
        public event Action OnChange;
        private void NotifyStateChanged() => OnChange?.Invoke();

        public void SetTile(string title)
        {
            HeadTitle = title;
            NotifyStateChanged();
        }
        public void SetUser(string userName)
        {
            LogingName = userName;
            NotifyStateChanged();
        }
    }
}
