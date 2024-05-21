using Microsoft.EntityFrameworkCore;
using NZWalks.Api.Data;
using NZWalks.Api.Mappings;
using NZWalks.Api.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<NZWalksDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("NZWalksConnectionString")));

/**
* Test Implementation for another use og interface
* builder.Services.AddScoped<IRegionRepository, InMemoryRegionRepository>();
*/
builder.Services.AddScoped<IRegionRepository, SQLRegionRepository>();

builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
