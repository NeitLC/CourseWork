using System;
using System.ComponentModel.DataAnnotations;
using CourseWork.Domain.Interfaces;

namespace CourseWork.Domain.Models
{
    public class Comment : IEntityWithId<int>
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public string Text { get; set; }
        
        public DateTime DateOfCreation { get; set; } = DateTime.UtcNow;
        
        public int UserId;
        
        public User User { get; set; }
        
        public int ItemId { get; set; }
        
        public Item Item { get; set; }
    }
}