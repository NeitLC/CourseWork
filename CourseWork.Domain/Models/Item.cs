using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CourseWork.Domain.Interfaces;

namespace CourseWork.Domain.Models
{
    public class Item : IEntityWithId<int>
    {
        [Key] public int Id { get; set; }

        [Required] [StringLength(30)] public string Name { get; set; }

        public int Likes { get; set; }

        public int? FirstOptionalNumberField { get; set; }

        public int? SecondOptionalNumberField { get; set; }

        public int? ThirdOptionalNumberField { get; set; }

        public string FirstOptionalStringField { get; set; }

        public string SecondOptionalStringField { get; set; }

        public string ThirdOptionalStringField { get; set; }

        public string FirstOptionalTextField { get; set; }

        public string SecondOptionalTextField { get; set; }

        public string ThirdOptionalTextField { get; set; }

        [Column(TypeName = "date")] 
        public DateTime? FirstOptionalDateTimeField { get; set; }

        [Column(TypeName = "date")] 
        public DateTime? SecondOptionalDateTimeField { get; set; }

        [Column(TypeName = "date")] 
        public DateTime? ThirdOptionalDateTimeField { get; set; }

        public bool? FirstOptionalBoolField { get; set; }

        public bool? SecondOptionalBoolField { get; set; }

        public bool? ThirdOptionalBoolField { get; set; }

        public int CollectionId { get; set; }

        public Collection Collection { get; set; }

        public ICollection<Tag> Tags { get; set; } = new List<Tag>();

        public ICollection<Comment> Comments { get; set; } = new List<Comment>();

        public ICollection<User> UsersLiked { get; set; } = new List<User>();
    }
}