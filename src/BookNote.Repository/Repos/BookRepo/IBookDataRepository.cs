using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookNote.Domain.Models;

namespace BookNote.Repository.Repos.BookRepo
{
    public interface IBookDataRepository : IFullDataRepository<Book>
    {
		string BOOK_NOT_FOUND { get; }
    }
}
