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
        public int XPosTopLeft { get; set; }
        public int YPosTopLeft { get; set; }
        public int XPosBottomRight { get; set; }
        public int YPosBottomRight { get; set; }

        public int SwitchBoxId { get; set; }
        public SwitchBox SwitchBox { get; set; } = null!;

        public ICollection<ComponentConnection> Connections { get; set; } = new List<ComponentConnection>();
        public ICollection<ComponentConnection> ConnectedTo { get; set; } = new List<ComponentConnection>();
    }
} 