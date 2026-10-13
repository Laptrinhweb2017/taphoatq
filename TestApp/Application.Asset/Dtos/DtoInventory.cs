namespace Application.Asset.Dtos
{
	public record DtoInventory
	{
		public int? id { get; set; }
		public int? WarehouseId { get; set; }
		public int? ProductId { get; set; }
		public int? Quantity { get; set; }

		public static readonly Dictionary<string, string> TypeList = new()
		{
			{ "id", "int" },
			{ "WarehouseId", "int" },
			{ "ProductId", "int" },
			{ "Quantity", "int" }
		};
	}
}