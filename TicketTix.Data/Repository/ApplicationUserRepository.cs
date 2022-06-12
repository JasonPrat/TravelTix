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
    public class ApplicationUserRepository : Repository<ApplicationUser>,IApplicationUserRepository
    {
        private TicketTixDbContext _db;
        public ApplicationUserRepository(TicketTixDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
