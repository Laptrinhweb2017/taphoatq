namespace Application.Asset.Dtos
{
	public record DtoCustomer
	{
		public int? id { get; set; }
		public string? CustomerCode { get; set; }
		public string? CustomerName { get; set; }
		public string? TaxCode { get; set; }
		public string? Phone { get; set; }
		public string? Address { get; set; }

		public static readonly Dictionary<string, string> TypeList = new()
		{
			{ "id", "int" },
			{ "CustomerCode", "varchar" },
			{ "CustomerName", "nvarchar" },
			{ "TaxCode", "varchar" },
			{ "Phone", "varchar" },
			{ "Address", "nvarchar" }
		};
	}
}