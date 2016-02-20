using System;
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
		public void GetSections_ByBookId_ShouldSuccedWithValidId()
		{
			throw new NotImplementedException();
		}

		public void GetSection_ByBookId_ShouldReturnNullForInvalidBookId()
		{
			throw new NotImplementedException();
		}

		public void GetSection_ByChapterId_ShouldSucceedWithValidId()
		{
			throw new NotImplementedException();
		}

		public void GetSection_ByChapterId_ShouldReturnNullForInvalidChapterId()
		{
			throw new NotImplementedException();
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
			var id = 1;

			GenFu.GenFu.Configure<Section>()
			    .Fill(p => p.Id, () => id++)
			    .Fill(p => p.Name, () => $"section_{id}");

			var sections = GenFu.GenFu.ListOf<Section>(20);

			dbContext.Sections.AddRange(sections);
			dbContext.SaveChanges();
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
