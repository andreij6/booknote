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
		#region Category Specific
		[Fact]
		public void GetCategories_ByBookId_ShouldReturnAllCategoriesForBook()
		{
			ArrangeSUT();

			var target = SUT.GetByBookId(1);

			target.Should().BeOfType<List<Category>>();
			target.Should().HaveCount(x => x == 20);
		}

		[Fact]
		public void GetCategories_ByBookId_ShouldReturnNullIfBookIdNotFound()
		{
			ArrangeSUT();

			Action action = () => SUT.GetByBookId(500);

			action.ShouldThrow<Exception>().WithMessage(SUT.BOOK_NOT_FOUND);
		}
		#endregion

		#region FullDataRepoCapable
		[Fact]
		public void GetById_ShouldReturnNullWhenInvalidId()
		{
			//Arrange
			ArrangeSUT();

			//ACT
			var target = SUT.GetById(99);

			//Assert
			target.Should().BeNull("No Category was found");
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
			//Arrange
			ArrangeSUT();

			var target_id = 9;
			var title = "Charlottes Web";

			var category = new Category() { Name = title };
			//ACT
			SUT.Update(target_id, category);

			var target = SUT.GetById(target_id);

			//Assert
			target.Should().BeOfType<Category>().Which.Name.Should().Be(title);
			target.Should().BeOfType<Category>().Which.Id.Should().Be(target_id);
		}

		[Fact]
		public void Update_InvalidEntityShouldThrowException()
		{
			//Arrange
			ArrangeSUT();

			var target_id = 9;
			var title = "";

			var category = new Category() { Name = title };
			//ACT

			Action action = () => SUT.Update(target_id, category);

			action.ShouldThrow<Exception>().WithMessage(category.ValidationMessage());
		}

		[Fact]
		public void Add_EntityShouldSucceedWithValidParameter()
		{
			ArrangeSUT();

			var title = "Charlottes Web";
			var target_id = 30;

			var chapter = new Category() { Name = title, Id = target_id };

			SUT.Add(chapter);

			var target = SUT.GetById(target_id);

			target.Should().BeOfType<Category>().Which.Name.Should().Be(title);
			target.Should().BeOfType<Category>().Which.Id.Should().Be(target_id);

		}

		[Fact]
		public void Add_InvalidParameterShouldthrowException()
		{
			ArrangeSUT();

			var title = String.Empty;
			var target_id = 30;

			var category  = new Category() { Name = title, Id = target_id };

			Action action = () => SUT.Add(category);

			action.ShouldThrow<Exception>().WithMessage(category.ValidationMessage());

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
			ArrangeSUT();

			//Act
			SUT.Delete(9);

			var target = SUT.GetById(99);

			//Assert
			target.Should().BeNull();
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

			SUT = new CategoryDataRepository(context);
		}
	
		#endregion


	}
}
