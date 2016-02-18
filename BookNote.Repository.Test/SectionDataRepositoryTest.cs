using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookNote.Repository.Models;
using BookNote.Repository.Repos.SectionRepo;

namespace BookNote.Repository.Test
{
	public class SectionDataRepositoryTest : DataRepositoryTest<ISectionDataRepository>
	{
		public override void Dispose()
		{
			
		}

		protected override void ArrangeSUT()
		{
			throw new NotImplementedException();
		}

		protected override void CreateTestData(BookNoteContext context)
		{
			throw new NotImplementedException();
		}
	}
}
