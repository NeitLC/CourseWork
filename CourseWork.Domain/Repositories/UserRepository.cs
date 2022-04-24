using CourseWork.Domain.Data;
using CourseWork.Domain.Models;

namespace CourseWork.Domain.Repositories
{
    public class UserRepository : CoreRepository<User, ApplicationContext, string>
    {
        public UserRepository(ApplicationContext context) : base(context)
        {
        }
    }
}