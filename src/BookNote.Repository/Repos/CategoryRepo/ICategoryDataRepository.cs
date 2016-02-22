using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookNote.Domain.Models;

namespace BookNote.Repository.Repos.CategoryRepo
{
    public interface ICategoryDataRepository : IFullDataRepository<Category>
    {
		string BOOK_NOT_FOUND { get; }
		IEnumerable<Category> GetByBookId(int bookId);
    }
}
