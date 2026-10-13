namespace Application.Asset.Dtos
{
	public record DtoPOTracking
	{
		public int? id { get; set; }
		public int? OrderId { get; set; }
		public int? StatusId { get; set; }
		public DateTime? TrackedAt { get; set; }

		public static readonly Dictionary<string, string> TypeList = new()
		{
			{ "id", "int" },
			{ "OrderId", "int" },
			{ "StatusId", "int" },
			{ "TrackedAt", "datetime" }
		};
	}
}