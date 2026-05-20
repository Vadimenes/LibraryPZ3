using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryPZ3.Models
{
    [Table("StorageLocations")]
    public class StorageLocation
    {
        [Key]
        public int LocationId { get; set; }

        [Required, MaxLength(50)]
        public string Room { get; set; } = string.Empty; // Например, "Главный зал"

        [Required, MaxLength(50)]
        public string Shelf { get; set; } = string.Empty; // Например, "Стеллаж 4, Полка 2"
    }
}