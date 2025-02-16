using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Elysian.Models
{

    public class Categories
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public Guid StoreId { get; set; }

        [ForeignKey("StoreId")]
        public Store Store { get; set; }

        [Required]
        public Guid BillBoardId { get; set; }

        [ForeignKey("BillBoardId")]
        public Billboard Billboard { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        public ICollection<Product> Products { get; set; } = new List<Product>();

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
