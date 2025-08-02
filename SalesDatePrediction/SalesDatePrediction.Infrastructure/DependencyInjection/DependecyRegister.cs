using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SalesDatePrediction.Application.Interfaces.Common;
using SalesDatePrediction.Infrastructure.Data;

namespace SalesDatePrediction.Infrastructure.DependencyInjection
{
    public class DependencyRegister : IDependencyRegister

    {
        public void RegisterDependencies(IConfiguration configuration, IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            var repositories = typeof(DependencyRegister).Assembly
                .DefinedTypes
                .Where(t => t.Name.EndsWith("Repository") && !t.IsInterface);

            foreach (var repo in repositories)
            {
                var iface = repo.GetInterfaces().FirstOrDefault(i => !i.Name.Contains("IRepository`1"));
                if (iface != null)
                {
                    services.AddScoped(iface, repo);
                }
            }
        }
    }
}
