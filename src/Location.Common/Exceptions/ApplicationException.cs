using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Location.Common.Exceptions
{
    public class ApplicationException : Exception
    {
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public ApplicationException(int errorCode, string errorMessage) : base(errorMessage)
        {
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
        }
    }
}
