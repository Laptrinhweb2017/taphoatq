namespace Application.Asset.Dtos
{
	public record DtoOrders
	{
		public int? id { get; set; }
		public string? OrderCode { get; set; }
		public int? CustomerId { get; set; }
		public DateTime? OrderDate { get; set; }
		public int? Status { get; set; }

		public static readonly Dictionary<string, string> TypeList = new()
		{
			{ "id", "int" },
			{ "OrderCode", "varchar" },
			{ "CustomerId", "int" },
			{ "OrderDate", "datetime" },
			{ "Status", "int" }
		};
	}
}