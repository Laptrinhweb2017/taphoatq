namespace Application.Asset.Dtos
{
	public record DtoEmployee
	{
		public int? id { get; set; }
		public string? FullName { get; set; }
		public string? Email { get; set; }
		public string? EmployeeCode { get; set; }

		public static readonly Dictionary<string, string> TypeList = new()
		{
			{ "id", "int" },
			{ "FullName", "nvarchar" },
			{ "Email", "varchar" },
			{ "EmloyeeCode", "varchar" }
		};
	}
}