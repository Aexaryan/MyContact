using System.ComponentModel.DataAnnotations;

namespace MyContacts.Models
{
    public class Category
    {
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Please enter a category name")]
        public string Name { get; set; } = null!;
    }
}
