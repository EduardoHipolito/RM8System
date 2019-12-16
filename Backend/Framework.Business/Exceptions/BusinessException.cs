using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Business.Exceptions
{
    public class BusinessException : BaseException
    {
        public BusinessException(string message, params object[] parameters)
            : base(message, parameters)
        {
            base._isMessageFriendly = true;
        }
    }
}
