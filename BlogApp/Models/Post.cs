using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogApp.Models
{
    public class Post
    {
        public int Id { get; set; }

        public string Title { get; set; }
        [Required]
        public string Author { get; set; }

        [MaxLength(5000)]
        public string Body { get; set; }

        [Display(Name = "Created At")]
        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; }

        public int CategoryId { get; set; }
        [ForeignKey(nameof(CategoryId))]
        [ValidateNever]
        public Category Category { get; set; }

    }
}
