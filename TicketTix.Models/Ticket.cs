using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TicketTix.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        [Required]
        public string Origin { get; set; }
        [Required]
        public string Destination { get; set; }
        [Required]
        public DateTime Departure { get; set; }
        [Required]
        public DateTime Arrival { get; set; }
        [Required, Range(100000,10000000)]
        public double price { get; set; }
        [Required]
        [ValidateNever]
        public string ImageUrl { get; set; }

        [Required]
        [Display(Name = "Airline")]
        public int AirlineId { get; set; }
        [ForeignKey("AirlineId")]
        [ValidateNever]
        public Airline Airline { get; set; }
    }
}
