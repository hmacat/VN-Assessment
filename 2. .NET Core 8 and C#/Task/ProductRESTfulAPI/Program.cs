
using Microsoft.EntityFrameworkCore;
using ProductRESTfulAPI.DbContexts;
using ProductRESTfulAPI.Middlewares;
using ProductRESTfulAPI.Models;
using ProductRESTfulAPI.Services;

namespace ProductRESTfulAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

            // Add in-memory database for simplicity.
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseInMemoryDatabase("AppDatabase"));

            // Add repository services
            builder.Services.AddTransient<IRepository<Product>, ProductRepository>();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            //add error handling middleware to the end of the middlewares.
            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.MapControllers();

            app.Run();
        }
    }
}
