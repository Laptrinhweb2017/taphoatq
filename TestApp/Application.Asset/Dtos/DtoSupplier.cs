namespace Application.Asset.Dtos
{
	public record DtoSupplier
	{
		public int? id { get; set; }
		public string? SupplierCode { get; set; }
		public string? SupplierName { get; set; }
		public string? Phone { get; set; }
		public string? Address { get; set; }

		public static readonly Dictionary<string, string> TypeList = new()
		{
			{ "id", "int" },
			{ "SupplierCode", "varchar" },
			{ "SupplierName", "nvarchar" },
			{ "Phone", "varchar" },
			{ "Address", "nvarchar" }
		};
	}
}