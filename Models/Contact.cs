using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace MyContacts.Models
{
    public class Contact
    {
        public int ContactId { get; set; } // PK for the Contact table

        [Required(ErrorMessage = "Please enter a first name")]
        public string FirstName { get; set; } = null!; // Null-forgiving operator for non-nullable reference types

        [Required(ErrorMessage = "Please enter a last name")]
        public string LastName { get; set; } = null!; // Null-forgiving operator for non-nullable reference types

        [Required(ErrorMessage = "Please enter a phone number")]
        public string PhoneNumber { get; set; } = null!; // Null-forgiving operator for non-nullable reference types

        [Required(ErrorMessage = "Please enter an email")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        public string Email { get; set; } = null!; // Null-forgiving operator for non-nullable reference types

        [Required(ErrorMessage = "Please select a category")]
        public int CategoryId { get; set; } // FK for the Category table

        [ValidateNever]
        public Category Category { get; set; } = null!; // Navigation property with null-forgiving operator

        public DateTime DateAdded { get; set; } = DateTime.Now; // Default value set to current time

        // Read-only property that returns the full name of the contact
        public string Slug =>
            (FirstName?.Replace(' ', '-').ToLower() + "-" + LastName?.Replace(' ', '-').ToLower()) ?? string.Empty;
    }
}
