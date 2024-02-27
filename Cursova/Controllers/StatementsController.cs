using Cursova.Interfaces;
using Cursova.Models;
using Cursova.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Cursova.Controllers
{
    public class StatementsController : Controller
    {
        private readonly IStatementsRepository _statementsRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IImageServise _imageServise;

        public StatementsController(IStatementsRepository statementsRepository,
            IHttpContextAccessor httpContextAccessor,
            IImageServise imageServise)
        {
            _statementsRepository = statementsRepository;
            _httpContextAccessor = httpContextAccessor;
            _imageServise = imageServise;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult SelectType()
        {
            return View();
        }

        [HttpGet]
        public IActionResult RepairAndMaintenance()
        {
            var StatementsVM = new CreateStatementsViewModel();
            return View(StatementsVM);
        }

        [HttpGet]
        public IActionResult RestorationAndImprovement()
        {
            var StatementsVM = new CreateStatementsViewModel();

            return View(StatementsVM);
        }

        [HttpPost]
        public async Task<IActionResult> CreateStatement(CreateStatementsViewModel model, string type)
        {
            if (!ModelState.IsValid) return RedirectToAction("SelectType");

            var curentUerId = _httpContextAccessor.HttpContext.User.GetUserId();
            var curUser = await _statementsRepository.GetUserById(curentUerId);
             
            var imageUrl = await _imageServise.SaveImage(model.Image);

            var statement = new Statements
            {
                AppUserId = curUser.Id,
                AppUser = curUser,
                Type = type,
                MainInfo = model.MainInfo,
                MoreDetail = model.MoreDetail,
                Status = Data.Enum.Status.Зареєстровано, 
                DateTime = DateTime.Now,
                ImageUrl = imageUrl,
            };

            if(_statementsRepository.Add(statement))
            {
                return RedirectToAction("Index","Home");
            }
            return RedirectToAction("SelectType");
        }

        [HttpGet]
        public async Task<IActionResult> MyStatements()
        {
            var curentUer = _httpContextAccessor.HttpContext.User.GetUserId();
            var statements = await _statementsRepository.GetStatementsByUserIdAsync(curentUer);
            var otherStatements = await _statementsRepository.GetOtherUserStatements(curentUer);
            var model = new MyStatemantsViewModel
            {
                MyStatements = statements,
                NotMyStatements = otherStatements,
            };
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> UsersStatements()
        {
            var statements = await _statementsRepository.GetAllUsersStatementaAsync();
            return View(statements);
        }
        [HttpGet]
        public async Task<IActionResult> AllStatementInfo(int id) 
        {
            var model = await _statementsRepository.GetAllStatementInfoAsync(id);
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AllStatementInfo(AllStatementInfoViewModel model)
        {
            if (!_statementsRepository.UpdateStatus(model)) return View(model);

            return RedirectToAction("UsersStatements");
        }
    }
}
