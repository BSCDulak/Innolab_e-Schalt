using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eSchalt.Backend.Models
{
    public class SwitchBox
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Floor { get; set; } = string.Empty;
        public string Room { get; set; } = string.Empty;
        public string Group { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;

        public ICollection<Component> Components { get; set; } = new List<Component>();
    }
} 