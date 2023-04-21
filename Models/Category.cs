using System.ComponentModel.DataAnnotations;

namespace WebApplication.Models
{
    public class Category
    {
        [Key] 
        public int PrId { get; set; }
        public int CategoryId { get; set; } 
        public string CategoryName { get; set; }
    }
}
