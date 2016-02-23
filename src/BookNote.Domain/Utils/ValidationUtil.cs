using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookNote.Domain.Utils
{
    public class ValidationUtil
    {
		public static bool validateName(string name)
		{
			if (name == "") return false;
			if (name == String.Empty) return false;
			if (name == null) return false;

			return true;
		}
    }
}
