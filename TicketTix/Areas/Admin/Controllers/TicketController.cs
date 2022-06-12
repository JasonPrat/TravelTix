using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TicketTix.DataAccess.Repository.IRepository;
using TicketTix.Models.ViewModels;
using TicketTix.Utility;

namespace TicketTix.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.adm)]
    public class TicketController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;

        public TicketController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }
        //GET 
        public IActionResult Upsert(int? id)
        {
            TicketVM ticketVM = new()
            {
                ticket = new(),
                AirlineList = _unitOfWork.Airline.GetAll().Select(
                u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                })
            };

            if (id == null || id == 0)
            {
                return View(ticketVM);
            }
            else
            {
                //Update Product
                ticketVM.ticket = _unitOfWork.Ticket.GetFirstOrDefault(u => u.Id == id);
                return View(ticketVM);

            }

        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(TicketVM obj, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string RootPath = _hostEnvironment.WebRootPath;
                if (file != null)
                {
                    string filename = Guid.NewGuid().ToString();
                    var uploads = Path.Combine(RootPath, @"images\tickets");
                    var extension = Path.GetExtension(file.FileName);

                    if (obj.ticket.ImageUrl != null)
                    {
                        var oldPath = Path.Combine(RootPath, obj.ticket.ImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldPath))
                        {
                            System.IO.File.Delete(oldPath);
                        }
                    }

                    using (var fileStreams = new FileStream(Path.Combine(uploads, filename + extension), FileMode.Create))
                    {
                        file.CopyTo(fileStreams);
                    }
                    obj.ticket.ImageUrl = @"\images\tickets\" + filename + extension;

                }
                if (obj.ticket.Id == 0)
                {
                    _unitOfWork.Ticket.Add(obj.ticket);
                }
                else
                {
                    _unitOfWork.Ticket.Update(obj.ticket);
                }

                _unitOfWork.Save();
                TempData["success"] = "Product Created Successfully";
                return RedirectToAction("Index");
            }
            return View(obj);

        }
        // API Endpoint GET
        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            var ProductList = _unitOfWork.Ticket.GetAll(includeProp: "Airline");
            return Json(new { data = ProductList });
        }
        //POST
        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var obj = _unitOfWork.Ticket.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            var oldPath = Path.Combine(_hostEnvironment.WebRootPath, obj.ImageUrl.TrimStart('\\'));
            if (System.IO.File.Exists(oldPath))
            {
                System.IO.File.Delete(oldPath);
            }
            _unitOfWork.Ticket.Remove(obj);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successful" });
        }
        #endregion
    }
}
