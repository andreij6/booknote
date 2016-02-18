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


		public override void Dispose()
		{
			SUT = null;
		}

		protected override void CreateTestData(BookNoteContext context)
		{
			var id = 1;

			GenFu.GenFu.Configure<Chapter>()
			    .Fill(p => p.Id, () => id++)
			    .Fill(p => p.Name, () => $"chapter_{id}");

			var chapters = GenFu.GenFu.ListOf<Chapter>(20);

			context.Chapters.AddRange(chapters);
			context.SaveChanges();
		}

		protected override void ArrangeSUT()
		{
			var dbContext = arrangeDB();

			SUT = new ChapterDataRepo(dbContext);
		}

		[Fact]
		public void GetById_ShouldReturnNullWhenInvalidId()
		{
			throw new NotImplementedException();
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
			throw new NotImplementedException();
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
