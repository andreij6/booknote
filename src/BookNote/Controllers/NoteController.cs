using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookNote.Services.ApplicationServices;
using Microsoft.AspNet.Mvc;

namespace BookNote.Controllers
{
	public class NoteController : Controller
	{

		public NoteController()
		{
		}

	
		public IActionResult Index()
		{
			return View();
		}
		

	
	}
}
