using Library.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Services.Services
{
    public interface IEmailService
    {

        void SendMail(SendModel send);

    }
}
