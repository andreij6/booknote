using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using BookNote.Services;
using BookNote.Services.Message.Implementations;
using BookNote.Services.Message;
using BookNote.Repository.Models;
using BookNote.Domain.Models;
using BookNote.Repository.Repos.ChapterRepo;
using BookNote.Repository.Repos.SectionRepo;
using BookNote.Repository.Repos.BookRepo;
using BookNote.Repository.Repos.CategoryRepo;
using BookNote.Services.ApplicationServices;
using React.AspNet;

namespace BookNote
{
	public class Startup
	{
		public Startup(IHostingEnvironment env)
		{
			// Set up configuration sources.

			var builder = new ConfigurationBuilder()
			    .AddJsonFile("appsettings.json")
			    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

			if (env.IsDevelopment()) {
				// For more details on using the user secret store see http://go.microsoft.com/fwlink/?LinkID=532709
				builder.AddUserSecrets();

				// This will push telemetry data through Application Insights pipeline faster, allowing you to view results immediately.
				builder.AddApplicationInsightsSettings(developerMode: true);
			}

			builder.AddEnvironmentVariables();
			Configuration = builder.Build();
		}

		public IConfigurationRoot Configuration { get; set; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			// Add framework services.
			services.AddApplicationInsightsTelemetry(Configuration);

			services.AddEntityFramework()
			    .AddSqlServer()
			    .AddDbContext<BookNoteContext>();

			services.AddIdentity<ApplicationUser, IdentityRole>()
			    .AddEntityFrameworkStores<BookNoteContext>()
			    .AddDefaultTokenProviders();

			services.AddMvc();

			services.AddReact();

			// Add application services.
			services.AddTransient<IEmailSender, AuthMessageSender>();
			services.AddTransient<ISmsSender, AuthMessageSender>();
			services.AddScoped<IBookShelfService, BookShelfService>();

			RegisterRepositories(services);
		}

		private void RegisterRepositories(IServiceCollection services)
		{
			services.AddScoped<IChapterDataRepository, ChapterDataRepo>();
			services.AddScoped<ISectionDataRepository, SectionDataRepository>();
			services.AddScoped<IBookDataRepository, BookDataRepository>();
			services.AddScoped<ICategoryDataRepository, CategoryDataRepository>();
		}

	

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
		{
			loggerFactory.AddConsole(Configuration.GetSection("Logging"));
			loggerFactory.AddDebug();

			app.UseApplicationInsightsRequestTelemetry();

			if (env.IsDevelopment()) {
				app.UseBrowserLink();
				app.UseDeveloperExceptionPage();
				app.UseDatabaseErrorPage();
			}
			else {
				app.UseExceptionHandler("/Home/Error");

				// For more details on creating database during deployment see http://go.microsoft.com/fwlink/?LinkID=615859
				try {
					using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>()
					    .CreateScope()) {
						serviceScope.ServiceProvider.GetService<BookNoteContext>()
							.Database.Migrate();
					}
				}
				catch { }
			}

			app.UseIISPlatformHandler(options => options.AuthenticationDescriptions.Clear());

			app.UseApplicationInsightsExceptionTelemetry();

			app.UseReact(config =>
			{
				// If you want to use server-side rendering of React components,
				// add all the necessary JavaScript files here. This includes
				// your components as well as all of their dependencies.
				// See http://reactjs.net/ for more information. Example:
				//config
				//    .AddScript("~/Scripts/First.jsx")
				//    .AddScript("~/Scripts/Second.jsx");

				// If you use an external build too (for example, Babel, Webpack,
				// Browserify or Gulp), you can improve performance by disabling
				// ReactJS.NET's version of Babel and loading the pre-transpiled
				// scripts. Example:
				//config
				//    .SetLoadBabel(false)
				//    .AddScriptWithoutTransform("~/Scripts/bundle.server.js");
			});

			app.UseStaticFiles();

			app.UseIdentity();

			// To configure external authentication please see http://go.microsoft.com/fwlink/?LinkID=532715

			app.UseMvc(routes => {
				routes.MapRoute(
				    name: "default",
				    template: "{controller=Home}/{action=Index}/{id?}");
			});
		}

		// Entry point for the application.
		public static void Main(string[] args) => WebApplication.Run<Startup>(args);
	}
}
