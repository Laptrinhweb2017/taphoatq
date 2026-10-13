using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Assets.FullDto
{
    public class FullLogin
    {
        public int? id { get; set; }
        [Required(ErrorMessage = "Cân nhập tài khoản")]
        [MaxLength(12, ErrorMessage = "Chiều dài tối đa 12 ký tự")]
        public string? UserID { get; set; }
        [Required(ErrorMessage = "Cần nhập mật khẩu")]
        public string?  Password { get; set; }
        public int? UID { get; set; }
        public string? UType { get; set; }
        public string? LoginName { get; set; }
    }
}
