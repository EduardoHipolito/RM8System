using System;
using System.Collections.Generic;
using System.Text;

namespace Framework.Business.Exceptions
{
    public class BaseException : Exception
    {
        private object[] _parameters;
        private string _methodName;
        protected bool _isMessageFriendly;

        public BaseException()
        {

        }

        public BaseException(string message)
            : base(message)
        {

        }

        public BaseException(string message, string methodName)
            : base(message)
        {
            this._methodName = methodName;
        }

        public BaseException(string message, params object[] parameters)
            : this(message, null, parameters)
        {

        }

        public BaseException(string message, Exception exception)
            : base(message, exception)
        {
        }

        public BaseException(string message, Exception exception, params object[] parameters)
            : base(message, exception)
        {
            this._parameters = parameters;
        }

        public object[] GetParameters()
        {
            return _parameters;
        }

        public string GetMethodName()
        {
            return _methodName;
        }
    }
}
