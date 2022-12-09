using ASP.BlogArticle.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASP.BlogArticle.DataAccess.ApplicationDbContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options) 
        {
           
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=MERT;Database=ASP.BlogArticleDb;User Id=mert;Password=sapass; TrustServerCertificate=True;");
        }

        public DbSet<Article> Articles { get; set; }
    }
}
