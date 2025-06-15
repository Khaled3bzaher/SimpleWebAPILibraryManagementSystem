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
    public class BorrowedBookRepository : GenericRepository<BorrowedBook>, IBorrowedBookRepository
    {
        private readonly LibraryDbContext _context;
        private readonly IMapper _mapper;

        public BorrowedBookRepository(LibraryDbContext context, IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<DisplayBorrowedBook>> GetDisplayBorrowedBookByBorrowerId(int borrowerId)
        {
            return await GetAsQueryAble().Where(bb => bb.BorrowerId == borrowerId).OrderByDescending(bb => bb.BorrowedDate).ProjectTo<DisplayBorrowedBook>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<List<DisplayBorrowedBook>> GetDisplayBorrowedBooks()
        {
            return await GetAsQueryAble().ProjectTo<DisplayBorrowedBook>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<BorrowedBook?> GetBorrowedBook(int bookId, int borrowerId)
        {
            return await GetAsQueryAble().FirstOrDefaultAsync(bb => bb.BorrowerId == borrowerId && bb.BookId == bookId);
        }

        public async Task<List<DisplayBorrowedBook>> GetDisplayBorrowersByBookId(int bookId)
        {
            return await GetAsQueryAble().Where(bb => bb.BookId == bookId).OrderByDescending(bb => bb.BorrowedDate).ProjectTo<DisplayBorrowedBook>(_mapper.ConfigurationProvider).ToListAsync();
        }

        public async Task<bool> isBorrowed(int bookId, int borrowerId)
        {
            return await GetAsQueryAble().AnyAsync(bb => bb.BookId == bookId
            && bb.BorrowerId == borrowerId);
        }
    }
}
