using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Assets.Dto
{
    public class DtoPayment
    {
        public int? Id { get; set; }

        public int? OrderId { get; set; }

        public decimal? TotalAmount { get; set; }

        public decimal? RemainingDebt { get; set; }
    }
}
