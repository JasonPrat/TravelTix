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
    public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
    {
        private TicketTixDbContext _db;
        public OrderHeaderRepository(TicketTixDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(OrderHeader obj)
        {
            _db.OrderHeaders.Update(obj);
        }

        public void UpdatePaymentID(int id, string sessionId, string paymentItentId)
        {
            var orderFromDb = _db.OrderHeaders.FirstOrDefault(u => u.Id == id);
            orderFromDb.PaymentDate = DateTime.Now;
            orderFromDb.SessionId = sessionId;
            orderFromDb.PaymentIntentId = paymentItentId;
        }

        public void UpdateStatus(int id, string orderStatus, string? paymentStatus = null)
        {
            var retrieveOrder = _db.OrderHeaders.FirstOrDefault(u => u.Id == id);
            if (retrieveOrder != null)
            {
                retrieveOrder.OrderStatus = orderStatus;
                if (paymentStatus != null)
                {
                    retrieveOrder.PaymentStatus =paymentStatus;
                }
            }
        }
    }
}
