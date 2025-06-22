using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eSchalt.Backend.Models
{
    public class ComponentConnection
    {
        [Key]
        public int Id { get; set; }

        public int FromComponentId { get; set; }
        public Component FromComponent { get; set; } = null!;

        public int ToComponentId { get; set; }
        public Component ToComponent { get; set; } = null!;
    }
} 