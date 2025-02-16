using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Elysian.Models
{

    public class Billboard
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public Guid StoreId { get; set; }

        [ForeignKey("StoreId")]
        public Store Store { get; set; }

        [Required]
        [MaxLength(255)]
        public string Label { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
