namespace Application.Asset.Dtos
{
	public record DtoPaymentDetail
	{
		public int? id { get; set; }
		public int? PaymentId { get; set; }
		public DateTime? PaymentDate { get; set; }
		public int? PaymentMethod { get; set; }
		public decimal? Amount { get; set; }

		public static readonly Dictionary<string, string> TypeList = new()
		{
			{ "id", "int" },
			{ "PaymentId", "int" },
			{ "PaymentDate", "datetime" },
			{ "PaymentMethod", "int" },
			{ "Amount", "decimal" }
		};
	}
}