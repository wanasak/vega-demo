using System;
using System.ComponentModel.DataAnnotations;

namespace vega_demo.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        public int ModelId { get; set; }
        public Model Model { get; set; }
        public bool IsRegistered { get; set; }
        [Required]
        [StringLength(255)]
        public string ContactName { get; set; }
        [Required]
        [StringLength(255)]
        public string ContactPhone { get; set; }
        [Required]
        [StringLength(255)]
        public string ContactEmail { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}