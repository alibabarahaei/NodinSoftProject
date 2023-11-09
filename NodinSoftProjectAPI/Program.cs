using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NodinSoftProject.Application.InterfaceService;
using NodinSoftProject.Application.Security.Identity;
using NodinSoftProject.Application.Services;
using NodinSoftProject.Application.Services.ProductService;
using NodinSoftProject.Domain.InterfaceRepositories.Base;
using NodinSoftProject.Domain.Models.User;
using NodinSoftProject.Infrastructure.EFcore.Context;
using NodinSoftProject.Infrastructure.EFcore.Repository;
using System.Reflection;
using System.Text;
using Westwind.AspNetCore.LiveReload;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddLiveReload();



// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(cfg=>cfg.RegisterServicesFromAssembly(typeof(CreateProduct.Handler).GetTypeInfo().Assembly));

builder.Services.AddAutoMapper(typeof(Program).Assembly);



#region Services

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IUserService, UserService>();

#endregion


#region Config Identity



builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<NodinSoftProjectDBContext>()
    .AddErrorDescriber<PersianIdentityErrorDescriber>(); ;
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;

});
#endregion




var jwtSettings = builder.Configuration.GetSection("JwtSettings");
builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings.GetSection("validIssuer").Value,
        ValidAudience = jwtSettings.GetSection("validAudience").Value,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.GetSection("securityKey").Value))
    };
});


#region ConfigCookie

//builder.Services.ConfigureApplicationCookie(options =>
//{
//    options.ExpireTimeSpan = TimeSpan.FromDays(10);
//    options.AccessDeniedPath = configurationSection.GetSection("Route:AccessDenied").Value;
//    options.Cookie.Name = configurationSection.GetSection("Cookie:Name").Value;
//    options.Cookie.HttpOnly = true;
//    options.LoginPath = configurationSection.GetSection("Route:Login").Value;
//    options.ReturnUrlParameter = CookieAuthenticationDefaults.ReturnUrlParameter;
//    options.SlidingExpiration = true;
//});

#endregion



#region Config Database
builder.Services.AddDbContext<NodinSoftProjectDBContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
});


#endregion


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
