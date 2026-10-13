namespace Application.Asset.Common;

public class DayStatus
{
    public DateTime OnDate { get; set; }

    public List<WorkDayStatus> Statuses { get; set; } = [];
}
