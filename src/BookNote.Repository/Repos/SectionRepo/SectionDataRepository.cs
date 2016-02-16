using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookNote.Domain.Models;
using BookNote.Repository.Models;
using Microsoft.Data.Entity;

namespace BookNote.Repository.Repos.SectionRepo
{
	public class SectionDataRepository : ISectionDataRepository
	{
		private BookNoteContext db;

		public SectionDataRepository(BookNoteContext context)
		{
			db = context;
		}

		public void Add(Section entity)
		{
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

			var result = new List<Section>();

			foreach(var chapter in book.Chapters) {
				result.AddRange(chapter.Sections);
			}

			return result;
		}

		public IEnumerable<Section> GetSectionsByChapterId(int chapterId)
		{
			var chapter = db.Chapters.Include(c => c.Sections).FirstOrDefault(c => c.Id == chapterId);

			if(chapter == null) {
				return Enumerable.Empty<Section>();
			}

			return chapter.Sections;
		}

		public void Update(int id, Section entity)
		{
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
