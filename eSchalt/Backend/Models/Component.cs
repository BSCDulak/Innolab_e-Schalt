using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eSchalt.Backend.Models
{
    public class Component
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        // Use double coordinates to preserve exact AI detection positions
        public double XPosTopLeft { get; set; }
        public double YPosTopLeft { get; set; }
        public double XPosBottomRight { get; set; }
        public double YPosBottomRight { get; set; }

        public int SwitchBoxId { get; set; }
        public SwitchBox SwitchBox { get; set; } = null!;

        public ICollection<ComponentConnection> Connections { get; set; } = new List<ComponentConnection>();
        public ICollection<ComponentConnection> ConnectedTo { get; set; } = new List<ComponentConnection>();
    }
} 