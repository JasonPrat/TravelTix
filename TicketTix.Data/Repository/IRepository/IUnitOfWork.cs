using TravelTix.DataAccess.Repository.IRepository;


namespace TicketTix.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        IAirlineRepository Airline { get; }
        ITicketRepository Ticket { get; }
        IApplicationUserRepository ApplicationUser { get; }
        ICartRepository Cart { get; }  
        IOrderDetailRepository OrderDetail { get; }
        IOrderHeaderRepository OrderHeader { get; } 
        void Save();
    }
}
