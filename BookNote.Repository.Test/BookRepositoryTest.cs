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
	// This project can output the Class library as a NuGet Package.
	// To enable this option, right-click on the project and select the Properties menu item. In the Build tab select "Produce outputs on build".
	public class BookRepositoryTest : DataRepositoryTest<IBookDataRepository>, IFullDataRepositoryCapable
	{

		public override void Dispose()
		{
			SUT = null;
		}

		protected override void ArrangeSUT()
		{
			var dbContext = arrangeDB();

			SUT = new BookDataRepository(dbContext);
		}

		[Fact]
		public void GetById_ShouldReturnNullWhenInvalidId()
		{
			//Arrange
			ArrangeSUT();

			//ACT
			var target = SUT.GetById(99);

			//Assert
			target.Should().BeNull("No Book was found");
		}

		[Fact]
		public void GetById_ShouldReturnValue()
		{
			//Arrange
			ArrangeSUT();

			//Act
			var target = SUT.GetById(9);

			//Assert
			target.Should().BeOfType<Book>()
				.Which.Name.Should().Be("hello_10");
		}

		[Fact]
		public void Update_ShouldChangeEntity()
		{
			//Arrange
			ArrangeSUT();

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
		public void Update_InvalidEntityShouldThrowException()
		{
			//Arrange
			ArrangeSUT();

			var target_id = 9;
			var title = "";
			var notes = "good book";

			var book = new Book(title) { Note = notes };
			//ACT

			Action action = () => SUT.Update(target_id, book);

			action.ShouldThrow<Exception>().WithMessage(book.ValidationMessage());
		}

		[Fact]
		public void Add_EntityShouldSucceedWithValidParameter()
		{
			ArrangeSUT();

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
		public void Add_InvalidParameterShouldthrowException()
		{
			ArrangeSUT();

			var title = String.Empty;
			var notes = "good book";
			var target_id = 30;

			var book = new Book(title) { Note = notes, Id = target_id };

			Action action = () => SUT.Add(book);

			action.ShouldThrow<Exception>().WithMessage(book.ValidationMessage());
		}

		[Fact]
		public void Delete_ShouldRemoveEntity()
		{
			//Arrange
			ArrangeSUT();

			//Act
			SUT.Delete(9);

			var target = SUT.GetById(9);

			//Assert
			target.Should().BeNull();
		}

		[Fact]
		public void Delete_ShouldDoNothingIfEntityNotFound()
		{
			//Arrange
			ArrangeSUT();

			//Act
			SUT.Delete(9);

			var target = SUT.GetById(99);

			//Assert
			target.Should().BeNull();
		}
	}
}
