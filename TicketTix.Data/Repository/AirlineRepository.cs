using TicketTix.DataAccess.Repository.IRepository;
using TicketTix.Models;

namespace TicketTix.DataAccess.Repository
{
    public class AirlineRepository : Repository<Airline>, IAirlineRepository
    {
        private TicketTixDbContext _db;
        public AirlineRepository(TicketTixDbContext db) : base(db)
        {
            _db = db;
        }


        public void Update(Airline obj)
        {
            _db.Airlines.Update(obj);
        }
    }
}
