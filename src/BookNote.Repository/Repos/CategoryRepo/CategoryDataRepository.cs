﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookNote.Domain.Models;
using BookNote.Repository.Models;

namespace BookNote.Repository.Repos.CategoryRepo
{
	public class CategoryDataRepository : DataRepository, ICategoryDataRepository
	{
		public readonly BookNoteContext db;
		public CategoryDataRepository(BookNoteContext context)
		{
			db = context;
		}

		public string BOOK_NOT_FOUND
		{
			get
			{
				return "Book not Found for specified Id";
			}
		}

		public void Add(Category entity)
		{
			CheckValidity(entity);

			db.Categories.Add(entity);
			Commit();
		}

		public void Delete(int id)
		{
			var entity = GetById(id);

			db.Categories.Remove(entity);

			Commit();
		}

		public IEnumerable<Category> GetAll()
		{
			return db.Categories.ToList();
		}

		public IEnumerable<Category> GetByBookId(int bookId)
		{
			var book = db.Books.FirstOrDefault(b => b.Id == bookId);

			if (book == null) throw CreateException(BOOK_NOT_FOUND);

			return book.Categories;
		}

		public Category GetById(int id)
		{
			return db.Categories.FirstOrDefault(c => c.Id == id);
		}

		public void Update(int id, Category entity)
		{
			CheckValidity(entity);

			var found = GetById(id);

			found.Name = entity.Name;

			db.Categories.Update(found);
			Commit();
		}

		private void Commit()
		{
			db.SaveChanges();
		}
	}
}
