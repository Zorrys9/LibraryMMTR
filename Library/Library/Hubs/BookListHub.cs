using Library.Common.Models;
using Library.Common.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Hubs
{
    /// <summary>
    /// Узел для обновления списка книг в режиме "Онлайн"
    /// </summary>
    public class BookListHub : Hub
    {
        public async Task RefreshList()
        {
            await Clients.All.SendAsync("RefreshList");
        }
    }
}
