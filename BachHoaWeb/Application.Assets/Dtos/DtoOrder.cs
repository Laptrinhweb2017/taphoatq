using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Assets.Dtos
{
    public class DtoOrder
    {
        public int? Id { get; set; }

        public string? OrderCode { get; set; }

        public int? CustomerId { get; set; }

        public DateTime? OrderDate { get; set; }

        public string? Status { get; set; }
    }
}
