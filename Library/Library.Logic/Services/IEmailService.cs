using Library.Logic.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Logic.Services
{
    public interface IEmailService
    {

        void SendMail(SendModel send);

    }
}
