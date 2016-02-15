using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookNote.Domain.Models;

namespace BookNote.Repository.Repos.ChapterRepo
{
	public class ChapterDataRepo : IChapterDataRepository
	{
		public void Add(Chapter entity)
		{
			throw new NotImplementedException();
		}

		public void Delete(int id)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Chapter> GetAll()
		{
			throw new NotImplementedException();
		}

		public Chapter GetById(int id)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Chapter> GetChaptersByBookId(int bookid)
		{
			throw new NotImplementedException();
		}

		public void Update(int id, Chapter entity)
		{
			throw new NotImplementedException();
		}
	}
}
