using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookNote.Domain;
using BookNote.Domain.Models;
using BookNote.Repository.Models;
using Microsoft.Data.Entity;

namespace BookNote.Repository.Repos.ChapterRepo
{
	public class ChapterDataRepo : DataRepository, IChapterDataRepository
	{
		BookNoteContext db;

		public ChapterDataRepo(BookNoteContext context)
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

		public void Add(Chapter entity)
		{
			CheckValidity(entity);

			db.Chapters.Add(entity);
			Commit();
		}

		public void Delete(int id)
		{
			var entity = GetById(id);

			db.Chapters.Remove(entity);

			Commit();
		}

		public IEnumerable<Chapter> GetAll()
		{
			return db.Chapters.ToList();
		}

		public Chapter GetById(int id)
		{
			return db.Chapters.FirstOrDefault(c => c.Id == id);
		}

		public IEnumerable<Chapter> GetChaptersByBookId(int bookid)
		{
			var book = db.Books.Include(b => b.Chapters)
					 .FirstOrDefault(b => b.Id == bookid);

			if (book == null) throw CreateException(BOOK_NOT_FOUND);

			return book.Chapters;
		}

		public void Update(int id, Chapter entity)
		{
			CheckValidity(entity);

			var found = GetById(id);

			found.Name = entity.Name;
			found.Note = entity.Note;

			db.Chapters.Update(found);
			Commit();
		}
	
		private void Commit()
		{
			db.SaveChanges();
		}
	}
}
