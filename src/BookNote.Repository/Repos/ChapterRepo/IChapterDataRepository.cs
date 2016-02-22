using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookNote.Domain.Models;

namespace BookNote.Repository.Repos.ChapterRepo
{
    public interface IChapterDataRepository : IFullDataRepository<Chapter>
    {
		string BOOK_NOT_FOUND { get; }

		IEnumerable<Chapter> GetChaptersByBookId(int bookid);
    }
}
