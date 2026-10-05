namespace MyControl
{
    public class MenuItem
    {
        public string MenuText { get; set; } = "Item";
        public string? LinkToPage { get; set; }
        public bool Checked { get; set; }
        public string? Icon { get; set; }
        public string UncheckStyle { get; set; } = "";
        public string CheckStyle { get; set; } = "";
        public bool Enabled { get; set; } = false;
        public string? IDPermission { get; set; }
        public List<MenuItem>? SubItems { get; set; }
    }
}
