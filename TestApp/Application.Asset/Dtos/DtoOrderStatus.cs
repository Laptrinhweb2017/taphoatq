namespace Application.Asset.Dtos
{
	public record DtoOrderStatus
	{
		public int? id { get; set; }
		public string? Descript { get; set; }

		public static readonly Dictionary<string, string> TypeList = new()
		{
			{ "id", "int" },
			{ "Descript", "nvarchar" }
		};
	}
}