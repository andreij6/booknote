using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookNote.Domain;

namespace BookNote.Repository.Repos
{
    public abstract class DataRepository
    {
		protected void CheckValidity(IValidatable entity)
		{
			if (!entity.isValid()) {
				throw CreateException(entity);
			}
		}

		protected Exception CreateException(IValidatable entity)
		{
			return new Exception(entity.ValidationMessage());
		}
	}
}
