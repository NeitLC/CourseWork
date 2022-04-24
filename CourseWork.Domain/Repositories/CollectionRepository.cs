using CourseWork.Domain.Data;
using CourseWork.Domain.Models;

namespace CourseWork.Domain.Repositories
{
    public class CollectionRepository : CoreRepository<Collection, ApplicationContext, int>
    {
        public CollectionRepository(ApplicationContext context) : base(context)
        {
        }
    }
}