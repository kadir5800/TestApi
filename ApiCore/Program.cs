using ApiCore.Infrastructure;
using ApiCore.Infrastructure.Middleware;
using ApiCore.Infrastructure.Swager;
using EntityFramework.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.OperationFilter<HeaderFilter>();
});
builder.Services.AddEntityFrameworkCollection();

builder.Services.AddDbContext<ZDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseActionExtensions();
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseActionExtensions();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
