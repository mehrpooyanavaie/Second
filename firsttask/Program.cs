using firsttask.Data;
using firsttask.Models;
using firsttask.Repository;
using firsttask.Repository.Base;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using System.IO.Pipelines;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        builder.Services.AddDbContext<MyFirstContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
        //builder.Services.AddDbContext<firsttask.Data.AppContext>(options =>
        //options.UseSqlServer(builder.Configuration.GetConnectionString("AppConnection")));
        //builder.Services.AddIdentity<AppliacationUser, IdentityRole>().AddEntityFrameworkStores<firsttask.Data.AppContext>().AddDefaultTokenProviders();
        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        builder.Services.AddTransient<firsttask.Repository.IUnitOfWork, firsttask.Repository.UnitOfWork>();
        //var tokenValidationParameters = new TokenValidationParameters()
        //{
        //    ValidateIssuerSigningKey = true,
        //    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["JWT:Secret"])),
        //    ValidateIssuer = true,
        //    ValidIssuer = builder.Configuration["JWT:Issuer"],
        //    ValidateAudience = true,
        //    ValidAudience = builder.Configuration["JWT:Audience"],
        //    ValidateLifetime = true,
        //    ClockSkew = TimeSpan.Zero
        //};
        //builder.Services.AddSingleton(tokenValidationParameters);
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        //add authentucation
        //builder.Services.AddAuthentication(options =>
        //{
        //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        //})
        ////Add  JWT Bearer
        //.AddJwtBearer(options =>
        //{
        //    options.SaveToken = true;
        //    options.RequireHttpsMetadata = false;
        //    options.TokenValidationParameters = tokenValidationParameters;
        //});
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseStaticFiles();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllers();

        app.UseEndpoints(endpoints =>
            endpoints.MapControllers()
        );
        app.Run();
        //AppDbInitializer.SeedRolesToDb(app).Wait();
    }
}