using AutoMapper;
using HS4_BlogProject.Application.Models.DTOs;
using HS4_BlogProject.Application.Models.VMs;
using HS4_BlogProject.Domain.Entities;
using HS4_BlogProject.Domain.Enums;
using HS4_BlogProject.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HS4_BlogProject.Application.Services.GenreSerivce
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IMapper _mapper;
        public GenreService(IGenreRepository genreRepository, IMapper mapper)
        {
            _genreRepository = genreRepository;
            _mapper = mapper;
        }

        public async Task Create(CreateGenreDTO model)
        {
            Genre genre = _mapper.Map<Genre>(model);

            await _genreRepository.Create(genre);
        }

        public async Task Delete(int id)
        {
            Genre genre = await _genreRepository.GetDefault(x => x.Id == id);
            genre.DeleteDate = DateTime.Now;
            genre.Status = Status.Passive;
            await _genreRepository.Delete(genre);
        }

        public async Task<UpdateGenreDTO> GetById(int id)
        {
            Genre genre = await _genreRepository.GetDefault(x => x.Id == id);

            var model = _mapper.Map<UpdateGenreDTO>(genre);
            return model;
        }

        public async Task<List<GenreVM>> GetGenres()
        {
           return await _genreRepository.GetFilteredList(
                select: x => new GenreVM
                {
                    Id = x.Id,
                    Name = x.Name
                },
                where: x => x.Status != Status.Passive,
                orderBy: x => x.OrderBy(x => x.Name)
                );
        }

        public async Task Update(UpdateGenreDTO model)
        {
            var genre = _mapper.Map<Genre>(model);
            await _genreRepository.Update(genre);
        }
    }
}
