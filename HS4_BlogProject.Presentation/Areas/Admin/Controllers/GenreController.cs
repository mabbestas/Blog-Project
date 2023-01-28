using HS4_BlogProject.Application.Models.DTOs;
using HS4_BlogProject.Application.Services.GenreSerivce;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HS4_BlogProject.Presentation.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class GenreController : Controller
    {
        private readonly IGenreService _genreService;

        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        // Genre List
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _genreService.GetGenres());
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateGenreDTO model)
        {
            if (ModelState.IsValid)
            {
                _genreService.Create(model);
            }

            return RedirectToAction("Index");
        }
               
        public async Task<IActionResult> Update(int id)
        {
            // id ile Genre seçilir           
            return View(await _genreService.GetById(id));
        }

        [HttpPost]
        public async Task<IActionResult> Update(UpdateGenreDTO model)
        {
            // seçilen Genre Edit edildikten sonra veritabanında güncellenmek için gönderilir.
            await _genreService.Update(model);

            return RedirectToAction("Index"); 
        }
         
        public async Task<IActionResult> Delete(int id)
        {
            await _genreService.Delete(id);
            return RedirectToAction("Index");
        }
    }
}
