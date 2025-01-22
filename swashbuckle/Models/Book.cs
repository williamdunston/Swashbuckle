using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookstoreAPI.Models
{
    public class Book
    {
        public int Id { get; set; }
        [Required]
        public int Stock { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Author { get; set; }
        [DefaultValue(1.00)]
        public decimal Price { get; set; }
    }
}