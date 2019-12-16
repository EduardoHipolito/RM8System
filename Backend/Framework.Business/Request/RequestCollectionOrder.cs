using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Business.Request
{
    public class RequestCollectionOrder
    {
        public int Column { get; set; }

        public OrderType Direction { get; set; }
    }
    public enum OrderType
    {
        Ascending = 1,
        Descending = 2
    }
}
