using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CourseWork.Domain.Interfaces;

namespace CourseWork.Domain.Models
{
    public class Tag : IEntityWithId<int>
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        public ICollection<Item> Items { get; set; } = new List<Item>();
    }
}