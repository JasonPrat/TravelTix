using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketTix.DataAccess.Repository.IRepository;
using TicketTix.Models;

namespace TicketTix.DataAccess.Repository
{
    public class TicketRepository : Repository<Ticket>, ITicketRepository
    {
        private TicketTixDbContext _db;
        public TicketRepository(TicketTixDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Ticket obj)
        {
            var objFromDb = _db.Tickets.FirstOrDefault(u=>u.Id== obj.Id);
            if (objFromDb != null)
            {
                objFromDb.Id = obj.Id;
                objFromDb.Departure = obj.Departure;
                objFromDb.Arrival = obj.Arrival;
                if (obj.ImageUrl != null)
                {
                    objFromDb.ImageUrl = obj.ImageUrl;
                }
                objFromDb.price = obj.price;
                objFromDb.AirlineId = obj.AirlineId;
                objFromDb.Origin=obj.Origin;
                objFromDb.Destination = obj.Destination;
            }
        }
    }
}
