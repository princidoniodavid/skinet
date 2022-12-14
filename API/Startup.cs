using API.Extensions;
using API.Helpers;
using API.Middleware;
using Infrastructure.Data;
using Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;

namespace API;

public class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }


    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddAutoMapper(typeof(MappingProfiles));
        services.AddControllers();
        services.AddDbContext<StoreContext>(x =>
            x.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));
        services.AddDbContext<AppIdentityDbContext>(x =>
            x.UseSqlServer(_configuration.GetConnectionString("IdentityConnection")));
        services.AddSingleton<IConnectionMultiplexer>(c =>
        {
            var config = ConfigurationOptions.Parse(_configuration
                .GetConnectionString("Redis"), true);
            return ConnectionMultiplexer.Connect(config);
        });
        services.AddApplicationServices();
        services.AddIdentityServices(_configuration);
        services.AddSwaggerDocumentation();
        services.AddCors(opt =>
        {
            opt.AddPolicy("CorsPolicy",
                policy => { policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:4200"); });
        });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseMiddleware<ExceptionMiddleware>();
        // app.UseDeveloperExceptionPage();

        app.UseStatusCodePagesWithReExecute("/errors/{0}");
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseStaticFiles();
        app.UseCors("CorsPolicy");
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseSwaggerDocumentation();
        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}