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
	public class ChapterDataRepositoryTest : DataRepositoryTest<IChapterDataRepository>
	{
		[Fact]
		public void GetByIdShouldFindItemById()
		{
			//Arrange
			ArrangeSUT();

			//Act
			var target = SUT.GetById(9);

			//Assert
			target.Should().BeOfType<Chapter>()
				.Which.Name.Should().Be("hello_10");
		}


		public override void Dispose()
		{
			SUT = null;
		}

		protected override void CreateTestData(BookNoteContext context)
		{
			throw new NotImplementedException();
		}

		protected override void ArrangeSUT()
		{
			var dbContext = arrangeDB();

			SUT = new ChapterDataRepo(dbContext);
		}
	}
}
