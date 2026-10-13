namespace Application.Asset.Dtos
{
	public record DtoBrand
	{
		public int? id { get; set; }
		public string? BrandName { get; set; }

		public static readonly Dictionary<string, string> TypeList = new()
		{
			{ "id", "int" },
			{ "BrandName", "nvarchar" }
		};
	}
}