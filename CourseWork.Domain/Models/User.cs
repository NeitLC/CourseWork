using System.Collections.Generic;
using CourseWork.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace CourseWork.Domain.Models
{
    public class User : IdentityUser, IEntityWithId<string>
    {
        public ICollection<Collection> Collections { get; set; } = new List<Collection>();
        
        public ICollection<Item> LikedItems  { get; set; } = new List<Item>();
        
        public ICollection<Comment> Comments  { get; set; } = new List<Comment>();
    }
}