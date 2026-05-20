using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryPZ3.Models
{
    [Table("Readers")]
    public class Reader
    {
        [Key]
        public int ReaderId { get; set; }

        [Required(ErrorMessage = "Поле 'Имя' обязательно для заполнения"), MaxLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Поле 'Фамилия' обязательно для заполнения"), MaxLength(100)]
        public string LastName { get; set; } = string.Empty;

        [MaxLength(20)]
        public string ClassNumber { get; set; } = string.Empty; // Например, "11-А" или "Учитель"

        [NotMapped]
        public string FullName => $"{FirstName} {LastName}";
    }
}