using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookNote.Domain.Models;

namespace BookNote.Repository.Repos.SectionRepo
{
    public interface ISectionDataRepository : IFullDataRepository<Section>
    {
		IEnumerable<Section> GetSectionsByChapterId(int chapterId);

		IEnumerable<Section> GetSectionsByBookId(int bookId);
    }
}
