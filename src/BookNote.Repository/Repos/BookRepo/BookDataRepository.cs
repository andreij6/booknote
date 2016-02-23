using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookNote.Domain;
using BookNote.Domain.Models;
using BookNote.Repository.Models;

namespace BookNote.Repository.Repos.BookRepo
{
	public class BookDataRepository : DataRepository, IBookDataRepository
	{
		private BookNoteContext db;

		public BookDataRepository(BookNoteContext context)
		{
			db = context;
		}

		public string BOOK_NOT_FOUND
		{
			get
			{
				return "Book not found for specified Id";
			}
		}

		public void Add(Book entity)
		{
			CheckValidity(entity);

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
			CheckValidity(entity);

			var found = GetById(id);

			if (found == null) throw CreateException(BOOK_NOT_FOUND);

			found.Name = entity.Name;
			found.Note = entity.Note;

			db.Books.Update(found);
			Commit();
		}

		private void Commit()
		{
			db.SaveChanges();
		}

	}
}
