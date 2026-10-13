namespace Application.Asset.Dtos
{
	public record DtoWarehouses
	{
		public int? id { get; set; }
		public string? WarehouseName { get; set; }
		public string? Address { get; set; }

		public static readonly Dictionary<string, string> TypeList = new()
		{
			{ "id", "int" },
			{ "WarehouseName", "nvarchar" },
			{ "Address", "nvarchar" }
		};
	}
}