using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SVoting.Application.Contracts.Persistence;
using SVoting.Persistence.Data;
using SVoting.Persistence.Repository;

namespace SVoting.Persistence;

public static class PersistenceServicesRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<SVotingDbContext>(options => options.UseSqlite("Data Source=SVoting.db", b => b.MigrationsAssembly("SVoting.Presentation.WebApi")));

        // add services
        services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ICodesRepository, CodeRepository>();
        services.AddScoped<INomineeRepository, NomineeRepository>();
        services.AddScoped<IPollCategoryRepository, PollCategoryRepository>();
        services.AddScoped<IPollingSpaceRepository, PollingSpaceRepository>();
        services.AddScoped<IPollRepository, PollRepository>();
        services.AddScoped<IVoteRepository, VoteRepository>();

        return services;
    }
}
