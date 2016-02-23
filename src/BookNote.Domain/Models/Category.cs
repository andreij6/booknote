using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookNote.Domain.Utils;

namespace BookNote.Domain.Models
{
    public class Category : IValidatable
    {
		public int Id { get; set; }
		public string Name { get; set; }

		public bool isValid()
		{
			return ValidationUtil.validateName(Name);
		}

		public string ValidationMessage()
		{
			return "Category Invalid: Category must be assigned a valid Name";
		}
	}
}
