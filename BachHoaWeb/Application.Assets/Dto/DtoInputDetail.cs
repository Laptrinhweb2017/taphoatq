using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Assets.Dto
{
    public class DtoInputDetail
    {
        public int? Id { get; set; }

        public int? InputId { get; set; }

        public int? ProductId { get; set; }

        public int? Quantity { get; set; }

        public decimal? UnitPrice { get; set; }
    }
}
