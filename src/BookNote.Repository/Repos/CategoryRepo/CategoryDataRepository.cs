using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookNote.Domain.Models;
using BookNote.Repository.Models;

namespace BookNote.Repository.Repos.CategoryRepo
{
	public class CategoryDataRepository : ICategoryDataRepository
	{
		public readonly BookNoteContext db;
		public CategoryDataRepository(BookNoteContext context)
		{
			db = context;
		}

		public void Add(Category entity)
		{
			throw new NotImplementedException();
		}

		public void Delete(int id)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Category> GetAll()
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Category> GetByBookId(int bookId)
		{
			throw new NotImplementedException();
		}

		public Category GetById(int id)
		{
			throw new NotImplementedException();
		}

		public void Update(int id, Category entity)
		{
			throw new NotImplementedException();
		}
	}
}
