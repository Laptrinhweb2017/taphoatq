using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Assets.Dto
{
    public class DtoPOTracking
    {
        public int? Id { get; set; }

        public int? OrderId { get; set; }

        public int? StatusId { get; set; }

        public DateTime? TrackedAt { get; set; }
    }
}
