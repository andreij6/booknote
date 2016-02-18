using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookNote.Domain.Models;
using BookNote.Repository.Models;
using BookNote.Repository.Repos.CategoryRepo;
using FluentAssertions;
using Xunit;

namespace BookNote.Repository.Test
{
	public class CategoryDataRepositoryTest : DataRepositoryTest<ICategoryDataRepository>, IFullDataRepositoryCapable
	{
		#region TESTS
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
			target.Should().BeOfType<Category>()
				.Which.Name.Should().Be("category_10");
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
		#endregion

		public override void Dispose()
		{
			SUT = null;
		}

		protected override void ArrangeSUT()
		{
			var context = arrangeDB();

			SUT = new CategoryDataRepository(context);
		}

		protected override void CreateTestData(BookNoteContext dbContext)
		{
			var id = 1;

			GenFu.GenFu.Configure<Category>()
			    .Fill(p => p.Id, () => id++)
			    .Fill(p => p.Name, () => $"category_{id}");

			var cats = GenFu.GenFu.ListOf<Category>(20);

			dbContext.Categories.AddRange(cats);
			dbContext.SaveChanges();
		}

		
	}
}
