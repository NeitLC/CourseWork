using CourseWork.Domain.Data;
using CourseWork.Domain.Models;

namespace CourseWork.Domain.Repositories
{
    public class ImageRepository : CoreRepository<Image, ApplicationContext, int>
    {
        public ImageRepository(ApplicationContext context) : base(context)
        {
        }
    }
}