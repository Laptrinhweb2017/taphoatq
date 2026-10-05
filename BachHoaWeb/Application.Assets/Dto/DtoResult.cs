using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Assets.Dto
{
    public class DtoResult<A>
    {
        public List<A>? ResultList { get; set; }
        public A? Result { get; set; }
        public string? Message { get; set; }
        public bool Succeed { get; set; } = false;
    }
}
