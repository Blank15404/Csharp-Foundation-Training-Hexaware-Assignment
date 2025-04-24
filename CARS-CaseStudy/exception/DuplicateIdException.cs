using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CARS_CaseStudy.exception
{
    public class DuplicateIdException : Exception
    {
        public DuplicateIdException() : base("Duplicate ID found") { }
        public DuplicateIdException(string message) : base(message) { }
    }
}