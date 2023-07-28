using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MVCT.Models;
using MVCT.Models.UserReport;
using MVCT.Repository.IReportRepository;

namespace MVCT.Controllers
{
    public class ReportsController : Controller
    {
        private readonly IReportRepository _repository;
        private readonly UserManager<AppUser> _userManager;
        public ReportsController(IReportRepository repository, UserManager<AppUser> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }
        public async Task< IActionResult> Index()
        {
            //List<UserReports> listReport = await _repository.GetAllAsync(includeProperties:"User");

            //return View(listReport);
            
            var emp = await _repository.GetAllAsync();
            UserReportsViewModel vm = new UserReportsViewModel();
        
            vm.UserReports = emp;
         
            
            return View(vm);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> CreateReport() 
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateReport(UserReportsViewModel model) 
        {
      
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);

                UserReports userModel = new()
                {
                    SendnDate = DateTime.Now,
                    Description = model.Description,
                    Messages = model.Messages,
                   UserId = user.Id,
                    Username = user.UserName
                  
                    

                };
              
                await _repository.CreatedAsync(userModel);
         
                TempData["StatusMessage"] = "Thông tin đã gửi thành công";
                //return RedirectToAction("Index", "Home");
                return RedirectToAction("Index", "Home");
            }

            TempData["error"] = "Lỗi không thể gửi";
            return View(model);
        }
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
         

            var detailReport = await _repository.GetAsync(p => p.Id == id);
            if (detailReport == null)
            {
                return NotFound();
            }

            return View(detailReport);
        }

        public async Task< JsonResult> DeleteEmployee(int id)
        {
            bool result = false;

            var emp = await _repository.GetAsync(p => p.Id ==id);

            if (emp != null)
            {
                result = true;
                await _repository.RemoveAsync(emp);
            }
            return Json(result);
        }

    }
}
