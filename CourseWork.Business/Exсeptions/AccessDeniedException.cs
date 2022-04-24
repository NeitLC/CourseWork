using System;

namespace CourseWork.Business.Exсeptions
{
    public class AccessDeniedException : Exception
    {
        public AccessDeniedException()
        {
        }

        public AccessDeniedException(string? message) : base(message)
        {
        }
    }
}