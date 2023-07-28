using MailKit.Search;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using MVCT.Data;
using MVCT.Models;
using MVCT.Models.Attandance;
using MVCT.Models.Member;
using MVCT.Models.UserReport;
using MVCT.Repository.ICheckinRepository;

using SkiaSharp;
using System.Diagnostics.Contracts;
using System.Security.Claims;
using System.Security.Principal;

namespace MVCT.Controllers
{
    public class AttandancesController : Controller
    {


        private readonly UserManager<AppUser> _userManager;
        private readonly IAttandancesRepository _repository;

        public AttandancesController(UserManager<AppUser> userManager, ApplicationDbContext db, IAttandancesRepository repository)
        {
            _userManager = userManager;

            _repository = repository;

        }

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Index()
        {
            
           
            
            List<Attandances> objAttList = await _repository.GetAllAsync(includeProperties: "User");
            List<AttandanceDTO> result = new List<AttandanceDTO>();
        

            var groupByUsers = objAttList.GroupBy(x => new { x.User, x.DateTime.Date });
            foreach (var item in groupByUsers)
            {
                var user = item.Key.User;
                var attendances = item.OrderBy(x => x.DateTime).ToList();

                DateTime? checkIn = null;
                DateTime? checkOut = null;

                if (attendances.Any())
                {
                    checkIn = attendances.First().DateTime;

                    if (attendances.Count > 1)
                    {
                        checkOut = attendances.Last().DateTime;
                    }
                }
                var model = new AttandanceDTO
                {
                    CheckIn = checkIn,
                    CheckOut = checkOut,
                    Username = user.UserName,
                    DateTime = item.Key.Date,
                    Attandances = attendances
                };
                result.Add(model);
            }

            return View(result);

        }
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }

            // Fetch the attendance record from the repository
            Attandances attandances = await _repository.GetAsync(p => p.Id == id);

            if (attandances == null)
            {
                return NotFound();
            }

            // Map the attendance record to the AttandanceDTO model
            var attandanceDTO = new AttandanceDTO
            {
                Id = attandances.Id,
                DateTime = attandances.DateTime,
                // Other properties from Attandances that you might want to map
            };

            return View(attandanceDTO);
        }
        [HttpPost, ActionName("Update")]
        public async Task<IActionResult> UpdateAsync(int id, AttandanceDTO model)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (id != model.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                Attandances attandances = await _repository.GetAsync(p => p.Id == model.Id);
                //Attandances attandances = new() 
                //{
                attandances.DateTime = model.DateTime;
                attandances.UserId = user.Id;
                attandances.Id = model.Id;
                //};

                if (attandances == null)
                {
                    return NotFound();
                }



                await _repository.UpdatedAsync(attandances);

                TempData["success"] = "Attendance updated successfully!";
                return RedirectToAction("Index");
            }

            return View(model);
        }
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Attandances? attandances = await _repository.GetAsync(p => p.Id == id);

            if (attandances == null)
            {
                return NotFound();
            }
            return View(attandances);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            Attandances? foundId = await _repository.GetAsync(p => p.Id == id);
            if (foundId == null)
            {
                return NotFound();
            }

            await _repository.RemoveAsync(foundId);
            TempData["success"] = "Bạn đã xóa thành công !";
            return RedirectToAction("Index");
        }


    }
}

