using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SimpleWebAPILibraryManagementSystem_AtosTask02.Data;
using SimpleWebAPILibraryManagementSystem_AtosTask02.DTOs;
using SimpleWebAPILibraryManagementSystem_AtosTask02.Models;
using SimpleWebAPILibraryManagementSystem_AtosTask02.Persistence;
using SimpleWebAPILibraryManagementSystem_AtosTask02.Repositories.Interfaces;

namespace SimpleWebAPILibraryManagementSystem_AtosTask02.Repositories.Implementations
{
    public class BookRepository : GenericRepository<Book>, IBookRepository
    {
        private readonly IMapper _mapper;

        public BookRepository(LibraryDbContext context, IMapper mapper) : base(context)
        {
            _mapper = mapper;
        }

        public async Task<List<DisplayBookDto>> GetAllAuthorBooks(int authorId)
        {
            return await GetAsQueryAble().Where(b => b.Author.Id == authorId).ProjectTo<DisplayBookDto>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<List<DisplayBookDto>> GetAllCategoryBooks(int categoryId)
        {
            return await GetAsQueryAble().Where(b => b.Category.Id == categoryId).ProjectTo<DisplayBookDto>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<DisplayBookDto?> GetDisplayBookById(int id)
        {
            return await GetAsQueryAble().Where(b => b.Id == id).ProjectTo<DisplayBookDto>(_mapper.ConfigurationProvider).FirstOrDefaultAsync();
        }

        public async Task<List<DisplayBookDto>> GetDisplayBooks()
        {
            return await GetAsQueryAble().ProjectTo<DisplayBookDto>(_mapper.ConfigurationProvider).ToListAsync();
        }
    }
}
