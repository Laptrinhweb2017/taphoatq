namespace Application.Asset.Dtos
{
	public record DtoLoginInfo
	{
		public int? id { get; set; }
		public string? UserID { get; set; }
		public string? PassWord { get; set; }

		public static readonly Dictionary<string, string> TypeList = new()
		{
			{ "id", "int" },
			{ "UserID", "varchar" },
			{ "PassWord", "varchar" }
		};
	}
}