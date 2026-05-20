using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibraryPZ3.Models
{
    [Table("BookMovements")]
    public class BookMovement
    {
        [Key]
        public int MovementId { get; set; }

        [Required]
        public int BookId { get; set; }
        public virtual Book Book { get; set; } = null!;

        // Кому выдали (может быть null, если книгу просто переложили на другую полку)
        public int? ReaderId { get; set; }
        public virtual Reader? Reader { get; set; }

        // Откуда взяли
        public int? FromLocationId { get; set; }
        public virtual StorageLocation? FromLocation { get; set; }

        // Куда положили (может быть null, если книга на руках у читателя)
        public int? ToLocationId { get; set; }
        public virtual StorageLocation? ToLocation { get; set; }

        [Required]
        public DateTime ActionDate { get; set; } = DateTime.UtcNow;

        public bool IsReturned { get; set; } = false; // Статус возврата
    }
}