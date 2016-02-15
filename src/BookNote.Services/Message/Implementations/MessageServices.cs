﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookNote.Services.Message.Implementations
{
	public class AuthMessageSender : IEmailSender, ISmsSender
	{
		public Task SendEmailAsync(string email, string subject, string message)
		{
			return Task.FromResult(0);
		}

		public Task SendSmsAsync(string number, string message)
		{
			return Task.FromResult(0);
		}
	}
}
