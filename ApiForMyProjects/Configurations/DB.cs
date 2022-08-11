//using HCM.DbContexts;
using Microsoft.EntityFrameworkCore;
public static class DB
{
    public static void DbProduction(this IServiceCollection services, IConfiguration Configuration)
    {
        string connection = Environment.GetEnvironmentVariable("ConnectionString");
        //services.AddDbContext<Context>(options => options.UseSqlServer(connection), ServiceLifetime.Transient);

        //services.AddDbContext<Context>(options => options.UseSqlServer(connection), ServiceLifetime.Transient);

        PartnerManagement.Helper.Connection.iBOS = connection;
    }

    public static void DbDevelopment(this IServiceCollection services, IConfiguration Configuration)
    {
        //services.AddDbContext<Context>(options => options.UseSqlServer(Configuration.GetConnectionString("Development")), ServiceLifetime.Transient);

       // services.AddDbContext<Context>(options => options.UseSqlServer(Configuration.GetConnectionString("Development")), ServiceLifetime.Transient);

        PartnerManagement.Helper.Connection.iBOS = Configuration.GetConnectionString("Development");
    }
}