using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace AD.Template.Dotnet.API.V1_0
{
    public class DataBaseContext : DbContext
    {
        public static IConfiguration Configuration { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer(get());

        public string get()
        {
            var build = new ConfigurationBuilder()
         .SetBasePath(Directory.GetCurrentDirectory())
         .AddJsonFile("appsettings.json");
            Configuration = build.Build();
            var configurationSection = Configuration.GetSection("ConnectionStrings:DefaultConnection");
            return configurationSection.Value;
        }
    }
}