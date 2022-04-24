using System;

namespace CourseWork.Business.Exсeptions
{
    public class UserNotLoggedExсeption : Exception
    {
        public UserNotLoggedExсeption()
        {
        }

        public UserNotLoggedExсeption(string? message) : base(message)
        {
            
        }
    }
}