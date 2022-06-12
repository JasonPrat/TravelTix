using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketTix.DataAccess.Repository.IRepository;
using TicketTix.Models;
using TicketTix.Utility;

namespace TicketTix.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.adm)]
    public class AirlineController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public AirlineController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Airline> objCategoryList = _unitOfWork.Airline.GetAll();
            return View(objCategoryList);
        }
        //GET
        public IActionResult Create()
        {
            return View();
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Airline obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Airline.Add(obj);
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(obj);

        }

        //GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var AirlineFromDbFirst = _unitOfWork.Airline.GetFirstOrDefault(u => u.Id == id);
            if (AirlineFromDbFirst == null)
            {
                return NotFound();
            }
            return View(AirlineFromDbFirst);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Airline obj)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.Airline.Update(obj);
                _unitOfWork.Save();
                return RedirectToAction("Index");
            }
            return View(obj);

        }
        //GET
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var categoryFromDbFirst = _unitOfWork.Airline.GetFirstOrDefault(u => u.Id == id);
            if (categoryFromDbFirst == null)
            {
                return NotFound();
            }
            return View(categoryFromDbFirst);
        }

        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _unitOfWork.Airline.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _unitOfWork.Airline.Remove(obj);
            _unitOfWork.Save();
            return RedirectToAction("Index");


        }
    }
}
