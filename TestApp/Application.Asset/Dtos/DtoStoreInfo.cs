namespace Application.Asset.Dtos
{
	public record DtoStoreInfo
	{
		public int? id { get; set; }
		public string? StoreName { get; set; }
		public string? Address { get; set; }
		public string? TaxCode { get; set; }
		public string? Phone { get; set; }

		public static readonly Dictionary<string, string> TypeList = new()
		{
			{ "id", "int" },
			{ "StoreName", "nvarchar" },
			{ "Address", "nvarchar" },
			{ "TaxCode", "varchar" },
			{ "Phone", "varchar" }
		};
	}
}