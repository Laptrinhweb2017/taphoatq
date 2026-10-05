using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Assets.Dto
{
    public class DtoProduct
    {
        public int? Id { get; set; }
        public string? ProductCode { get; set; }
        public string? ProductName { get; set; }
        public string? Image { get; set; }
        public string? Descript { get; set; }
        public DateTime? CreatedAt { get; set; }
        public int? BrandId { get; set; }
        public int? CateId { get; set; }

    }
}
