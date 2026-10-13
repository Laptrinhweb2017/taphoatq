namespace Application.Asset.Common;
public class WorkDays
{
    public int TotalWorkDays { get; set; }
    public DateTime Month { get; set; }

    public List<DayStatus> Days { get; set; } = new();
}