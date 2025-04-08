using System;

namespace Task_8_SIS
{
    public class SISException : Exception
    {
        public SISException(string message) : base(message) { }
    }
}