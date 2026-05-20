using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; // Важно для [NotMapped]

namespace LibraryPZ3.Models
{
    [Table("Authors")]
    public class Author
    {
        [Key]
        public int AuthorId { get; set; }

        [Required, MaxLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string LastName { get; set; } = string.Empty;

        [Column(TypeName = "date")]
        public DateTime BirthDate { get; set; }

        [NotMapped] // Это говорит EF Core не создавать колонку FullName в БД
        public string FullName => $"{FirstName} {LastName}";

        public virtual ICollection<Book> Books { get; set; } = new List<Book>();
    }
}