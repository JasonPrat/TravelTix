using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketTix.DataAccess.Repository.IRepository;
using TravelTix.Models;

namespace TravelTix.DataAccess.Repository.IRepository
{
    public interface ICartRepository : IRepository<Cart>
    {
    }
}
