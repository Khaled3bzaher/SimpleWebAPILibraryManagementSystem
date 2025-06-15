using Microsoft.EntityFrameworkCore;
using SimpleWebAPILibraryManagementSystem_AtosTask02.Data;
using SimpleWebAPILibraryManagementSystem_AtosTask02.Persistence;
using SimpleWebAPILibraryManagementSystem_AtosTask02.Repositories.Implementations;
using SimpleWebAPILibraryManagementSystem_AtosTask02.Repositories.Interfaces;
using SimpleWebAPILibraryManagementSystem_AtosTask02.Services.Implementations;
using SimpleWebAPILibraryManagementSystem_AtosTask02.Services.Interfaces;

namespace SimpleWebAPILibraryManagementSystem_AtosTask02.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDIServices(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<LibraryDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<IBookRepository, BookRepository>();
            services.AddScoped<IBorrowedBookRepository, BorrowedBookRepository>();

            services.AddScoped<IBorrowerService, BorrowerService>();
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IBorrowedBookService, BorrowedBookService>();

            return services;
        }
    }
}
