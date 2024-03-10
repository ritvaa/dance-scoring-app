using API;
using API.Profiles;
using API.Services;
using API.Services.Interfaces;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IDancerService, DancerService>();
builder.Services.AddScoped<ICompetitionService, CompetitionService>();
builder.Services.AddScoped<ITeamService, TeamService>();
builder.Services.AddScoped<ICoachService, CoachService>();

//add controllers
builder.Services.AddControllers().AddFluentValidation();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(MapperProfiles).Assembly);

// Added configuration for PostgreSQL
var configuration = builder.Configuration;

//todo move connection string to appsettings
builder.Services.AddDbContext<DancerScoringAppDbContext>(options =>
    options.UseNpgsql("Host=localhost;Port=5432;Database=DanceScoringApp;Username=postgres;Password=P@ssw0rd"));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<DancerScoringAppDbContext>();
    dbContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "DanceScoringApp API v1"); });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();