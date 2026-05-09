using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryPZ3.Models
{
    [Table("Books")]
    public class Book
    {
        [Key]
        public int BookId { get; set; }

        [Required, MaxLength(200)]
        public string Title { get; set; } = string.Empty;

        [MaxLength(20)]
        public string? ISBN { get; set; }

        public int AuthorId { get; set; }

        public virtual Author Author { get; set; }
    }
}