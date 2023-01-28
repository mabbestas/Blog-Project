using HS4_BlogProject.Application.Models.DTOs;
using HS4_BlogProject.Application.Models.VMs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HS4_BlogProject.Application.Services.GenreSerivce
{
    public interface IGenreService
    {
        Task<List<GenreVM>> GetGenres();
        Task Create(CreateGenreDTO model);
        Task Update(UpdateGenreDTO model);
        Task Delete(int id);
        Task<UpdateGenreDTO> GetById(int id);
    }
}
