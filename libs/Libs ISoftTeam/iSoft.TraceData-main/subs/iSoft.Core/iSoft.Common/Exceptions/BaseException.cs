using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iSoft.Common.Exceptions
{
    public class BaseException : Exception
    {
        public BaseException(string? message)
            : base(message)
        {
        }
        public BaseException(Exception ex)
            :base(ex.ToString())
        {
        }
        public BaseException(string message, Exception ex)
            : base(message + ex.ToString())
        {
        }
    }
}
