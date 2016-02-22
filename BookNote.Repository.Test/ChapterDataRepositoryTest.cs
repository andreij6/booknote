using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookNote.Domain.Models;
using BookNote.Repository.Models;
using BookNote.Repository.Repos.ChapterRepo;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace BookNote.Repository.Test
{
	public class ChapterDataRepositoryTest : DataRepositoryTest<IChapterDataRepository>, IFullDataRepositoryCapable
	{
		#region Helpers
		public override void Dispose()
		{
			SUT = null;
		}

		protected override void ArrangeSUT()
		{
			var dbContext = arrangeDB();

			SUT = new ChapterDataRepo(dbContext);
		}
		#endregion

		#region Chapter Specific
		[Fact]
		public void GetChapters_ByBookId_ShouldSucceedWithValidBookId()
		{
			ArrangeSUT();

			var target = SUT.GetChaptersByBookId(1);

			target.Should().BeOfType<List<Chapter>>();
			target.Should().HaveCount(x => x == 20);
		}

		[Fact]
		public void GetChapters_ByBookId_ShouldBeNullForInvalidBookId()
		{
			ArrangeSUT();

			Action action = () => SUT.GetChaptersByBookId(999);

			action.ShouldThrow<Exception>().WithMessage(SUT.BOOK_NOT_FOUND);

		}
		#endregion

		#region IFullDataRepoCapable
		[Fact]
		public void GetById_ShouldReturnNullWhenInvalidId()
		{
			//Arrange
			ArrangeSUT();

			//ACT
			var target = SUT.GetById(99);

			//Assert
			target.Should().BeNull("No Chapter was found");
		}

		[Fact]
		public void GetById_ShouldReturnValue()
		{
			//Arrange
			ArrangeSUT();

			//Act
			var target = SUT.GetById(9);

			//Assert
			target.Should().BeOfType<Chapter>()
				.Which.Name.Should().Be("chapter_10");
		}

		[Fact]
		public void Update_ShouldChangeEntity()
		{
			//Arrange
			ArrangeSUT();

			var target_id = 9;
			var title = "Charlottes Web";
			var notes = "good book";

			var chapter = new Chapter() { Name = title, Note = notes };
			//ACT
			SUT.Update(target_id, chapter);

			var target = SUT.GetById(target_id);

			//Assert
			target.Should().BeOfType<Chapter>().Which.Name.Should().Be(title);
			target.Should().BeOfType<Chapter>().Which.Note.Should().Be(notes);
			target.Should().BeOfType<Chapter>().Which.Id.Should().Be(target_id);
		}

		[Fact]
		public void Update_InvalidEntityShouldThrowException()
		{
			//Arrange
			ArrangeSUT();

			var target_id = 9;
			var title = "";
			var notes = "good book";

			var chapter = new Chapter() { Name = title, Note = notes };
			//ACT

			Action action = () => SUT.Update(target_id, chapter);

			action.ShouldThrow<Exception>().WithMessage(chapter.ValidationMessage());
		}

		[Fact]
		public void Add_EntityShouldSucceedWithValidParameter()
		{
			ArrangeSUT();

			var title = "Charlottes Web";
			var notes = "good book";
			var target_id = 30;

			var chapter = new Chapter() {Name = title, Note = notes, Id = target_id };

			SUT.Add(chapter);

			var target = SUT.GetById(target_id);

			target.Should().BeOfType<Chapter>().Which.Name.Should().Be(title);
			target.Should().BeOfType<Chapter>().Which.Note.Should().Be(notes);
			target.Should().BeOfType<Chapter>().Which.Id.Should().Be(target_id);
		}

		[Fact]
		public void Add_InvalidParameterShouldthrowException()
		{
			ArrangeSUT();

			var title = String.Empty;
			var notes = "good book";
			var target_id = 30;

			var chapter = new Chapter() {Name = title, Note = notes, Id = target_id };

			Action action = () => SUT.Add(chapter);

			action.ShouldThrow<Exception>().WithMessage(chapter.ValidationMessage());
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
