﻿using System;
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
	// This project can output the Class library as a NuGet Package.
	// To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
	public class BookRepositoryTest : IDisposable
	{
		private readonly IServiceProvider serviceProvider;
		private IBookDataRepository SUT;

		public BookRepositoryTest()
		{
			var services = new ServiceCollection();

			services.AddEntityFramework()
				.AddInMemoryDatabase()
				.AddDbContext<BookNoteContext>(options => options.UseInMemoryDatabase());

			serviceProvider = services.BuildServiceProvider();
		}

		[Fact]
		public void GetByIdShouldFindItemById()
		{
			//Arrange
			var dbContext = serviceProvider.GetRequiredService<BookNoteContext>();

			CreateTestData(dbContext);

			SUT = new BookDataRepository(dbContext);

			//Act
			var target = SUT.GetById(9);

			//Assert
			target.Should().BeOfType<Book>()
				.Which.Name.Should().Be("hello_10");
		}

		[Fact]
		public void GetByIdShouldReturnNullWhenBookNotFound()
		{
			//Arrange
			var dbContext = serviceProvider.GetRequiredService<BookNoteContext>();

			CreateTestData(dbContext);

			SUT = new BookDataRepository(dbContext);

			//ACT
			var target = SUT.GetById(99);

			//Assert
			target.Should().BeNull("No Book was found");
		}

		[Fact]
		public void UpdateBookShouldChangeName()
		{
			//Arrange
			var dbContext = serviceProvider.GetRequiredService<BookNoteContext>();

			CreateTestData(dbContext);

			SUT = new BookDataRepository(dbContext);

			var target_id = 9;
			var title = "Charlottes Web";
			var notes = "good book";

			var book = new Book(title) { Note = notes };
			//ACT
			SUT.Update(target_id, book);

			var target = SUT.GetById(target_id);

			//Assert
			target.Should().BeOfType<Book>().Which.Name.Should().Be(title);
			target.Should().BeOfType<Book>().Which.Note.Should().Be(notes);
			target.Should().BeOfType<Book>().Which.Id.Should().Be(target_id);

		}

		[Fact]
		public void AddBookShouldSucceedWithValidBook()
		{
			var dbContext = serviceProvider.GetRequiredService<BookNoteContext>();

			CreateTestData(dbContext);

			SUT = new BookDataRepository(dbContext);

			var title = "Charlottes Web";
			var notes = "good book";
			var target_id = 30;

			var book = new Book(title) { Note = notes, Id = target_id };

			SUT.Add(book);

			var target = SUT.GetById(target_id);

			target.Should().BeOfType<Book>().Which.Name.Should().Be(title);
			target.Should().BeOfType<Book>().Which.Note.Should().Be(notes);
			target.Should().BeOfType<Book>().Which.Id.Should().Be(target_id);
		}

		[Fact]
		public void AddBookWithoutNameShouldThrowException()
		{
			var dbContext = serviceProvider.GetRequiredService<BookNoteContext>();

			CreateTestData(dbContext);

			SUT = new BookDataRepository(dbContext);

			var title = String.Empty;
			var notes = "good book";
			var target_id = 30;

			var book = new Book(title) { Note = notes, Id = target_id };

			Action action = () => SUT.Add(book);

			action.ShouldThrow<Exception>().WithMessage(book.ValidationMessage());
		}

		[Fact]
		public void DeleteShouldRemoveBook()
		{
			//Arrange
			var dbContext = serviceProvider.GetRequiredService<BookNoteContext>();

			CreateTestData(dbContext);

			SUT = new BookDataRepository(dbContext);

			//Act
			SUT.Delete(9);

			var target = SUT.GetById(9);

			//Assert
			target.Should().BeNull();
		}

		[Fact]
		public void DeleteShouldDoNothingIfNoBookFoundForGivenId()
		{
			//Arrange
			var dbContext = serviceProvider.GetRequiredService<BookNoteContext>();

			CreateTestData(dbContext);

			SUT = new BookDataRepository(dbContext);

			//Act
			SUT.Delete(9);

			var target = SUT.GetById(99);

			//Assert
			target.Should().BeNull();
		}

		private void CreateTestData(BookNoteContext dbContext)
		{
			var i = 0;
			var id = 1;

			GenFu.GenFu.Configure<Book>()
			    .Fill(p => p.Id, () => id++)
			    .Fill(p => p.Name, () => $"hello_{id}");

			var books = GenFu.GenFu.ListOf<Book>(20);

			dbContext.Books.AddRange(books);
			dbContext.SaveChanges();
		}

		public void Dispose()
		{
			SUT = null;
		}
	}
}