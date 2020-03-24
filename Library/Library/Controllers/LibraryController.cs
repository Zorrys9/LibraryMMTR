using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Library.Common.ViewModels;
using Library.Logic.Services;
using Library.Logic.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Library.Common.Enums;

namespace Library.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LibraryController : ControllerBase
    {
        private readonly ILibraryService _libraryService;
        private readonly IUsersService _usersService;
        public LibraryController(ILibraryService libraryService, IUsersService usersService)
        {
            _libraryService = libraryService;
            _usersService = usersService;
        }
        /// <summary>
        /// Показывает страницу создания новой книги
        /// </summary>
        /// <returns> Результат создания </returns>
        [HttpGet("Books/[action]")]
        [Authorize(Roles="Director")]
        public IActionResult CreateBook()
        {
            return new ViewResult();
        }

        /// <summary>
        /// Показывает карточку книги
        /// </summary>
        /// <param name="bookId"> Id книги </param>
        /// <returns> Результат получения книги по Id </returns>
        [HttpGet("Books/[action]")]
        [Authorize(Roles = "Director")]
        public IActionResult BookCard(Guid bookId)
        {
                var result = _libraryService.GetBook(bookId);
                if (result != null)
                    return new OkObjectResult(result);
                else return new BadRequestObjectResult(bookId);
        }

        /// <summary>
        /// Показывает страницу со списком всех книг (есть вариант фильтрации книг)
        /// </summary>
        /// <param name="model"> Модель поиска </param>
        /// <returns> Результат вывода книг </returns>
        [HttpPost("Books/[action]")]
        [Authorize(Roles = "Director, User")]
        public IActionResult ListBooks([FromForm]SearchViewModel model)
        {
            if (model.Name == null && model.Category == BookCategory.All)
                model = null;
           var result = _libraryService.GetAllBooks(model);
            if (result != null)
                return new OkObjectResult(result);
            else return new BadRequestObjectResult(null);
        }
        /// <summary>
        /// Показывает страницу со списком всех книг текущего пользователя (есть вариант фильтрации книг)
        /// </summary>
        /// <param name="model"> Модель поиска  </param>
        /// <returns> Результат вывода книг </returns>
        [HttpPost("Books/[action]")]
        [Authorize(Roles = "Director, User")]
        public IActionResult MyBooks([FromForm]SearchViewModel model)
        {
            if (model.Name == null && model.Category == BookCategory.All)
                model = null;
            var result = _libraryService.GetListBooksByUser(CurrentUser(), model);
            if (result != null)
                return new OkObjectResult(result);
            else return new BadRequestObjectResult(model);
        }

        /// <summary>
        /// Запрос на создание книги
        /// </summary>
        /// <param name="model"> Модель книги </param>
        /// <returns> Результат создания книги </returns>
        [HttpPost("Books/[action]")]
        [Authorize(Roles = "Director")]
        public IActionResult CreateBook([FromForm]BookViewModel model)
        {
         //   try
        //    {

                if (ModelState.IsValid)
                {
                    var result = _libraryService.CreateBook(model);
                    if (result != null)
                        return new OkObjectResult(result);
                    else return new BadRequestObjectResult("При создании книги возникла ошибка");

                }
                else return new BadRequestObjectResult("При создании книги возникла ошибка");

        //    }
    //        catch (Exception ex)
            //{
       //         return new BadRequestObjectResult(ex.Message);
         //   }


        }

        /// <summary>
        /// Запрос на изменение книги
        /// </summary>
        /// <param name="model"> Модель измененной книги </param>
        /// <returns> Результат изменения </returns>
        [HttpPut("Books/[action]")]
        [Authorize(Roles = "Director")]
        public IActionResult UpdateBook(BookViewModel model)
        {


                if (ModelState.IsValid)
                {

                    var result = _libraryService.UpdateBook(model);
                    if (result != null)
                        return new OkObjectResult(result);
                    else return new BadRequestObjectResult("При изменении книги произошла ошибка");

                }
                else return new BadRequestObjectResult("При изменении книги произошла ошибка");

        }

        /// <summary>
        /// Запрос на удаление книги
        /// </summary>
        /// <param name="id"> Id книги </param>
        /// <returns> Результат удаления книги </returns>
        [HttpDelete("Books/[action]")]
        [Authorize(Roles = "Director")]
        public IActionResult DeleteBook([FromForm]Guid id)
        {
            try
            {

                if (ModelState.IsValid)
                {

                    var result = _libraryService.DeleteBook(id);
                    if (result != null)
                        return new OkObjectResult(result);
                    else return new BadRequestObjectResult("При удалении книги возникла ошибка");

                }
                else return new BadRequestObjectResult("При удалении книги возникла ошибка");

            }
            catch(Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        /// <summary>
        /// Запрос на получение книги
        /// </summary>
        /// <param name="bookId"> Id книги </param>
        /// <returns> Результат получения книги </returns>
        [HttpPost("Books/[action]")]
        [Authorize(Roles = "Director, User")]
        public IActionResult ReceivingBook([FromForm]Guid bookId)
        {
            try
            {

                if (ModelState.IsValid)
                {

                    var result = _libraryService.ReceivingBook(bookId, CurrentUser());
                    if (result != null)
                        return new OkObjectResult(result);
                    else return new BadRequestObjectResult("При получении книги возникла ошибка");

                }
                else return new BadRequestObjectResult("При получении книги возникла ошибка");

            }
            catch(Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        /// <summary>
        /// Запрос на возврат книги
        /// </summary>
        /// <param name="bookId"> Id книги </param>
        /// <returns> Результат возврата книги </returns>
        [HttpPost("Books/[action]")]
        [Authorize(Roles = "Director, User")]
        public IActionResult ReturnBook([FromForm]Guid bookId)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var result = _libraryService.ReturnBook(bookId, CurrentUser());
                    if (result != null)
                        return new OkObjectResult(result);
                    else return new BadRequestObjectResult("При возврате книги возникла ошибка");
                }
                else return new BadRequestObjectResult("При возврате книги возникла ошибка");

            }
            catch(Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        /// <summary>
        /// Запрос на создание оповещения при появлении книги в наличии
        /// </summary>
        /// <param name="bookId"> Id книги </param>
        /// <returns> Результат создания оповещения </returns>
        [HttpPost("Books/[action]")]
        [Authorize(Roles = "Director, User")]
        public IActionResult CreateNotification([FromForm]Guid bookId)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var result = _libraryService.CreaterNotification(CurrentUser(), bookId);
                    if (result != null)
                        return new OkObjectResult(result);
                    else return new BadRequestObjectResult("При возврате книги возникла ошибка");
                }
                else return new BadRequestObjectResult("При возврате книги возникла ошибка");

            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        [HttpPost("Books/[action]")]
        public IActionResult SendEmail([FromForm] SendModel send)
        {
            return new OkObjectResult(send);
        }

        /// <summary>
        /// Получение Id текущего пользователя
        /// </summary>
        /// <returns> Id текущего пользователя </returns>
        string CurrentUser()
        {
            ClaimsPrincipal currentUser = this.User;
            var userId = currentUser.FindFirst(ClaimTypes.NameIdentifier).Value;
            return userId;
        }

    }
}