using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookNote.Domain.Models
{
    public class Chapter
    {
		public int Id { get; set; }
		public string Name { get; set; }
		public string Note { get; set; }
		public ICollection<Section> Sections { get; set; }
	}
}
