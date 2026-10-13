namespace Application.Asset.Dtos
{
	public record DtoCategory
	{
		public int? id { get; set; }
		public string? CateName { get; set; }

		public static readonly Dictionary<string, string> TypeList = new()
		{
			{ "id", "int" },
			{ "CateName", "nvarchar" }
		};
	}
}