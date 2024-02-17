//using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.AspNetCore.Builder;

//namespace DanceScoringApp {
//    public class Startup {
//        private readonly IConfiguration _configuration;

//        public Startup(IConfiguration configuration) {
//            _configuration = configuration;
//        }

//        public void ConfigureServices(IServiceCollection services) {
//            services.AddDbContext<DancerScoringAppDbContext>(options =>
//                options.UseNpgsql("Host=localhost;Port=5432;Database=DanceScoringApp;Username=postgres;Password=P@ssw0rd")); // Ensure correct context name

//        }

//        public void Configure(IApplicationBuilder app) {
//            // Configure request pipeline here
//        }
//    }
//}

