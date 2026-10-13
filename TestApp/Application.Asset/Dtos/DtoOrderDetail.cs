namespace Application.Asset.Dtos
{
	public record DtoOrderDetail
	{
		public int? id { get; set; }
		public int? OrderId { get; set; }
		public int? ProductId { get; set; }
		public int? Quantity { get; set; }
		public decimal? UnitPrice { get; set; }

		public static readonly Dictionary<string, string> TypeList = new()
		{
			{ "id", "int" },
			{ "OrderId", "int" },
			{ "ProductId", "int" },
			{ "Quantity", "int" },
			{ "UnitPrice", "decimal" }
		};
	}
}