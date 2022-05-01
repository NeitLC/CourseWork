using System.Collections.Generic;
using CourseWork.Domain.Enums;
using CourseWork.Domain.Models;
using Microsoft.AspNetCore.Http;
using Npgsql.PostgresTypes;

namespace CourseWork.Business.Dto
{
    public class CollectionDto
    {
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public string Description { get; set; }
        
        public string Topic { get; set; }
        
        public List<IFormFile> Files { get; set; }
        
        public string FirstFieldName { get; set; }
        
        public FieldType FirstFieldType { get; set; }
        
        public string SecondFieldName { get; set; }
        
        public FieldType SecondFieldType { get; set; }

        public string ThirdFieldName { get; set; }
        
        public FieldType ThirdFieldType { get; set; }
        
        public User User { get; set; }
        
        public IEnumerable<string> Topics { get; set; }
        
    }
}