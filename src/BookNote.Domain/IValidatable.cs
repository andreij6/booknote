using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookNote.Domain
{
    public interface IValidatable
    {
		bool isValid();

		string ValidationMessage();
    }
}
