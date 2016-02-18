using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookNote.Domain.Models;
using BookNote.Repository.Models;
using Microsoft.Data.Entity;

namespace BookNote.Repository.Repos.ChapterRepo
{
	public class ChapterDataRepo : IChapterDataRepository
	{
		BookNoteContext db;

		public ChapterDataRepo(BookNoteContext context)
		{
			db = context;
		}

		public void Add(Chapter entity)
		{
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
			return db.Books.Include(b => b.Chapters)
				      .FirstOrDefault(b => b.Id == bookid)
					 .Chapters;
		}

		public void Update(int id, Chapter entity)
		{
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
