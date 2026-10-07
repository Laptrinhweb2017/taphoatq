using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Assets.Dtos
{
    public class DtoStockOutput
    {
        public int? Id { get; set; }

        public string? OutputCode { get; set; }

        public DateTime OutputDate { get; set; }

        public int? EmployeeId { get; set; }

        public int? OrderId { get; set; }
    }
}
