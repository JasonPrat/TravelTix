using TicketTix.Models;

namespace TicketTix.DataAccess.Repository.IRepository
{
    public interface ITicketRepository : IRepository<Ticket>
    {
        void Update (Ticket obj);
    }
}
