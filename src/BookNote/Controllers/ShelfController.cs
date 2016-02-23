using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookNote.Services.ApplicationServices;
using Microsoft.AspNet.Mvc;

namespace BookNote.Controllers
{
	public class ShelfController : Controller
	{
		IBookShelfService Service;

		public ShelfController(IBookShelfService service)
		{
			Service = service;
		}

		//Book Shelf
		public IActionResult Index()
		{
			//var books = Service.GetBookShelf();

			return View();
		}

		//Book Shelf Get Book
		public IActionResult Book(int bookid)
		{
			//var book = Service.GetBook(bookid);

			return View();
		}

	
	}
}
