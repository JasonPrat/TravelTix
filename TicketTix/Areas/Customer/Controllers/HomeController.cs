using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TicketTix.DataAccess.Repository.IRepository;
using TicketTix.Models;

namespace TicketTix.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger,IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork= unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Ticket> ticketList = _unitOfWork.Ticket.GetAll(includeProp:"Airline");
            return View(ticketList);
        }

        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}