using CourseWork.Domain.Data;
using CourseWork.Domain.Models;

namespace CourseWork.Domain.Repositories
{
    public class ItemRepository : CoreRepository<Item, ApplicationContext, int>
    {
        public ItemRepository(ApplicationContext context) : base(context)
        {
        }
    }
}