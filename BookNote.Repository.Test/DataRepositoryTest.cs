using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Microsoft.Data.Entity;
using BookNote.Domain.Models;
using BookNote.Repository.Models;
using BookNote.Repository.Repos.BookRepo;
using Microsoft.Extensions.DependencyInjection;
using FluentAssertions;

namespace BookNote.Repository.Test
{
    public abstract class DataRepositoryTest<T> : IDisposable
    {
		protected readonly IServiceProvider serviceProvider;
		protected T SUT;

		public DataRepositoryTest()
		{
			var services = new ServiceCollection();

			services.AddEntityFramework()
				.AddInMemoryDatabase()
				.AddDbContext<BookNoteContext>(options => options.UseInMemoryDatabase());

			serviceProvider = services.BuildServiceProvider();
		}

		public abstract void Dispose();
		protected abstract void CreateTestData(BookNoteContext context);

		protected abstract void ArrangeSUT();

		protected BookNoteContext arrangeDB()
		{
			var dbContext = serviceProvider.GetRequiredService<BookNoteContext>();

			CreateTestData(dbContext);

			return dbContext;
		}

	}
}
