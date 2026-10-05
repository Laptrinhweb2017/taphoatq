using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Assets.Dto
{
    public class DtoInventory
    {
        public int? Id { get; set; }

        public int? WarehouseId { get; set; }

        public int? ProductId { get; set; }

        public int? Quantity { get; set; }
    }
}
