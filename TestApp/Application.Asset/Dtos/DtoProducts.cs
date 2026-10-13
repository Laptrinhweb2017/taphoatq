namespace Application.Asset.Dtos
{
	public record DtoProducts
	{
		public int? id { get; set; }
		public string? ProductCode { get; set; }
		public string? ProductName { get; set; }
		public string? Image { get; set; }
		public string? Descript { get; set; }
		public DateTime? CreatedAt { get; set; }
		public int? BrandID { get; set; }
		public int? CateID { get; set; }

		public static readonly Dictionary<string, string> TypeList = new()
		{
			{ "id", "int" },
			{ "ProductCode", "varchar" },
			{ "ProductName", "nvarchar" },
			{ "Image", "varchar" },
			{ "Descript", "nvarchar" },
			{ "CreatedAt", "datetime" },
			{ "BrandID", "int" },
			{ "CateID", "int" }
		};
	}
}