using System;

public class DateInputState
{
    public int? Day { get; set; }
    public int? Month { get; set; }
    public int? Year { get; set; }

    public int? Hour { get; set; }
    public int? Minute { get; set; }
    public int? Second { get; set; }

    public bool IsInvalid { get; set; }
    public string? Hint { get; set; }

    public bool IsCompleteDate => Day.HasValue && Month.HasValue && Year.HasValue;
    public bool IsCompleteTime => Hour.HasValue && Minute.HasValue && Second.HasValue;

    // Hàm chuẩn hóa thông minh: Giữ ngày hợp lệ tối đa của tháng hiện tại
    public void NormalizeSmartDate()
    {
        if (!Month.HasValue)
            return;

        // Giới hạn tháng từ 1 - 12
        if (Month < 1)
            Month = 1;
        if (Month > 12)
            Month = 12;

        if (Day.HasValue)
        {
            if (Day < 1)
                Day = 1;

            // Lấy năm hiện tại nếu chưa nhập năm để tính số ngày tối đa của tháng
            int checkYear = Year ?? DateTime.Today.Year;
            int maxDays = DateTime.DaysInMonth(checkYear, Month.Value);

            if (Day > maxDays)
            {
                Day = maxDays; // Tự động đưa về ngày tối đa (ví dụ 31 -> 29/02/2020)
                Hint = $"Tự động điều chỉnh về ngày tối đa của tháng {Month} ({maxDays} ngày).";
            }
        }
    }
}
