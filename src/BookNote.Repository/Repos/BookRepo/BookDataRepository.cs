using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookNote.Domain.Models;

namespace BookNote.Repository.Repos.BookRepo
{
	public class BookDataRepository : IFullDataRepository<Book>
	{
		public void Add(Book entity)
		{
			throw new NotImplementedException();
		}

		public void Delete(int id)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Book> GetAll()
		{
			throw new NotImplementedException();
		}

		public Book GetById(int id)
		{
			throw new NotImplementedException();
		}

		public void Update(int id, Book entity)
		{
			throw new NotImplementedException();
		}
	}
}
