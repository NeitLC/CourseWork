using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CourseWork.Domain.Attributes;
using CourseWork.Domain.Enums;
using CourseWork.Domain.Interfaces;

namespace CourseWork.Domain.Models
{
    public class Collection : IEntityWithId<int>
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        
        [Required]
        public string Description { get; set; }
        
        [Required]
        [Topic(new[] { "Alcohol", "Books", "Postmarks", "Coins", "Clocks", "Comics", "Magnetics" })]
        public string Topic { get; set; }
        
        [StringLength(50)]
        public string FirstFieldName { get; set; }
        
        [StringLength(50)]
        public string SecondFieldName { get; set; }
        
        [StringLength(50)]
        public string ThirdFieldName { get; set; }
        
        public FieldType FirstFieldType { get; set; }
        
        public FieldType SecondFieldType { get; set; }
        
        public FieldType ThirdFieldType { get; set; }
        
        [Required]
        [Column(TypeName = "varchar(500)")]
        public string UserId { get; set; }

        public User User { get; set; }

        public IEnumerable<Item> Items { get; set; }

        public ICollection<Image> Images { get; set; } = new List<Image>();
    }
}