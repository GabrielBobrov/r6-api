using System;
using System.Runtime.Serialization;

namespace R6.Services.Excpetion
{
    [Serializable]
    public class CustomException : Exception
    {
        public string Operator { get; }

        public CustomException() { }

        public CustomException(string message)
        : base(message) { }

        public CustomException(string message, Exception inner)
            : base(message, inner) { }

         public CustomException(string message, string operatorName)
            : this(message)
        {
            Operator = operatorName;
        }
    }

}