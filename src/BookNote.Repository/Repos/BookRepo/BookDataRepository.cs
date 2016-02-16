using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookNote.Domain.Models;
using BookNote.Repository.Models;

namespace BookNote.Repository.Repos.BookRepo
{
	public class BookDataRepository : IBookDataRepository
	{
		private BookNoteContext db;

		public BookDataRepository(BookNoteContext context)
		{
			db = context;
		}

		public void Add(Book entity)
		{
			db.Books.Add(entity);
			Commit();
		}

		public void Delete(int id)
		{
			var entity = GetById(id);
			db.Books.Remove(entity);
			Commit();
		}

		public IEnumerable<Book> GetAll()
		{
			return db.Books.ToList();
		}

		public Book GetById(int id)
		{
			return db.Books.FirstOrDefault(b => b.Id == id);
		}

		public void Update(int id, Book entity)
		{
			var found = GetById(id);

			found.Name = entity.Name;
			found.Note = entity.Note;

			db.Books.Update(entity);
			Commit();
		}

		private void Commit()
		{
			db.SaveChanges();
		}
	}
}
