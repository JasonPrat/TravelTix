using TicketTix.DataAccess.Repository.IRepository;
using TravelTix.DataAccess.Repository;
using TravelTix.DataAccess.Repository.IRepository;

namespace TicketTix.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private TicketTixDbContext _db;
        public UnitOfWork(TicketTixDbContext db)
        {
            _db = db;
            Airline = new AirlineRepository(_db);
            Ticket=new TicketRepository(_db);
            Cart = new CartRepository(_db);
            ApplicationUser = new ApplicationUserRepository(_db);
            OrderHeader = new OrderHeaderRepository(_db);
            OrderDetail = new OrderDetailRepository(_db);
        }
        public IAirlineRepository Airline { get; private set; }
        public ITicketRepository Ticket { get; private set; }
        public ICartRepository Cart { get; private set; }
        public IApplicationUserRepository ApplicationUser { get; private set; }
        public IOrderHeaderRepository OrderHeader { get; private set; }
        public IOrderDetailRepository OrderDetail { get; private set; }
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
