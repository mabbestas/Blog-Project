using HS4_BlogProject.Application.Models.DTOs;
using HS4_BlogProject.Application.Services.AppUserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HS4_BlogProject.Presentation.Controllers
{
    /*
        Core identity is a membership
        create, update, delete user accounts
        AccountConfiguration
        Auhentication & Authorization
        Password Recovery
        Two-factor authentication with sms
        microsoft, facebook, google login providers
    */

    [Authorize]
    public class AccountController : Controller
    {
        private readonly IAppUserService _appUserService;

        public AccountController(IAppUserService appUserService)
        {
            _appUserService = appUserService;
        }

        [AllowAnonymous]
        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated) // eğer kullanıcı hali hazırda sisteme Authenticate olmuşsa
            {
                return RedirectToAction("Index", ""); // Areas
            }

            return View();
        }

        // UserManager

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            // Service 
            // AppUserService.Create(registerDTO);

            if (ModelState.IsValid)
            {
                var result = await _appUserService.Register(registerDTO);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "");
                }

                // Identity'nin içerisinde gömülü olarak bulunan Errors listesinin içerisinde dolaşıyoruz. result error ile dolarsa hataları yazdırıyoruz.

                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, item.Description);
                    TempData["Error"] = "Something went wrong";
                }
            }

            return View();

        }

        [AllowAnonymous]
        public IActionResult Login(string returnUrl = "/")
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", nameof(Areas.Member.Controllers.HomeController));
            }
            ViewData["ReturnUrl"] = returnUrl;

            return View();
        }

        [HttpPost, AllowAnonymous]
        public async Task<IActionResult> Login(LoginDTO model, string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = await _appUserService.Login(model);

                if (result.Succeeded)
                {
                    return RedirectToLocal(ReturnUrl);
                    //return RedirectToAction("Index", nameof(Areas.Member.Controllers.HomeController));
                }

                ModelState.AddModelError("", "Invalid Login Attapt!");

            }
            return View(model);
        }

        private IActionResult RedirectToLocal(string returnUrl = "/")
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", nameof(Areas.Member.Controllers.HomeController));
            }
        }

        /* Jira, Azure gibi platformalar üzerinden iş parçalarımız bulunmaktadır.
         * Jira , efor, kaynak şeçimi, süre, task. 
         * Profil güncelleme işini üzerimize aldığımızı düşünelim.
         * Wireframescathers, balsamic 
         * 1- Controller gelip action oluştururum.
         * 2- View sayfasını geliştiricem
         * 3- UpdateProfileDTO class'ını oluştururum
         * 4- GetByUserName metodunu Application katmanında Service'in işine yazarız. Interface ve Concrete olarak
         * 5- Service içinde AppUserRepository i inject etmem gerekiyor.
         * 6- Service içinde GetByUserNamemotodunu doldurdum
         * 7- Repository üzerinden GetFilteredFirstOrDefault metodunu çağırdım.
         * 8- Task, async, await keywordlerini yazdım. Asenkrın hale getirdim
         * 

        */
    
        public async Task<IActionResult> Edit(string username)
        {
            // Kullanıcı bilgilerimizi edit edeceğiz
            if (username != "")
            {
                UpdateProfileDTO user = await _appUserService.GetByUserName(username);

                return View(user);
            }
            else
                return RedirectToAction("Index", nameof(Areas.Member.Controllers.HomeController));
        }

        [HttpPost]       
        public async Task<IActionResult> Edit(UpdateProfileDTO model)
        {
            // Validation
            // service metoduna UpdateProfileDTO gönder
            if (ModelState.IsValid)
            {
                try
                {
                    await _appUserService.UpdateUser(model);
                }
                catch (Exception)
                {
                    TempData["Error"] = "";
                }

                // Mesaj yazılacak
                return RedirectToAction("Index", "Home"); 
            }
            else
            {
                TempData["Error"] = "Your profile hasn't been updated!";
                return View(model);
            }
        }

        public async Task<IActionResult> LogOut()
        {
            await _appUserService.Logout();

            return RedirectToAction("Index", "Home");
        }
    }
}
