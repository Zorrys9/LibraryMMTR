using Library.Common.ViewModels;
using Library.Logic.LogicModels;
using Library.Logic.Logics;
using Library.Models;
using Library.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Rewrite.Internal.UrlActions;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Library.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LibraryController : Controller
    {
        private readonly ILibraryLogic _libraryLogic;
        private readonly IBookService _bookService;
        private readonly IUserService _userService;
        private readonly IKeyWordService _keyWordService;

        public LibraryController(ILibraryLogic libraryLogic, IBookService bookService, IUserService userService, IKeyWordService keyWordService)
        {
            _libraryLogic = libraryLogic;
            _bookService = bookService;
            _userService = userService;
            _keyWordService = keyWordService;
        }




        /// <summary>
        /// Показывает страницу создания новой книги
        /// </summary>
        /// <returns> Результат создания </returns>
        [HttpGet("Books/[action]")]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateBook()
        {
            try
            {

                return View();

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);

            }
        }

        /// <summary>
        /// Показывает карточку книги
        /// </summary>
        /// <param name="bookId"> Id книги </param>
        /// <returns> Результат получения книги по Id </returns>
        [HttpGet("Books/[action]")]
        [Authorize(Roles = "Admin")]
        public IActionResult BookCard(Guid bookId)
        {
            try
            {

                if (ModelState.IsValid)
                {

                    var result = _libraryLogic.GetBookCard(bookId, CurrentUser());

                    if (result != null)
                    {
                        return View(result);
                    }
                    else
                    {
                        return BadRequest();
                    }

                }
                else
                {
                    return BadRequest();
                }


            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        /// <summary>
        /// Вывод частичного представления "Справочник ключевых слов"
        /// </summary>
        /// <param name="name"> Введенное слово на странице </param>
        /// <returns> Вывод частичного представления </returns>
        [HttpPost("Books/SelectKeyWords")]
        public IActionResult SelectKeyWords([FromForm]string name)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var result = _keyWordService.GetAll();

                    result = result.Where(word => word.ToLower().Contains(name.ToLower())).Take(3).ToList();

                    return PartialView(result);
                }
                else
                {
                    return BadRequest();
                }


            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        /// <summary>
        /// ВЫвод частичного представления "Список книг"
        /// </summary>
        /// <param name="actionName"> Action, в котором используется частичное представление</param>
        /// <param name="model"> Модель поиска </param>
        /// <param name="page"> Текущая страница </param>
        /// <param name="pageItems"> Количество книг на странице </param>
        /// <returns> Вывод частичного представления </returns>
        [HttpPost("Books/[action]")]
        public IActionResult ListBooks([FromForm]string actionName, [FromForm]SearchViewModel model, [FromForm]int page = 1, [FromForm]int pageItems = 4)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    ListBooksViewModel result = null;

                    switch (actionName)
                    {
                        case "AllBooks":
                            result = _libraryLogic.GetAllBook(CurrentUser(), model);
                            break;

                        case "CurrentReadList":
                            result = _libraryLogic.GetCurrentReadBooks(CurrentUser(), model);
                            break;

                        case "PreviousReadList":
                            result = _libraryLogic.GetPreviousReadBooks(CurrentUser(), model);
                            break;
                    }

                    if (result != null)
                    {
                        result.PageView = pageItems;

                        if (pageItems == 1)
                        {
                            pageItems = 4;
                        }

                        Pagination pagination = new Pagination
                        {
                            PageItemsAmount = pageItems,
                            CurrentPage = page,
                            ControllerName = "Library",
                            ActionName = actionName,
                            ShowLastAndFirstPages = true
                        };

                        result.Search = model;
                        pagination.ItemsAmount = result.Books.Count();
                        pagination.Refresh();
                        ViewBag.Pagination = pagination;

                        result.Books = result.Books.OrderBy(book => book.Title).OrderByDescending(book => book.Count).Skip((page - 1) * pageItems).Take(pageItems).ToList();

                        return PartialView(result);
                    }

                    return PartialView(result);
                }
                else
                {
                    return BadRequest();
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        /// <summary>
        /// Показывает страницу со списком всех книг
        /// </summary>
        /// <returns> Результат вывода книг </returns>
        [HttpGet("Books/[action]")]
        [Authorize(Roles = "Admin, User")]
        public IActionResult AllBooks()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Показывает страницу со списком книг текущего пользователя 
        /// </summary>
        /// <returns> Результат вывода книг </returns>
        [HttpGet("Books/[action]")]
        [Authorize(Roles = "Admin, User")]
        public IActionResult CurrentReadList()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Показывает страницу со списком прочитанных пользователем книг
        /// </summary>
        /// <returns> Результат вывода книг </returns>
        [HttpGet("Books/[action]")]
        [Authorize(Roles = "Admin, User")]
        public IActionResult PreviousReadList()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Возвращает представление со списком действий с книгой
        /// </summary>
        /// <param name="bookId"> Id книги </param>
        /// <param name="count"> Количество возвращаемых записей </param>
        /// <param name="countRequest"> Количество предыдущих запросов </param>
        /// <returns> Представление со списком действий </returns>
        [HttpPost("Logs/[action]")]
        public IActionResult LogsBook([FromForm]Guid bookId, [FromForm] int count = 5, [FromForm] int countRequest = 0)
        {
            try
            {

                var result = _libraryLogic.GetLogsBook(bookId, count, countRequest);

                return View(result);

            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);

            }
        }

        /// <summary>
        /// Возвращает представление со списком активных пользователей книги
        /// </summary>
        /// <param name="bookId"> Id книги </param>
        /// <param name="count"> Количество возвращаемых записей </param>
        /// <param name="countRequest"> Количество предыдущих запросов </param>
        /// <returns> Представление со списком активных пользователей книги </returns>
        [HttpPost("Holders/[action]")]
        public IActionResult HoldersBook([FromForm] Guid bookId, [FromForm] int count = 5, [FromForm] int countRequest = 0)
        {
            try
            {

                var result = _libraryLogic.GetHolderBook(bookId, count, countRequest);

                return View(result);

            }
            catch(Exception ex)
            {

                return BadRequest(ex.Message);

            }
        }

        /// <summary>
        /// Проверка используется ли книга пользователем
        /// </summary>
        /// <param name="bookId"> Id книги </param>
        /// <returns> Результат проверки </returns>
        [HttpPost("Books/[action]")]
        public IActionResult CheckBook([FromForm]Guid bookId)
        {
            try
            {
                var result = _libraryLogic.CheckBook(bookId);

                if (!result)
                {

                    return Ok();


                }
                else
                {

                    return BadRequest("Данная книга используется пользователями, удаление невозможно");

                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Запрос на создание книги
        /// </summary>
        /// <param name="model"> Модель книги </param>
        /// <returns> Результат создания книги </returns>
        [HttpPost("Books/[action]")]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateBook([FromForm]BookViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var result = _libraryLogic.Create(model);

                    if (result != null)
                    {
                        return Ok();
                    }
                    else
                    {
                        throw new Exception("При создании книги возникла ошибка");
                    }

                }
                else
                {

                    throw new Exception("Создание книги невозможно, проверьте введенные данные и повторите попытку");

                }


            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Показывает страницу изменения книги
        /// </summary>
        /// <param name="bookId"> Id книги </param>
        /// <returns> Страница изменения книги </returns>
        [HttpGet("Books/EditBook")]
        public IActionResult EditBook(Guid bookId)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var result = _libraryLogic.GetBook(bookId);

                    return View(result);
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
        /// Запрос на изменение книги
        /// </summary>
        /// <param name="model"> Модель измененной книги </param>
        /// <returns> Результат изменения </returns>
        [HttpPost("Books/[action]")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateBook([FromForm]BookViewModel model)
        {
            try
            {

                if (ModelState.IsValid)
                {

                    var result = await _libraryLogic.Update(model);

                    if (result != null)
                    {

                        return Ok();

                    }
                    else
                    {

                        throw new Exception("При изменении книги произошла ошибка");

                    }

                }
                else
                {
                    throw new Exception("При изменении книги произошла ошибка, данные заполнены не верно");
                }

            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        /// <summary>
        /// Запрос на удаление книги
        /// </summary>
        /// <param name="id"> Id книги </param>
        /// <returns> Результат удаления книги </returns>
        [HttpPost("Books/[action]")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteBook([FromForm]Guid bookId)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var result = _bookService.Delete(bookId);

                    if (result != null)
                    {
                        return RedirectToAction("AllBooks");
                    }
                    else
                    {
                        return BadRequest("При удалении книги возникла ошибка");
                    }

                }
                else
                {
                    return BadRequest("При удалении книги возникла ошибка");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Запрос на получение книги
        /// </summary>
        /// <param name="bookId"> Id книги </param>
        /// <returns> Результат получения книги </returns>
        [HttpPost("Books/[action]")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> ReceivingBook([FromForm]Guid bookId)
        {
            try
            {

                if (ModelState.IsValid)
                {

                    var result = await _libraryLogic.Receiving(bookId, CurrentUser());

                    if (result != null)
                    {
                        return Ok();
                    }
                    else
                    {
                        throw new Exception("При получении книги возникла ошибка");
                    }

                }
                else
                {
                    throw new Exception("При получении книги возникла ошибка");
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        /// <summary>
        /// Запрос на возврат книги
        /// </summary>
        /// <param name="bookId"> Id книги </param>
        /// <returns> Результат возврата книги </returns>
        [HttpPost("Books/[action]")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> ReturnBook([FromForm]Guid bookId)
        {
            try
            {

                if (ModelState.IsValid)
                {

                    var result = await _libraryLogic.Return(bookId, CurrentUser());

                    if (result != null)
                    {
                        return Ok();
                    }
                    else
                    {
                        throw new Exception("При возврате книги возникла ошибка");
                    }

                }
                else
                {
                    throw new Exception("При возврате книги возникла ошибка");
                }

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Запрос на создание оповещения при появлении книги в наличии
        /// </summary>
        /// <param name="bookId"> Id книги </param>
        /// <returns> Результат создания оповещения </returns>
        [HttpPost("Books/[action]")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> CreateNotification([FromForm]Guid bookId)
        {
            try
            {

                if (ModelState.IsValid)
                {

                    var result = await _libraryLogic.CreateNotification(bookId, CurrentUser());

                    if (result != null)
                    {
                        return Ok();
                    }
                    else
                    {
                        return new BadRequestObjectResult("При возврате книги возникла ошибкa " + bookId.ToString());
                    }

                }
                else
                {
                    return new BadRequestObjectResult("При возврате книги возникла ошибка");
                }

            }
            catch (Exception ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
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