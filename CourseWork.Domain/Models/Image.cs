using System.ComponentModel.DataAnnotations;
using CourseWork.Domain.Interfaces;

namespace CourseWork.Domain.Models
{
    public class Image : IEntityWithId<int>
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string ImagePath { get; set; }
        
        public string PublicId { get; set; }
        
        public int CollectionId { get; set; }
        
        public Collection Collection { get; set; }
    }
}