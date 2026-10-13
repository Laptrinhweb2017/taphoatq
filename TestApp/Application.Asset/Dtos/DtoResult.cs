namespace Application.Asset.Dtos
{
	public class DtoResult<T>
	{
		public T? Result { get; set; }
		public List<T>? ResultList { get; set; }
		public string Message { get; set; } = "OK";
        public bool Succeed { get; set; }
    }
}