using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketTix.Models;

namespace TravelTix.Models.ViewModels
{
    public class CartVM
    {
        public IEnumerable<Cart> ListCart { get; set; }
        public OrderHeader OrderHeader { get; set; }
        
    }
}
