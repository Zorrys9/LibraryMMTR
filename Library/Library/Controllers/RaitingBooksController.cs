using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Common.ViewModels;
using Library.Extensions;
using Library.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RaitingBooksController : Controller
    {

        private readonly IRaitingBooksService _raitingBooksService;
        public RaitingBooksController(IRaitingBooksService raitingBooksService)
        {

            _raitingBooksService = raitingBooksService;

        }




        /// <summary>
        /// Запрос на добавление новой оценки
        /// </summary>
        /// <param name="model"> Модель новой оценки </param>
        /// <returns> Результат запроса </returns>
        [HttpPost("[action]")]
        [Authorize(Roles = "Admin, User")]
        public IActionResult Create([FromForm]RaitingBooksViewModel model)
        {
            try
            {

                if (ModelState.IsValid)
                {

                    var result = _raitingBooksService.Create(model, this.CurrentUser());

                    if (result != null)
                    {

                        return Ok();

                    }
                    else
                    {

                        throw new Exception("При добавлении оценки возникла ошибка");

                    }

                }
                else
                {

                    throw new Exception("Данные заполнены не верно");

                }

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);

            }


        }

        /// <summary>
        /// Запрос на изменение оценки книги
        /// </summary>
        /// <param name="model"> Измененная модель оценки </param>
        /// <returns> Результат запроса </returns>
        [HttpPost("[action]")]
        [Authorize(Roles = "Admin, User")]
        public IActionResult Update([FromForm]RaitingBooksViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var result = _raitingBooksService.Update(model, this.CurrentUser());

                    if (result != null)
                    {

                        return Ok();

                    }
                    else
                    {

                        throw new Exception("При изменении оценки произошла ошибка");

                    }
                }
                else
                {

                    throw new Exception("Данные заполнены не верно");

                }

            }
            catch(Exception ex)
            {

                return BadRequest(ex.Message);

            }
        }

    }
}