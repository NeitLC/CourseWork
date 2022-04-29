using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CourseWork.Domain.Dto;
using CourseWork.Domain.Models;

namespace CourseWork.Business.Dto
{
    public class ItemDto
    {
        public int? Id { get; set; }
        
        [StringLength(30)]
        public string Name { get; set; }
        
        public int Likes { get; set; } = 0;
        
        public int? FirstOptionalNumberField { get; set; }
        
        public int? SecondOptionalNumberField { get; set; }
        
        public int? ThirdOptionalNumberField { get; set; }
        
        public string FirstOptionalStringField { get; set; }
        
        public string SecondOptionalStringField { get; set; }
        
        public string ThirdOptionalStringField { get; set; }
        
        public string FirstOptionalTextField { get; set; }
        
        public string SecondOptionalTextField { get; set; }
        
        public string ThirdOptionalTextField { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime? FirstOptionalDateTimeField { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime? SecondOptionalDateTimeField { get; set; }

        [DataType(DataType.Date)]
        public DateTime? ThirdOptionalDateTimeField { get; set; }
        
        public bool? FirstOptionalBoolField { get; set; }
        
        public bool? SecondOptionalBoolField { get; set; }
        
        public bool? ThirdOptionalBoolField { get; set; }
        
        public Image Image { get; set; }
        
        public Collection Collection { get; set; }
        
        public int? CollectionId { get; set; }

        public bool Liked { get; set; } = false;
        
        public IEnumerable<Tag> Tags { get; set; }
        
        public string TagJson { get; set; }
        
        public EntityPageDto<Comment> Comments { get; set; }
    }
}