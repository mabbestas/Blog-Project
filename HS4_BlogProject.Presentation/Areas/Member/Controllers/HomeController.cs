using HS4_BlogProject.Application.Services.Postservice;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HS4_BlogProject.Presentation.Areas.Member.Controllers
{
    [Area("Member")]
    [Authorize]
    public class HomeController : Controller
    {
        private readonly IPostService _postService;
        public HomeController(IPostService postService)
        {
            _postService = postService;
        }

        // Üyelerin post'larını gösteriyorum
        public async Task<IActionResult> Index()
        {             
            return View(await _postService.GetPostsForMembers());
        }
    }
}
