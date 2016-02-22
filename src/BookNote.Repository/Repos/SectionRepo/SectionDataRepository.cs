using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookNote.Domain.Models;
using BookNote.Repository.Models;
using Microsoft.Data.Entity;

namespace BookNote.Repository.Repos.SectionRepo
{
	public class SectionDataRepository : DataRepository, ISectionDataRepository
	{
		private BookNoteContext db;

		public SectionDataRepository(BookNoteContext context)
		{
			db = context;
		}

		private static string BOOK_NOT_FOUND = "Book not found for specified Id";
		public static string CHAPTER_NOT_FOUND = "Chapter not found for specified Id";

		string ISectionDataRepository.BOOK_NOT_FOUND
		{
			get
			{
				return BOOK_NOT_FOUND;
			}
		}

		public void Add(Section entity)
		{
			CheckValidity(entity);
			db.Sections.Add(entity);
			Commit();
		}

		public void Delete(int id)
		{
			var entity = GetById(id);

			db.Sections.Remove(entity);

			Commit();
		}

		public IEnumerable<Section> GetAll()
		{
			return db.Sections.ToList();
		}

		public Section GetById(int id)
		{
			return db.Sections.FirstOrDefault(s => s.Id == id);
		}

		public IEnumerable<Section> GetSectionsByBookId(int bookId)
		{
			var book = db.Books
						.Include(b => b.Chapters)
						.FirstOrDefault(b => b.Id == bookId);

			if (book == null) throw CreateException(BOOK_NOT_FOUND);

			var result = new List<Section>();

			foreach(var chapter in book.Chapters) {
				if (chapter.Sections != null) {
					result.AddRange(chapter.Sections);
				}
			}

			return result;
		}

		public IEnumerable<Section> GetSectionsByChapterId(int chapterId)
		{
			var chapter = db.Chapters.Include(c => c.Sections).FirstOrDefault(c => c.Id == chapterId);

			if (chapter == null) throw CreateException(CHAPTER_NOT_FOUND);

			return chapter.Sections.ToList();
		}

		public void Update(int id, Section entity)
		{
			CheckValidity(entity);

			var section = GetById(id);

			section.Name = entity.Name;
			section.Note = entity.Note;

			db.Sections.Update(section);
			Commit();
		}

		private void Commit()
		{
			db.SaveChanges();
		}
	}
}
