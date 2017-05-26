using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace vega_demo.Models
{
    [Table("Photos")]
    public class Photo
    {
        public int Id { get; set; }
        [Required]
        [StringLength(255)]
        public string FileName { get; set; }
        public int VehicleId { get; set; }
    }
}