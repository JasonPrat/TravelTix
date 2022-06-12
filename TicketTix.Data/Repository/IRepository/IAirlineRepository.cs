using TicketTix.Models;

namespace TicketTix.DataAccess.Repository.IRepository
{
    public interface IAirlineRepository : IRepository<Airline>
    {
        void Update(Airline obj);

    }
}
