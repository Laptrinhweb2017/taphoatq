namespace Application.Asset.Dtos
{
	public record DtoPaymentMethod
	{
		public int? id { get; set; }
		public string? PayMethod { get; set; }

		public static readonly Dictionary<string, string> TypeList = new()
		{
			{ "id", "int" },
			{ "PayMethod", "nvarchar" }
		};
	}
}