using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Logic.EventBus
{
    public interface IMailSend
    {

        string MailTo { get;}
        string Subject { get; }
        string Body { get;}

    }
}
