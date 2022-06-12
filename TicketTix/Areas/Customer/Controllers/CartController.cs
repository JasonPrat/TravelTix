using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using System.Security.Claims;
using TicketTix.DataAccess.Repository.IRepository;
using TicketTix.Utility;
using TravelTix.Models;
using TravelTix.Models.ViewModels;

namespace TravelTix.Areas.Customer.Controllers
{

    [Area("Customer")]
    [Authorize]
    public class CartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        [BindProperty]
        public CartVM scVM { get; set; }
        public double subtotal { get; set; }
        public CartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            scVM = new CartVM()
            {
                ListCart = _unitOfWork.Cart.GetAll(u=>u.ApplicationUserId==claim.Value, includeProp: "ticket"),
                OrderHeader=new()
            };
            foreach (var cart in scVM.ListCart)
            {
                cart.Price = cart.ticket.price;
                scVM.OrderHeader.GrandTotal += (cart.person * cart.Price);
            }
            return View(scVM);
        }
        public IActionResult Remove(int cartId)
        {
            var cart = _unitOfWork.Cart.GetFirstOrDefault(u => u.Id == cartId);
            _unitOfWork.Cart.Remove(cart);
            _unitOfWork.Save();
            var count = _unitOfWork.Cart.GetAll(u => u.ApplicationUserId == cart.ApplicationUserId).ToList().Count;
            HttpContext.Session.SetInt32(SD.SessionCart, count);
            return RedirectToAction("Index");
        }
        public IActionResult Summary()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            scVM = new CartVM()
            {
                ListCart = _unitOfWork.Cart.GetAll(u => u.ApplicationUserId == claim.Value,
                includeProp: "ticket"),
                OrderHeader = new()
            };
            scVM.OrderHeader.ApplicationUser = _unitOfWork.ApplicationUser.GetFirstOrDefault(
                u => u.Id == claim.Value);

            scVM.OrderHeader.Name = scVM.OrderHeader.ApplicationUser.Name;
            scVM.OrderHeader.PhoneNumber = scVM.OrderHeader.ApplicationUser.PhoneNumber;
            scVM.OrderHeader.Address = scVM.OrderHeader.ApplicationUser.Address;
            scVM.OrderHeader.City = scVM.OrderHeader.ApplicationUser.City;
            scVM.OrderHeader.State = scVM.OrderHeader.ApplicationUser.State;
            scVM.OrderHeader.NIK = scVM.OrderHeader.ApplicationUser.NIK;



            foreach (var cart in scVM.ListCart)
            {
                cart.Price = cart.ticket.price;
                scVM.OrderHeader.GrandTotal += (cart.Price * cart.person);
            }
            return View(scVM);
        }
        [HttpPost]
        [ActionName("Summary")]
        [ValidateAntiForgeryToken]
        public IActionResult SummaryPOST(CartVM scVM)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            scVM.ListCart = _unitOfWork.Cart.GetAll(u => u.ApplicationUserId == claim.Value,
                includeProp: "ticket");

            scVM.OrderHeader.PaymentStatus = SD.PaymentStatusPending;
            scVM.OrderHeader.OrderStatus = SD.StatusPending;
            scVM.OrderHeader.OrderDate = System.DateTime.Now;
            scVM.OrderHeader.ApplicationUserId = claim.Value;


            foreach (var cart in scVM.ListCart)
            {
                cart.Price = cart.ticket.price;
                scVM.OrderHeader.GrandTotal += (cart.Price * cart.person);
            }
            ApplicationUser appUser = _unitOfWork.ApplicationUser.GetFirstOrDefault(u => u.Id == claim.Value);

           
                scVM.OrderHeader.PaymentStatus = SD.PaymentStatusPending;
                scVM.OrderHeader.OrderStatus = SD.StatusPending;
           

            _unitOfWork.OrderHeader.Add(scVM.OrderHeader);
            _unitOfWork.Save();
            foreach (var cart in scVM.ListCart)
            {
                OrderDetail orderDetail = new()
                {
                    TicketId = cart.TicketId,
                    OrderId = scVM.OrderHeader.Id,
                    price = cart.Price,
                    person = cart.person
                };
                _unitOfWork.OrderDetail.Add(orderDetail);
                _unitOfWork.Save();
            }


            
                //Payment Settings
                var domain = "https://localhost:7106/";
                var options = new SessionCreateOptions
                {

                    LineItems = new List<SessionLineItemOptions>(),
                    Mode = "payment",
                    SuccessUrl = domain + $"Customer/Cart/OrderConfirmation?id={scVM.OrderHeader.Id}",
                    CancelUrl = domain + $"Customer/Cart/Index",
                };
                foreach (var item in scVM.ListCart)
                {
                    var sessionLineItem = new SessionLineItemOptions
                    {
                        PriceData = new SessionLineItemPriceDataOptions
                        {
                            UnitAmount = (long)(item.Price * 100),
                            Currency = "idr",
                            ProductData = new SessionLineItemPriceDataProductDataOptions
                            {
                                Name = item.ticket.Origin+" - "+item.ticket.Destination,
                            },

                        },
                        Quantity = item.person,
                    };
                    options.LineItems.Add(sessionLineItem);
                }

                var service = new SessionService();
                Session session = service.Create(options);
                _unitOfWork.OrderHeader.UpdatePaymentID(scVM.OrderHeader.Id, session.Id, session.PaymentIntentId);
                _unitOfWork.Save();
                Response.Headers.Add("Location", session.Url);
                return new StatusCodeResult(303);
            }
        public IActionResult OrderConfirmation(int id)
        {
            OrderHeader oh = _unitOfWork.OrderHeader.GetFirstOrDefault(u => u.Id == id);
            
                var service = new SessionService();
                Session session = service.Get(oh.SessionId);
                if (session.PaymentStatus.ToLower() == "paid")
                {
                    _unitOfWork.OrderHeader.UpdateStatus(id, SD.StatusApproved, SD.PaymentStatusApproved);
                    _unitOfWork.Save();
                }
            
            
            List<Cart> shoppingCarts = _unitOfWork.Cart.GetAll(u => u.ApplicationUserId == oh.ApplicationUserId).ToList();
            HttpContext.Session.Clear();
            _unitOfWork.Cart.RemoveRange(shoppingCarts);
            _unitOfWork.Save();
            return View(id);
        }
     }
}
