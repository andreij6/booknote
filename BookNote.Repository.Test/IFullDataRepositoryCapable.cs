using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BookNote.Repository.Test
{
    public interface IFullDataRepositoryCapable
    {
		void GetById_ShouldReturnNullWhenInvalidId();

		void GetById_ShouldReturnValue();

		void Update_ShouldChangeEntity();

		void Update_InvalidEntityShouldThrowException();

		void Add_EntityShouldSucceedWithValidParameter();

		void Add_InvalidParameterShouldthrowException();

		void Delete_ShouldRemoveEntity();

		[Fact]
		void Delete_ShouldDoNothingIfEntityNotFound();

    }
}
