using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookNote.Domain.Models
{
    public class Section : IValidatable
    {
		public int Id { get; set; }
		public string Name { get; set; }
		public string Note { get; set; }

		public bool isValid()
		{
			if (Name == "") return false;
			if (Name == String.Empty) return false;
			if (Name == null) return false;

			return true;
		}

		public string ValidationMessage()
		{
			return "Section Invalid: Section Must be assigned a valid Name";
		}
	}
}
