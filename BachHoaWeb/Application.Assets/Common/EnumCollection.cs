using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Asset.Common
{
    internal class EnumCollection
    {
    }

    public enum SheetStatus
    {
        NeedCheck,
        AutoApproved,
        Rejected,
        Approved,
        Forget,
        OverTime,
        FakeGPS
    }

    public enum DialogType
    {
        OkOnly,
        OkCancel,
        YesNo,
        YesNoCancel
    }

    public enum DialogResult
    {
        None,
        Ok,
        Cancel,
        Yes,
        No
    }

    public enum DialogIcon
    {
        None,
        Warning,
        Question,
        Info,
        Success
    }
    public enum WorkDayStatus : byte
    {
        None = 0,
        Off = 1,
        Approved = 2,
        Reject = 3,
        NeedCheck = 4,
        OverTime = 5,
        Leave = 6
    }
}
