namespace Application.Asset.Dtos
{
	public record DtoPrice
	{
		public int? id { get; set; }
		public int? ProdID { get; set; }
		public decimal? Price { get; set; }
		public DateTime? AppliedDate { get; set; }

		public static readonly Dictionary<string, string> TypeList = new()
		{
			{ "id", "int" },
			{ "ProdID", "int" },
			{ "Price", "decimal" },
			{ "AppliedDate", "datetime" }
		};
	}
}