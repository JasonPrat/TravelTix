using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TicketTix.DataAccess.Repository.IRepository;
using TicketTix.Models;
using TicketTix.Utility;
using TravelTix.Models;
using TravelTix.Models.ViewModels;

namespace TravelTix.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class BookingController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public BookingController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index(string Destination , DateTime? departure)
        {
            IEnumerable<Ticket> ticketList = _unitOfWork.Ticket.GetAll();
            if (Destination != null)
            {
                ticketList = ticketList.Where(x=> x.Destination.Contains(Destination));    
            }
            if (departure.HasValue)
            {
                ticketList=ticketList.Where(x=>x.Departure==departure);
            }
            return View(ticketList);
        }
        public IActionResult Details(int ticketId)
        {
            Cart cart = new()
            {
                person = 1,
                TicketId = ticketId,
                ticket = _unitOfWork.Ticket.GetFirstOrDefault(u=>u.Id==ticketId, includeProp:"Airline")
            };
            return View(cart);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public IActionResult Details(Cart cart)
        {
            var claimIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimIdentity.FindFirst(ClaimTypes.NameIdentifier);
            cart.ApplicationUserId = claim.Value;
            Cart retrieveCart = _unitOfWork.Cart.GetFirstOrDefault(
           u => u.ApplicationUserId == claim.Value && u.TicketId == cart.TicketId);
            if (retrieveCart == null)
            {
                _unitOfWork.Cart.Add(cart);
                _unitOfWork.Save();
                //HttpContext.Session.SetInt32(SD.SessionCart, _unitOfWork.Cart.GetAll(u => u.ApplicationUserId == claim.Value).ToList().Count);
            }
            else
            {
                _unitOfWork.Save();
            }
               // HttpContext.Session.SetInt32(SD.SessionCart, _unitOfWork.Cart.GetAll(u => u.ApplicationUserId == claim.Value).ToList().person);
            
            return RedirectToAction("Index");
        }
    }
}
