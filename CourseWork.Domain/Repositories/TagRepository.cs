using CourseWork.Domain.Data;
using CourseWork.Domain.Models;

namespace CourseWork.Domain.Repositories
{
    public class TagRepository : CoreRepository<Tag, ApplicationContext, int>
    {
        public TagRepository(ApplicationContext context) : base(context)
        {
        }
    }
}