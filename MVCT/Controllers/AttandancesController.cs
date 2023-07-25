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
            List<AttandanceInfo> result = new List<AttandanceInfo>();
            var groupByUsers = objAttList.GroupBy(x => new { x.User, x.DataTime.Date });
            foreach (var item in groupByUsers)
            {
                var user = item.Key.User;
                var attendances = item.OrderBy(x => x.DataTime).ToList();

                DateTime? checkIn = null;
                DateTime? checkOut = null;

                if (attendances.Any())
                {
                    checkIn = attendances.First().DataTime;

                    if (attendances.Count > 1)
                    {
                        checkOut = attendances.Last().DataTime;
                    }
                }
                var model = new AttandanceInfo
                {
                    CheckIn = checkIn,
                    CheckOut = checkOut,
                    Username = user.UserName,
                    CurrentDate = item.Key.Date,
                    Attandances = attendances
                };
                result.Add(model);
            }

            return View(result);

        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Attandances attandances = await _repository.GetAsync(p => p.Id == id);

            if (attandances == null)
            {
                return NotFound();
            }

            return View(attandances);
        }

        [HttpPost, ActionName("Update")]
        public async Task<IActionResult> UpdateAsync(Attandances model)
        {
               var user = await _userManager.FindByNameAsync(User.Identity.Name);

            //Tạo Post
            //Attandances newpost = new Attandances()
            //{
            //    DataTime = DateTime.Now,
            //    UserId = user.Id
            //};
            if (ModelState.IsValid)
            {
                Attandances attandances = await _repository.GetAsync(p => p.Id == model.Id);

                if (attandances == null)
                {
                    return NotFound();
                }

                attandances.DataTime = model.DataTime;
                attandances.UserId = user.Id;
                //attandances.User = user.Id;

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
          
          await  _repository.RemoveAsync(foundId);
            TempData["success"] = "Bạn đã xóa thành công !";
            return RedirectToAction("Index");
        }

    
    }
}
    

