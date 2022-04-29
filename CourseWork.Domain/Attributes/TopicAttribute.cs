using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CourseWork.Domain.Attributes
{
    public class TopicAttribute : ValidationAttribute
    {
        private readonly string[] _topics;

        public TopicAttribute(string[] topics)
        {
            _topics = topics;
        }

        public string[] Topics => _topics;

        public override bool IsValid(object value)
        {
            return value != null && _topics.Contains(value.ToString());
        }
    }
}