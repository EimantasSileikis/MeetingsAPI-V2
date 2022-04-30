using MeetingsAPI_V2.Data;
using MeetingsAPI_V2.DatabaseSeed;
using MeetingsAPI_V2.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("MeetingsDatabase"), options => options.EnableRetryOnFailure());
});

builder.Services.AddScoped<IMeetingRepository, MeetingRepository>();

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

Seed.PrepSeed(app);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

