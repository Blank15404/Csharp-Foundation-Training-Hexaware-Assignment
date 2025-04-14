using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_7_11_SIS.Models
{
    public class SISException : Exception
    {
        public SISException(string message) : base(message) { }
    }
}
