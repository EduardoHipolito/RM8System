using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Business.Request
{
    public class RequestCollectionSetting
    {
        public int Start { get; set; }
        public int Length { get; set; }
        public RequestCollectionOrder Order { get; set; }

        public RequestCollectionSetting()
        {
            this.Start = 0;
            this.Length = 10;
            this.Order = new RequestCollectionOrder();
        }
    }
}
