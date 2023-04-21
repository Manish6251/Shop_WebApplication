using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication.Models
{
    public class Product
    {
        [Key] 
        public int ProductId { get; set; }

        [Required(ErrorMessage ="Required")]
        public string ProductName { get; set; }

        [Required(ErrorMessage ="Required")]
        [Display(Name ="CategoryId")]
        public int PrId { get; set;}

        [NotMapped]
        public int CategoryId { get; set;}

        [NotMapped]
        public string CategoryName { get; set; } = string.Empty;
    }
}
