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
		protected IServiceProvider serviceProvider;
		protected T SUT;
		private BookNoteContext DBContext;

		public DataRepositoryTest()
		{
			var services = new ServiceCollection();

			services.AddEntityFramework()
				.AddInMemoryDatabase()
				.AddDbContext<BookNoteContext>(options => options.UseInMemoryDatabase());

			serviceProvider = services.BuildServiceProvider();
		}

		public virtual void Dispose() {
			DeleteSections();
			DeleteCategories();
			DeleteChapters();
			DeleteBooks();

			DBContext.Database.EnsureDeleted();
			DBContext.Dispose();
			DBContext = null;

			serviceProvider = null;
		}

		private void DeleteBooks()
		{
			var books = DBContext.Books.ToList();

			DBContext.Books.RemoveRange(books);

			DBContext.SaveChanges();
		}

		private void DeleteChapters()
		{
			var chapters = DBContext.Chapters.ToList();

			DBContext.Chapters.RemoveRange(chapters);

			DBContext.SaveChanges();
		}

		private void DeleteCategories()
		{
			var categories = DBContext.Categories.ToList();

			DBContext.Categories.RemoveRange(categories);

			DBContext.SaveChanges();
		}

		private void DeleteSections()
		{
			var sections = DBContext.Sections.ToList();

			DBContext.Sections.RemoveRange(sections);

			DBContext.SaveChanges();
		}

		protected abstract void ArrangeSUT();

		protected BookNoteContext arrangeDB()
		{
			DBContext = serviceProvider.GetRequiredService<BookNoteContext>();

			CreateTestData(DBContext);

			return DBContext;
		}

		protected void CreateTestData(BookNoteContext dbContext)
		{
			var books = CreateBooks();
			var chapters = CreateChapters();
			var sections = CreateSections();
			var categories = CreateCategories();

			//Add Section to Chapters
			var firstChapter = chapters.ElementAt(0);
			var secondChapter = chapters.ElementAt(1);

			firstChapter.Sections = sections.Take(10).ToList();
			secondChapter.Sections = sections.Skip(10).Take(10).ToList();

			//Add Chapters to book
			var firstBook = books.ElementAt(0);

			firstBook.Categories = categories.ToList();
			firstBook.Chapters = chapters.ToList();

			dbContext.Books.AddRange(books);
			dbContext.Chapters.AddRange(chapters);
			dbContext.Sections.AddRange(sections);

			dbContext.SaveChanges();
		}

		private IEnumerable<Category> CreateCategories()
		{
			var c_id = 1;

			GenFu.GenFu.Configure<Category>()
			    .Fill(p => p.Id, () => c_id++)
			    .Fill(p => p.Name, () => $"category_{c_id}");

			return GenFu.GenFu.ListOf<Category>(20);
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
