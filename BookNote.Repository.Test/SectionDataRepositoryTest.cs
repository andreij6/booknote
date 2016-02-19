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
		}

		[Fact]
		public void Update_InvalidEntityShouldThrowException()
		{
			throw new NotImplementedException();
		}

		[Fact]
		public void Add_EntityShouldSucceedWithValidParameter()
		{
			throw new NotImplementedException();
		}

		[Fact]
		public void Add_InvalidParameterShouldthrowException()
		{
			throw new NotImplementedException();
		}

		[Fact]
		public void Delete_ShouldRemoveEntity()
		{
			throw new NotImplementedException();
		}

		[Fact]
		public void Delete_ShouldDoNothingIfEntityNotFound()
		{
			throw new NotImplementedException();
		}
	}
}
