using AspNetCoreRateLimit;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Elastic.Apm.NetCoreAll;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Newtonsoft.Json;
using ApiForMyProjects.ErrorHandling;
using System.Net;
using ApiForMyProjects.Configurations;
using ApiForMyProjects;
using Microsoft.EntityFrameworkCore;
using ApiForMyProjects.DbContexts;

var builder = WebApplication.CreateBuilder(args);


#region Configure Service
builder.Services.AddControllers();
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
});

builder.Services.AddDbContext<Context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
if (builder.Environment.IsProduction())
{
    builder.Services.DbProduction(builder.Configuration);
}
else
{
    builder.Services.DbDevelopment(builder.Configuration);
}

builder.Services.AddControllers(opts =>
{
    if (builder.Environment.IsDevelopment())
    {
        opts.Filters.Add<AllowAnonymousFilter>();
    }
    else
    {
        var authenticatedUserPolicy = new AuthorizationPolicyBuilder()
                  .RequireAuthenticatedUser()
                  .Build();
        opts.Filters.Add(new AuthorizeFilter(authenticatedUserPolicy));
    }

});

builder.Services.AddModelValidationResponse();

ServicePointManager.UseNagleAlgorithm = true;
ServicePointManager.Expect100Continue = true;
ServicePointManager.DefaultConnectionLimit = 5000;

builder.Services.JwtConfiguration(builder.Configuration);
builder.Services.LoggerService(builder);
builder.Services.AddSwaggerService();

//builder.Services.AddMediatR(typeof(Startup));
//RegisterServices(services);
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder => DependencyContainer.ConfigureContainer(builder));
//builder.Services.AddOData();

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(builder => DependencyContainer.ConfigureContainer(builder));
#endregion

#region ==== Rate limit ======

builder.Services.AddMemoryCache();

//load general configuration from appsettings.json
builder.Services.Configure<IpRateLimitOptions>(builder.Configuration.GetSection("IpRateLimiting"));

//load ip rules from appsettings.json
builder.Services.Configure<IpRateLimitPolicies>(builder.Configuration.GetSection("IpRateLimitPolicies"));

// inject counter and rules stores
builder.Services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
builder.Services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
builder.Services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();

// Add framework services.
builder.Services.AddMvc();

// configuration (resolvers, counter key builders)
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
#endregion === Close ========// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
    app.UseDeveloperExceptionPage();
else
    app.ConfigureCustomExceptionMiddleware();

app.UseHttpsRedirection();
app.AddHeaderPolicy();

app.UseRouting();
app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseAuthentication();
app.UseAuthorization();

app.UseIpRateLimiting();
app.UseAllElasticApm(builder.Configuration);

app.UseSwagger(c =>
{
    c.RouteTemplate = "ApiForMyProjects/swagger/{documentName}/swagger.json";
});
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/ApiForMyProjects/swagger/v1/swagger.json", "Api");
    c.RoutePrefix = "ApiForMyProjects/swagger";
});

app.MapControllers();

app.Run();