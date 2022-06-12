using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TicketTix.DataAccess.Repository.IRepository;
using TravelTix.Models;

namespace TravelTix.DataAccess.Repository.IRepository
{
    public interface IOrderHeaderRepository : IRepository<OrderHeader>
    {
        void Update(OrderHeader obj);
        void UpdateStatus(int id, string orderStatus, string? paymentStatus = null);
        void UpdatePaymentID(int id, string sessionId, string paymentItentId);
    }
}
