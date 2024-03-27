using API;
using API.Profiles;
using API.Services;
using API.Services.Interfaces;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models; // Added for Swagger documentation
using Swashbuckle.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<ICoachService, CoachService>();
builder.Services.AddScoped<ICompetitionService, CompetitionService>();
builder.Services.AddScoped<IDancerService, DancerService>();
builder.Services.AddScoped<IPenaltyPointsService, PenaltyPointsService>();
builder.Services.AddScoped<IRatingService, RatingService>();
builder.Services.AddScoped<IRoutineService, RoutineService>();
builder.Services.AddScoped<ISquadService, SquadService>();
builder.Services.AddScoped<ITeamService, TeamService>();
builder.Services.AddScoped<IUserRoleService, UserRoleService>();
builder.Services.AddScoped<IUserService, UserService>();

//add controllers
builder.Services.AddControllers().AddFluentValidation();

builder.Services.AddEndpointsApiExplorer();

// Swagger configuration
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "DanceScoringApp API", Version = "v1" });
});

builder.Services.AddAutoMapper(typeof(MapperProfiles).Assembly);

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
    app.UseSwaggerUI(c => { c.SwaggerEndpoint("swagger.json", "DanceScoringApp API v1"); });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
