using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Assets.Dtos
{
    public class DtoPaymentDetail
    {
        public int? Id {  get; set; }

        public int? PaymentId { get; set; }

        public DateTime? PaymentDate { get; set; }

        public string? PaymentMethod { get; set; }

        public decimal? Amount { get; set; }
    }
}
