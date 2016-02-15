using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookNote.Domain.Models;

namespace BookNote.Repository.Repos.SectionRepo
{
	public class SectionDataRepository : ISectionDataRepository
	{
		public void Add(Section entity)
		{
			throw new NotImplementedException();
		}

		public void Delete(int id)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Section> GetAll()
		{
			throw new NotImplementedException();
		}

		public Section GetById(int id)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Section> GetSectionsByBookId(int bookId)
		{
			throw new NotImplementedException();
		}

		public IEnumerable<Section> GetSectionsByChapterId(int chapterId)
		{
			throw new NotImplementedException();
		}

		public void Update(int id, Section entity)
		{
			throw new NotImplementedException();
		}
	}
}
