using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketTix.DataAccess;
using TicketTix.DataAccess.Repository;
using TravelTix.DataAccess.Repository.IRepository;
using TravelTix.Models;

namespace TravelTix.DataAccess.Repository
{
    public class OrderDetailRepository : Repository<OrderDetail>, IOrderDetailRepository
    {
        private TicketTixDbContext _db;
        public OrderDetailRepository(TicketTixDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(OrderDetail obj)
        {
            _db.OrderDetails.Update(obj);
        }
    }
}
