using System.Collections.Generic;

namespace CourseWork.Domain.Dto
{
    public class EntityPageDto<TEntity> where TEntity: class
    {
        public IEnumerable<TEntity> Entities { get; set; }
        public Page Page { get; set; }
    }
}