using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Elysian.Models
{
    public class Store
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        public string UserId { get; set; }

        public ICollection<Billboard> Billboards { get; set; } = new List<Billboard>();
        public ICollection<Categories> Categories { get; set; } = new List<Categories>();
        public ICollection<Size> Size { get; set; } = new List<Size>();
        public ICollection<Color> Color { get; set; } = new List<Color>();

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
