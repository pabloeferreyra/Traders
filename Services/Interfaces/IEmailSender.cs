﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Traders.Models;

namespace Traders.Services
{
    public interface IEmailSender
    {
        Task SendEmailAsync(MailRequestViewModel mailRequest);
    }
}
