using CourseWork.Domain.Data;
using CourseWork.Domain.Models;

namespace CourseWork.Domain.Repositories
{
    public class CommentRepository : CoreRepository<Comment, ApplicationContext, int>
    {
        public CommentRepository(ApplicationContext context) : base(context)
        {
        }
    }
}