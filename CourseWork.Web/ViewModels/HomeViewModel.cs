using System.Collections.Generic;
using CourseWork.Domain.Models;

namespace Collections.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Item> LastCreatedItems { get; set; }
        
        public IEnumerable<Collection> LargestNumberItems { get; set; }
    }
}