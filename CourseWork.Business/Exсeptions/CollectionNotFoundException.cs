using System;

namespace CourseWork.Business.Exсeptions
{
    public class CollectionNotFoundException : Exception
    {
        public CollectionNotFoundException()
        {
        }

        public CollectionNotFoundException(string? message) : base(message)
        {
        }
    }
}