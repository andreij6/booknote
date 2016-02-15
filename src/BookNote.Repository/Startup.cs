using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace BookNote.Repository
{
    public class Startup
    {
		public static IConfigurationRoot BuildConfiguration()
		{
			var builder = new ConfigurationBuilder()
						 .AddJsonFile("appsettings.json");

			return builder.Build();
		}
    }
}
