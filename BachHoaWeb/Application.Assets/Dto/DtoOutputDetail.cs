using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Assets.Dto
{
    public class DtoOutputDetail
    {
        public int? Id { get; set; }

        public int? OutputId { get; set; }

        public int? ProductId { get; set; }

        public int? Quantity { get; set; }

        public decimal UnitPrice { get; set; }
    }
}
