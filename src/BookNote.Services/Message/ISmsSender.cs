using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookNote.Services.Message
{
    public interface ISmsSender
    {
		Task SendSmsAsync(string number, string message);
    }
}
