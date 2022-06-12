using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TicketTix.Models;
using TravelTix.Models;

namespace TicketTix.DataAccess
{
    public class TicketTixDbContext : IdentityDbContext
    {
        public TicketTixDbContext(DbContextOptions<TicketTixDbContext> options) : base(options)
        {
        }
        public DbSet<Airline> Airlines { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<OrderHeader> OrderHeaders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }
    }
}