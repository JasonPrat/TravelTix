using System.ComponentModel.DataAnnotations;

namespace TicketTix.Models
{
    public class Airline
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}