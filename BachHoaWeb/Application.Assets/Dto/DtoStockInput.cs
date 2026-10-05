using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Assets.Dto
{
    public class DtoStockInput
    {
        public int? Id { get; set; }

        public string? ReceiptCode { get; set; }

        public DateTime? ReceivedDate { get; set; }

        public int? SupplierId
        {
            get; set;
        }

        public int? EmployeeId { get; set; }
}
}
