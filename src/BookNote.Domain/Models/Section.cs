using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookNote.Domain.Utils;

namespace BookNote.Domain.Models
{
    public class Section : IValidatable
    {
		public int Id { get; set; }
		public string Name { get; set; }
		public string Note { get; set; }

		public bool isValid()
		{
			return ValidationUtil.validateName(Name);
		}

		public string ValidationMessage()
		{
			return "Section Invalid: Section Must be assigned a valid Name";
		}
	}
}
