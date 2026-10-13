namespace Application.Asset.Dtos
{
	public record DtoStockInput
	{
		public int? id { get; set; }
		public string? ReceiptCode { get; set; }
		public DateTime? ReceivedDate { get; set; }
		public int? SupplierId { get; set; }
		public int? EmployeeId { get; set; }

		public static readonly Dictionary<string, string> TypeList = new()
		{
			{ "id", "int" },
			{ "ReceiptCode", "varchar" },
			{ "ReceivedDate", "datetime" },
			{ "SupplierId", "int" },
			{ "EmployeeId", "int" }
		};
	}
}