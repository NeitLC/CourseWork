using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CourseWork.Domain.Enums;
using CourseWork.Domain.Interfaces;

namespace CourseWork.Domain.Models
{
    public class Collection : IEntityWithId<int>
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        [StringLength(30)]
        public string Name { get; set; }
        
        [Required]
        [MaxLength(150)]
        public string Description { get; set; }
        
        public string FirstFieldName { get; set; }
        
        public string SecondFieldName { get; set; }
        
        public string ThirdFieldName { get; set; }
        
        public FieldType FirstFieldType { get; set; }
        
        public FieldType SecondFieldType { get; set; }
        
        public FieldType ThirdFieldType { get; set; }

        public int UserId;
        
        public User User;
        
        public IEnumerable Items = new List<Item>();
    }
}