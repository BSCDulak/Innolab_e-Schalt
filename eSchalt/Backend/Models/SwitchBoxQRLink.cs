using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eSchalt.Backend.Models
{
    public class SwitchBoxQRLink
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int SwitchBoxId { get; set; }
        public SwitchBox SwitchBox { get; set; } = null!;

        [Required]
        [MaxLength(512)]
        public string QRLink { get; set; } = string.Empty;
    }
}