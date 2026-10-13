using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Asset.ViewDtos
{
    public class ViewDto<T>
    {
        public string? ID { get; set; }
        public string? DisplayText { get; set; }
        public List<ViewDto<T>> Children { get; set; } = [];
        public List<T> Data { get; set; } = [];
    }
}
