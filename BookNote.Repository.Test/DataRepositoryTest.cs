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

		protected abstract void ArrangeSUT();

		protected BookNoteContext arrangeDB()
		{
			var dbContext = serviceProvider.GetRequiredService<BookNoteContext>();

			CreateTestData(dbContext);

			return dbContext;
		}

		protected void CreateTestData(BookNoteContext dbContext)
		{
			var books = CreateBooks();
			var chapters = CreateChapters();
			var sections = CreateSections();

			//Add Section to Chapters
			var firstChapter = chapters.ElementAt(0);
			var secondChapter = chapters.ElementAt(1);

			firstChapter.Sections = sections.Take(10).ToList();
			secondChapter.Sections = sections.Skip(10).Take(10).ToList();

			//Add Chapters to book
			var onlyBook = books.ElementAt(0);

			onlyBook.Chapters = chapters.ToList();

			dbContext.Books.AddRange(books);
			dbContext.Chapters.AddRange(chapters);
			dbContext.Sections.AddRange(sections);

			dbContext.SaveChanges();
		}

		private IEnumerable<Section> CreateSections()
		{
			var id = 1;

			GenFu.GenFu.Configure<Section>()
			    .Fill(p => p.Id, () => id++)
			    .Fill(p => p.Name, () => $"section_{id}");

			return GenFu.GenFu.ListOf<Section>(20);
		}

		private IEnumerable<Chapter> CreateChapters()
		{
			var c_id = 1;

			GenFu.GenFu.Configure<Chapter>()
			    .Fill(p => p.Id, () => c_id++)
			    .Fill(p => p.Name, () => $"chapter_{c_id}");

			return GenFu.GenFu.ListOf<Chapter>(20);
		}

		private IEnumerable<Book> CreateBooks()
		{
			var b_id = 1;

			GenFu.GenFu.Configure<Book>()
			    .Fill(p => p.Id, () => b_id++)
			    .Fill(p => p.Name, () => $"hello_{b_id}");

			return GenFu.GenFu.ListOf<Book>(20);
		}

	}
}
