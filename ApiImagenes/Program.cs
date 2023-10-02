using ApiImagenes.Data;
using Microsoft.EntityFrameworkCore;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.Extensions.DependencyInjection;
using ApiImagenes.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAWSService<IAmazonS3>();
builder.Services.AddTransient<ServiceS3>();

builder.Services.AddDbContext<ApiImagenesContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ApiImagenesContext")));
builder.Services.AddControllers();


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

app.MapControllers();

app.Run();
