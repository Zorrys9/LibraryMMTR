﻿using Library.Common.ViewModels;
using Library.Extensions;
using Library.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Threading.Tasks;

namespace Library.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RaitingBooksController : Controller
    {

        private readonly IRaitingBooksService _raitingBooksService;
        private readonly ILogger _logger;
        public RaitingBooksController(IRaitingBooksService raitingBooksService, ILogger logger)
        {
            _raitingBooksService = raitingBooksService;
            _logger = logger;
        }

        /// <summary>
        /// Запрос на добавление новой оценки
        /// </summary>
        /// <param name="model"> Модель новой оценки </param>
        /// <returns> Результат запроса </returns>
        [HttpPost("[action]")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> Create([FromForm] RaitingBooksViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Данные заполнены не верно");
            }
            await _raitingBooksService.Create(model, this.CurrentUser());
            return Ok("Новая оценка успешно добавлена");
        }

        /// <summary>
        /// Запрос на изменение оценки книги
        /// </summary>
        /// <param name="model"> Измененная модель оценки </param>
        /// <returns> Результат запроса </returns>
        [HttpPut("[action]")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> Update([FromForm] RaitingBooksViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Данные заполнены не верно");
            }

            await _raitingBooksService.Update(model, this.CurrentUser());

            return Ok("Оценка успешно изменена");
        }
    }
}