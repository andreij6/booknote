using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookNote.Domain.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using Microsoft.Extensions.Configuration;

namespace BookNote.Repository.Models
{
	public class BookNoteContext : IdentityDbContext<ApplicationUser>
	{
		public IConfigurationRoot Configuration { get; set; }

		public BookNoteContext()
		{
			Database.EnsureCreated();
		}

		public DbSet<Book> Books { get; set; }
		public DbSet<Chapter> Chapters { get; set; }
		public DbSet<Section> Sections { get; set; }

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			// Customize the ASP.NET Identity model and override the defaults if needed.
			// For example, you can rename the ASP.NET Identity table names and more.
			// Add your customizations after calling base.OnModelCreating(builder);
		}

		
		//Could use if we wanted to have the connection string in this project instead of the web project
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			Configuration = Startup.BuildConfiguration();

			optionsBuilder.UseSqlServer(Configuration["Data:DefaultConnection:ConnectionString"]);

			base.OnConfiguring(optionsBuilder);
		}
		
	}
}
