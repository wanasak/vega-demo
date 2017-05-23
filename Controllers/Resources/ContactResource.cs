using System.ComponentModel.DataAnnotations;

namespace vega_demo.Controllers.Resources
{
    public class ContactResource
    {
        [Required]
        [StringLength(255)]
        public string ContactName { get; set; }
        [Required]
        [StringLength(255)]
        public string ContactPhone { get; set; }
        [Required]
        [StringLength(255)]
        public string ContactEmail { get; set; }
    }
}