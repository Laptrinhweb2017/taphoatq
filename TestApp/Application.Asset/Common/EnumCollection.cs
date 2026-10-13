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
        Approved = 0,
        Rejected = 1,
        InLate = 2,
        OutSoon = 3,
        WrongCheckPoint = 4,
        FakeGPS = 5,
        OverTime = 6,
        ForgetOut = 7,
        NotCheckOut = 8
    }
    public enum LeaveStatus
    {
        NeedApprove = 0,
        Approved = 1,
        Rejected = 2,
        Broke = 3,
    }

    public enum WorkDayStatus : byte
    {
        None = 0,
        Off = 1,
        Approved = 2,
        Reject = 3,
        NeedCheck = 4,
        OverTime = 5,
        Leave = 6,
        NotOut = 7
    }
}
