using Cursova.Data;
using Cursova.Interfaces;
using Cursova.Models;
using Cursova.Services;
using Cursova.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;


namespace SISLab1.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IImageServise _imageServise;

        public AccountController(UserManager<AppUser> userManager, 
            SignInManager<AppUser> signInManager,
            IImageServise imageServise)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _imageServise = imageServise;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginVM)
        {
            if (!ModelState.IsValid) return View(loginVM);

            var user = await _userManager.FindByEmailAsync(loginVM.Email);

            if (user != null)
            {
                //User is found, check password
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginVM.Password);
                if (passwordCheck)
                {
                    //Password correct, sign in
                    var result = await _signInManager.PasswordSignInAsync(user, loginVM.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                //Password is incorrect
                ModelState.AddModelError("", "Неправильний пароль");
                return View(loginVM);
            }
            //User not found
            ModelState.AddModelError("", "Такого користувача не існує.");
            return View(loginVM);
        }

        [HttpGet]
        public IActionResult Register()
        {
            var regsterVM = new RegisterViewModel();
            return View(regsterVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel registerVM)
        {
            if (!ModelState.IsValid) return View(registerVM);

            var user = await _userManager.FindByEmailAsync(registerVM.Email);
            if (user != null)
            {
                ModelState.AddModelError("", "Користувач з тикою електроною почтою вже існує");
                return View(registerVM);
            }
            var imageUrl = await _imageServise.SaveImage(registerVM.Image);

            var newUser = new AppUser()
            {
                UserName = registerVM.Email,
                Email = registerVM.Email,
                PhoneNumber = registerVM.PhoneNumber,
                FirstName = registerVM.FirstName,
                LastName = registerVM.LastName,
                MiddleName = registerVM.MiddleName,
                Buildind = registerVM.Buildind,
                Street = registerVM.Street,
                Apartment = registerVM.Apartment,
                DocumentUrl = imageUrl,
            };
            var newUserResponse = await _userManager.CreateAsync(newUser, registerVM.Password);

            if (newUserResponse.Succeeded)
            {
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);
                var result = await _signInManager.PasswordSignInAsync(newUser, registerVM.Password, false, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }

            }
            return View(registerVM);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOff()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
