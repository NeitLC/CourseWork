using System;

namespace CourseWork.Business.Exсeptions
{
    public class NoRightsException : Exception
    {
        public NoRightsException()
        {
        }

        public NoRightsException(string message) : base(message)
        {
        }
    }
}