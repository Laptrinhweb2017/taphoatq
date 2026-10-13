namespace Application.Asset.Dtos
{
	public record DtoTrackingStatus
	{
		public int? id { get; set; }
		public string? StatusName { get; set; }

		public static readonly Dictionary<string, string> TypeList = new()
		{
			{ "id", "int" },
			{ "StatusName", "nvarchar" }
		};
	}
}