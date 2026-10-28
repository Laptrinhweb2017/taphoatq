namespace Application.Asset.Dtos //Định danh vị trí file nằm trong thư mục Dtos của projec Application.Asset
 // Khi các tầng(ModelLib,ServiceLib) muốn dùng DtoCustomer, thì khai báo using Application.Asset.Dtos;
{
    public record DtoCustomer //khai báo DtoCustomer kiểu record thay vì class thông thường, kiểu record trong C# được thiết kế tối ưu cho các lớp DTO/Data Model, hiệu năng cao và tiết kiệm bộ nhớ khi truyền qua lại giữa các tầng
    {
		public int? id { get; set; } //cột id của khách hàng là khóa chính,'?'cho phép nhận gtrị rỗng(null)
		public string? CustomerCode { get; set; } //cột mã khách hàng
		public string? CustomerName { get; set; } //cột tên khách hàng
		public string? TaxCode { get; set; } //cột mã số thuế
		public string? Phone { get; set; } //cột điện thoại
		public string? Address { get; set; } //cột địa chỉ
		//get: cho phép các tầng khác đọc giá trị của thuộc tính.
		//set: cho phép các tầng khác gán/thay đổi giá trị cho thuộc tính.

        public static readonly Dictionary<string, string> TypeList = new()
		{
			{ "id", "int" },
			{ "CustomerCode", "varchar" },
			{ "CustomerName", "nvarchar" },
			{ "TaxCode", "varchar" },
			{ "Phone", "varchar" },
			{ "Address", "nvarchar" }
		};
	}
}