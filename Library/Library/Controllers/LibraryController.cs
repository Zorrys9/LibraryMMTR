using Library.Common.Models;
using Library.Common.ViewModels;
using Library.Exceptions;
using Library.Extensions;
using Library.Logic.Logics;
using Library.Models;
using Library.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System;
using System.Linq;
using System.Threading.Tasks;

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
        private readonly ILogger _logger;

        public LibraryController(ILibraryLogic libraryLogic, IBookService bookService, IUserService userService, IKeyWordService keyWordService, ILogger logger)
        {
            _libraryLogic = libraryLogic;
            _bookService = bookService;
            _userService = userService;
            _keyWordService = keyWordService;
            _logger = logger;
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
            catch (BuisnessException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                 _logger.Error("Error: " + ex.Message, ex);
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Показывает карточку книги
        /// </summary>
        /// <param name="bookId"> Id книги </param>
        /// <returns> Результат получения книги по Id </returns>
        [HttpGet("Books/[action]")]
        [Authorize(Roles = "User, Admin")]
        public IActionResult BookCard(Guid bookId)
        {
            try
            {
                var result = _libraryLogic.GetBookCard(bookId, this.CurrentUser());

                return View(result);
            }
            catch (BuisnessException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                 _logger.Error("Error: " + ex.Message, ex);
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Вывод частичного представления "Справочник ключевых слов"
        /// </summary>
        /// <param name="name"> Введенное слово на странице </param>
        /// <returns> Вывод частичного представления </returns>
        [HttpPost("Books/SelectKeyWords")]
        [Authorize(Roles = "Admin, User")]
        public IActionResult SelectKeyWords([FromForm] string name)
        {
            try
            {
                var result = _keyWordService.GetAll();

                result = result.Where(word => word.ToLower().Contains(name.ToLower())).Take(3).ToList();

                return PartialView(result);
            }
            catch (BuisnessException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                 _logger.Error("Error: " + ex.Message, ex);
                return StatusCode(500);
            }

        }

        /// <summary>
        /// Вывод частичного предстваления списка книг
        /// </summary>
        /// <param name="pageInfo"> Модель с некоторой информацией о странице</param>
        /// <param name="model">Модель поиска книг</param>
        /// <returns>Вывод частичного представления списка книг</returns>
        [HttpPost("Books/[action]")]
        [Authorize(Roles = "Admin, User")]
        public IActionResult ListBooks([FromForm] PageInfoModel pageInfo, [FromForm] SearchViewModel model)
        {
            try
            {
                ListBooksViewModel result = null;

                switch (pageInfo.ActionName.Replace("/", ""))
                {
                    case "AllBooks":
                        result = _libraryLogic.GetAllBook(this.CurrentUser(), model);
                        break;
                    case "CurrentReadList":
                        result = _libraryLogic.GetCurrentReadBooks(this.CurrentUser(), model);
                        break;
                    case "PreviousReadList":
                        result = _libraryLogic.GetPreviousReadBooks(this.CurrentUser(), model);
                        break;
                }

                if (result != null)
                {
                    result.PageView = pageInfo.PageItems;

                    if (pageInfo.PageItems == 1)
                    {
                        pageInfo.PageItems = 4;
                    }

                    result.Search = model;

                    ViewBag.Pagination = Pagination(pageInfo, result);

                    result.Books = result.Books.OrderByDescending(book => (book.Aviable != 0)).ThenBy(book => book.Title).Skip((pageInfo.Page - 1) * pageInfo.PageItems).Take(pageInfo.PageItems).ToList();

                    return PartialView(result);
                }
                return PartialView(result);
            }
            catch (BuisnessException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex) 
            {
                 _logger.Error("Error: " + ex.Message, ex);
                return StatusCode(500); 
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
            catch (BuisnessException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex) 
            {
                 _logger.Error("Error: " + ex.Message, ex);
                return StatusCode(500); 
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
            catch (BuisnessException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                 _logger.Error("Error: " + ex.Message, ex);
                return StatusCode(500); 
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
            catch (BuisnessException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex) 
            {
                 _logger.Error("Error: " + ex.Message, ex);
                return StatusCode(500); 
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
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> LogsBook([FromForm] Guid bookId, [FromForm] int count = 5, [FromForm] int countRequest = 0)
        {
            try
            {
                var result = await _libraryLogic.GetLogsBook(bookId, count, countRequest);

                return View(result);
            }
            catch (BuisnessException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex) 
            {
                 _logger.Error("Error: " + ex.Message, ex);
                return StatusCode(500);
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
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> HoldersBook([FromForm] Guid bookId, [FromForm] int count = 5, [FromForm] int countRequest = 0)
        {
            try
            {
                var result = await _libraryLogic.GetHolderBook(bookId, count, countRequest);

                return View(result);
            }
            catch (BuisnessException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                 _logger.Error("Error: " + ex.Message, ex);
                return StatusCode(500); 
            }
        }

        /// <summary>
        /// Проверка используется ли книга пользователем
        /// </summary>
        /// <param name="bookId"> Id книги </param>
        /// <returns> Результат проверки </returns>
        [HttpPost("Books/[action]")]
        [Authorize(Roles = "Admin, User")]
        public IActionResult CheckBook([FromForm] Guid bookId)
        {
            try
            {
                _libraryLogic.CheckBook(bookId);

                return Ok("Книге никем не используется");
            }
            catch (BuisnessException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(Exception ex)
            {
                 _logger.Error("Error: " + ex.Message, ex);
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Запрос на создание книги
        /// </summary>
        /// <param name="model"> Модель книги </param>
        /// <returns> Результат создания книги </returns>
        [HttpPost("Books/[action]")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateBook([FromForm] BookViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Создание книги невозможно, проверьте введенные данные и повторите попытку");
                }
                await _libraryLogic.Create(model);

                return Ok("Книга успешно добавлена");
            }
            catch (BuisnessException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(Exception ex)
            {
                 _logger.Error("Error: " + ex.Message, ex);
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Показывает страницу изменения книги
        /// </summary>
        /// <param name="bookId"> Id книги </param>
        /// <returns> Страница изменения книги </returns>
        [HttpGet("Books/EditBook")]
        [Authorize(Roles = "Admin")]
        public IActionResult EditBook(Guid bookId)
        {
            try
            {
                var result = _libraryLogic.GetBook(bookId);

                return View(result);
            }
            catch (BuisnessException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(Exception ex)
            {
                 _logger.Error("Error: " + ex.Message, ex);
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Запрос на изменение книги
        /// </summary>
        /// <param name="model"> Модель измененной книги </param>
        /// <returns> Результат изменения </returns>
        [HttpPut("Books/[action]")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateBook([FromForm] BookViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("При изменении книги произошла ошибка, данные заполнены не верно");
                }

                var url = Request.GetDisplayUrl();
                
                await _libraryLogic.Update(model, url);
                
                return Ok("Книга успешно изменена");
            }
            catch (BuisnessException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(Exception ex)
            {
                 _logger.Error("Error: " + ex.Message, ex);
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Запрос на удаление книги
        /// </summary>
        /// <param name="bookId"> Id книги </param>
        /// <returns> Результат удаления книги </returns>
        [HttpDelete("Books/[action]")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteBook([FromForm] Guid bookId)
        {
            try
            {
                await _bookService.Delete(bookId);
                
                return Ok("Книга успешно удалена");
            }
            catch (BuisnessException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(Exception ex)
            {
                 _logger.Error("Error: " + ex.Message, ex);
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Запрос на получение книги
        /// </summary>
        /// <param name="bookId"> Id книги </param>
        /// <returns> Результат получения книги </returns>
        [HttpPost("Books/[action]")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> ReceivingBook([FromForm] Guid bookId)
        {
            try
            {
                await _libraryLogic.Receiving(bookId, this.CurrentUser());

                return Ok("Книга взята для прочтения");
            }
            catch (BuisnessException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                 _logger.Error("Error: " + ex.Message, ex);
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Запрос на возврат книги
        /// </summary>
        /// <param name="bookId"> Id книги </param>
        /// <returns> Результат возврата книги </returns>
        [HttpPost("Books/[action]")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> ReturnBook([FromForm] Guid bookId)
        {
            try
            {
                var url = Request.GetDisplayUrl();

                await _libraryLogic.Return(bookId, this.CurrentUser(), url);
                
                return Ok("Вы успешно вернули книгу");
            }
            catch (BuisnessException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(Exception ex)
            {
                 _logger.Error("Error: " + ex.Message, ex);
                return StatusCode(500);
            }
        }

        /// <summary>
        /// Запрос на создание оповещения при появлении книги в наличии
        /// </summary>
        /// <param name="bookId"> Id книги </param>
        /// <returns> Результат создания оповещения </returns>
        [HttpPost("Books/[action]")]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> CreateNotification([FromForm] Guid bookId)
        {
            try
            {
                await _libraryLogic.CreateNotification(bookId, this.CurrentUser());

                return Ok("При появлении книги в наличии Вам будет отправлено уведомление");
            }
            catch (BuisnessException ex)
            {
                return BadRequest(ex.Message);
            }
            catch(Exception ex)
            {
                 _logger.Error("Error: " + ex.Message, ex);
                return StatusCode(500);
            }
        }



        /// <summary>
        /// Возвращает модель пагинации страницы
        /// </summary>
        /// <param name="pageInfo"> Информация о странице </param>
        /// <param name="books"> Список книг страницы </param>
        /// <returns> Модель пагинации страницы</returns>
        static Pagination Pagination(PageInfoModel pageInfo, ListBooksViewModel books)
        {
            Pagination pagination = new Pagination
            {
                PageItemsAmount = pageInfo.PageItems,
                CurrentPage = pageInfo.Page,
                ControllerName = "Library",
                ActionName = pageInfo.ActionName.Replace("/", ""),
                ShowLastAndFirstPages = true
            };

            pagination.ItemsAmount = books.Books.Count();
            pagination.Refresh();

            return pagination;
        }
    }
}