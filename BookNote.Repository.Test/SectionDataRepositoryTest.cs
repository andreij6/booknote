﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookNote.Domain.Models;
using BookNote.Repository.Models;
using BookNote.Repository.Repos.SectionRepo;
using FluentAssertions;
using Xunit;

namespace BookNote.Repository.Test
{
	public class SectionDataRepositoryTest : DataRepositoryTest<ISectionDataRepository>, IFullDataRepositoryCapable
	{
		//Test until Fear turns to boredom, Clean Code

		#region Section Specific
		[Fact]
		public void GetSections_ByBookId_ShouldSuccedWithValidId()
		{
			ArrangeSUT();

			var target = SUT.GetSectionsByBookId(1);

			target.Should().BeOfType<IEnumerable<Section>>();
			target.Should().HaveCount(x => x == 20);
		}

		[Fact]
		public void GetSection_ByBookId_ShouldThrowExpectionInvalidBookId()
		{
			ArrangeSUT();

			Action action = () => SUT.GetSectionsByBookId(5);

			action.ShouldThrow<Exception>().WithMessage(SectionDataRepository.BOOK_NOT_FOUND);
		}

		[Fact]
		public void GetSection_ByChapterId_ShouldSucceedWithValidId()
		{
			ArrangeSUT();

			var target = SUT.GetSectionsByChapterId(1);

			target.Should().BeOfType<List<Section>>();  //not sure why IEnumerable<Section> didnt work
			target.Should().HaveCount(x => x == 10);
		}

		[Fact]
		public void GetSection_ByChapterId_ShouldReturnNullForInvalidChapterId()
		{
			ArrangeSUT();

			Action action = () => SUT.GetSectionsByChapterId(99);

			action.ShouldThrow<Exception>().WithMessage(SectionDataRepository.CHAPTER_NOT_FOUND);
		}
		#endregion

		#region Helpers
		public override void Dispose()
		{
			SUT = null;
		}

		protected override void ArrangeSUT()
		{
			var context = arrangeDB();

			SUT = new SectionDataRepository(context);
		}

		protected override void CreateTestData(BookNoteContext dbContext)
		{
			var books = CreateBooks();
			var chapters = CreateChapters();
			var sections = CreateSections();

			//Add Section to Chapters
			var firstChapter = chapters.ElementAt(0);
			var secondChapter = chapters.ElementAt(1);

			firstChapter.Sections = sections.Take(10).ToArray();
			secondChapter.Sections = sections.Skip(10).Take(10).ToArray();

			//Add Chapters to book
			var onlyBook = books.ElementAt(0);

			onlyBook.Chapters = chapters.ToArray();

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

			return GenFu.GenFu.ListOf<Chapter>(2);
		}

		private IEnumerable<Book> CreateBooks()
		{
			var b_id = 1;

			GenFu.GenFu.Configure<Book>()
			    .Fill(p => p.Id, () => b_id++)
			    .Fill(p => p.Name, () => $"hello_{b_id}");

			return GenFu.GenFu.ListOf<Book>(1);
		}
		#endregion

		#region IFullDataCapable tests
		[Fact]
		public void GetById_ShouldReturnNullWhenInvalidId()
		{
			//Arrange
			ArrangeSUT();

			//ACT
			var target = SUT.GetById(99);

			//Assert
			target.Should().BeNull("No Section was found");
		}

		[Fact]
		public void GetById_ShouldReturnValue()
		{
			ArrangeSUT();

			var target = SUT.GetById(9);

			target.Should().BeOfType<Section>()
				.Which.Name.Should().Be("section_10");
		}

		[Fact]
		public void Update_ShouldChangeEntity()
		{
			//Arrange
			ArrangeSUT();

			var target_id = 9;
			var title = "Charlottes Web";
			var notes = "good book";

			var section = new Section() { Name = title, Note = notes };
			//ACT
			SUT.Update(target_id, section);

			var target = SUT.GetById(target_id);

			//Assert
			target.Should().BeOfType<Section>().Which.Name.Should().Be(title);
			target.Should().BeOfType<Section>().Which.Note.Should().Be(notes);
			target.Should().BeOfType<Section>().Which.Id.Should().Be(target_id);

		}

		[Fact]
		public void Update_InvalidEntityShouldThrowException()
		{
			//Arrange
			ArrangeSUT();

			var target_id = 9;
			var title = "";
			var notes = "good book";

			var section = new Section() { Name = title, Note = notes };
			//ACT

			Action action = () => SUT.Update(target_id, section);

			action.ShouldThrow<Exception>().WithMessage(section.ValidationMessage());

		}

		[Fact]
		public void Add_EntityShouldSucceedWithValidParameter()
		{
			ArrangeSUT();

			var title = "Charlottes Web";
			var notes = "good book";
			var target_id = 30;

			var section = new Section() { Name = title, Note = notes, Id = target_id };

			SUT.Add(section);

			var target = SUT.GetById(target_id);

			target.Should().BeOfType<Section>().Which.Name.Should().Be(title);
			target.Should().BeOfType<Section>().Which.Note.Should().Be(notes);
			target.Should().BeOfType<Section>().Which.Id.Should().Be(target_id);

		}

		[Fact]
		public void Add_InvalidParameterShouldthrowException()
		{
			ArrangeSUT();

			var title = String.Empty;
			var notes = "good book";
			var target_id = 30;

			var section = new Section() { Name = title, Note = notes, Id = target_id };

			Action action = () => SUT.Add(section);

			action.ShouldThrow<Exception>().WithMessage(section.ValidationMessage());

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
		#endregion
	}
}
