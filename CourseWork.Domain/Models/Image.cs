using System.ComponentModel.DataAnnotations;
using CourseWork.Domain.Interfaces;

namespace CourseWork.Domain.Models
{
    public class Image : IEntityWithId<int>
    {
        [Key]
        public int Id { get; set; }
    }
}