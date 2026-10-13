namespace Application.Asset.Dtos
{
	public record DtoPayment
	{
		public int? id { get; set; }
		public int? OrderId { get; set; }
		public decimal? TotalAmount { get; set; }
		public decimal? RemainingDebt { get; set; }

		public static readonly Dictionary<string, string> TypeList = new()
		{
			{ "id", "int" },
			{ "OrderId", "int" },
			{ "TotalAmount", "decimal" },
			{ "RemainingDebt", "decimal" }
		};
	}
}