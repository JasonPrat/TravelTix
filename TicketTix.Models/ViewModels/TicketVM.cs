using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace TicketTix.Models.ViewModels
{
    public class TicketVM
    {
        public Ticket ticket { get; set; }
        [ValidateNever]
        public IEnumerable<SelectListItem> AirlineList { get; set; }
    }
}
