using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using CourseWork.Domain.Enums;
using CourseWork.Domain.Models;
using Microsoft.AspNetCore.Http;

namespace CourseWork.Web.ViewModels
{
    public class CollectionViewModel
    {
        public int? Id { get; set; }
        
        [StringLength(50)]
        public string Name{ get; set; }
        
        public string Description { get; set; }
        
        public string Topic { get; set; }
        
        public IEnumerable<string> Topics { get; set; }
        
        public IEnumerable<IFormFile> Files { get; set; }
        
        public string FirstFieldName { get; set; }
        
        public string SecondFieldName { get; set; }
        
        public string ThirdFieldName { get; set; }
        
        public FieldType FirstFieldType { get; set; }
        
        public FieldType SecondFieldType { get; set; }
        
        public FieldType ThirdFieldType { get; set; }
        
        public User User { get; set; }
    }
}