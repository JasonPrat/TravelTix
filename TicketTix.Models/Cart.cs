using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketTix.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace TravelTix.Models
{
    public class Cart
    {
        public int Id { get; set; }
        public int TicketId { get; set; }
        [ForeignKey("TicketId")]
        [ValidateNever]
        public Ticket ticket { get; set; }
        [Range(1, 200, ErrorMessage = "Booking can't be more than 200 seats")]
        public int person { get; set; }
        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }
        [NotMapped]
        public double Price { get; set; }
    }
}
