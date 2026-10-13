namespace Application.Asset.Dtos
{
	public record DtoStockOutput
	{
		public int? id { get; set; }
		public string? OutputCode { get; set; }
		public DateTime? OutputDate { get; set; }
		public int? EmployeeId { get; set; }
		public int? OrderId { get; set; }

		public static readonly Dictionary<string, string> TypeList = new()
		{
			{ "id", "int" },
			{ "OutputCode", "varchar" },
			{ "OutputDate", "datetime" },
			{ "EmployeeId", "int" },
			{ "OrderId", "int" }
		};
	}
}