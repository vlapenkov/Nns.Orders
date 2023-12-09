using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nns.Orders.Common.Exceptions
{
    public class NotFoundException : AppException
    {
        public NotFoundException(string message):base(message)
        {

        }

        
    }
}
