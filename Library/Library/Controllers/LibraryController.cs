using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Library.Common.ViewModels;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Library.Common.Enums;
using Library.Models;
using Library.Logic.LogicModels;
using Library.Services.Services;

namespace Library.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LibraryController : Controller
    {
        private readonly LibraryLogic _libraryLogic;
        private readonly IBookService _bookService;
        private readonly IUserService _userService;
        private readonly IKeyWordService _keyWordService;

        public LibraryController(LibraryLogic libraryLogic, IBookService bookService, IUserService userService, IKeyWordService keyWordService)
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
            var result = _libraryLogic.GetBookCard(bookId, CurrentUser());
            if (result != null)
            {
                return View(result);
            }
                
            //else return new BadRequestObjectResult(bookId);
            else return BadRequest();
        }

        [HttpPost("Books/SelectKeyWords")]
        public IActionResult SelectKeyWords([FromForm]string name)
        {
            var result = _keyWordService.GetAll();

            result = result.Where(word => word.ToLower().Contains(name.ToLower())).Take(3).ToList();

            return PartialView(result);
        }

        [HttpPost("Books/[action]")]
        public IActionResult ListBooks([FromForm]SearchViewModel model, [FromForm]int page=1, [FromForm]int pageItems=4)
        {
            var result = _libraryLogic.GetAllBook(CurrentUser(), model);

            if (result != null)
            {
                result.PageView = pageItems;
                if(pageItems == 1)
                {
                    pageItems = 4;
                }

                Pagination pagination = new Pagination
                {
                    PageItemsAmount = pageItems,
                    CurrentPage = page,
                    ControllerName = "Library",
                    ActionName = "AllBooks",
                    ShowLastAndFirstPages = true
                };

                //pagination.Params.Add("Category", (int)model.Category);
                //pagination.Params.Add("Name", model.Name);

                result.Search = model;
                pagination.ItemsAmount = result.Books.Count();
                pagination.Refresh();
                ViewBag.Pagination = pagination;

                result.Books = result.Books.OrderBy(book => book.Title).OrderByDescending(book => book.Count).Skip((page - 1) * pageItems).Take(pageItems).ToList();

                //TempData["PageView"] = viewModel;

                return PartialView(result);
            }

            return PartialView(result);

        }
        /// <summary>
        /// Показывает страницу со списком всех книг (есть вариант фильтрации книг)
        /// </summary>
        /// <param name="model"> Модель поиска </param>
        /// <returns> Результат вывода книг </returns>
        [HttpGet("Books/[action]")]
        [Authorize(Roles = "Director, User")]
        public IActionResult AllBooks()
        {
            return View();
        }

        //[HttpPost("Books/[action]/{name?}/{category?}/{page?}")]
        //public IActionResult ListBooks(SearchViewModel model, int page = 1)
        //{
        //    ListBooksViewModel result;
        //    int pageItems = 4;

        //    result = _libraryLogic.GetAllBook(CurrentUser(), model);

           
        //    else return Ok();
        //    else return RedirectToAction("ListBooks", "Library");

        //}

        /// <summary>
        /// Показывает страницу со списком всех книг текущего пользователя (есть вариант фильтрации книг)
        /// </summary>
        /// <param name="model"> Модель поиска  </param>
        /// <returns> Результат вывода книг </returns>
        [HttpPost("Books/[action]")]
        [Authorize(Roles = "Director, User")]
        public IActionResult MyBooks([FromForm]SearchViewModel model)
        {
            //if (model.Name == null && model.Category == BookCategory.All)
            //    model = null;
            var result = _libraryLogic.GetMyBooks(CurrentUser());
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
                var result = _libraryLogic.Create(model);
                if (result != null)
                    return RedirectToAction("AllBooks");
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
        public async Task<IActionResult> UpdateBook([FromForm]BookViewModel model)
        {


                if (ModelState.IsValid)
                {

                var result = await _libraryLogic.Update(model);
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

                    var result = _bookService.Delete(id);
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
        public async Task<IActionResult> ReceivingBook([FromForm]Guid bookId, [FromForm]string url)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _libraryLogic.Receiving(bookId, CurrentUser());
                    if (result != null)
                        return Redirect(url);
                    else throw new Exception("При получении книги возникла ошибка");

                }
                else throw new Exception("При получении книги возникла ошибка");
            }
            catch(Exception ex)
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
        [Authorize(Roles = "Director, User")]
        public async Task<IActionResult> ReturnBook([FromForm]Guid bookId, [FromForm]string url)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var result = await _libraryLogic.Return(bookId, CurrentUser());
                    if (result != null)
                        return Redirect(url);
                    else throw new Exception("При возврате книги возникла ошибка");
                }
                else throw new Exception("При возврате книги возникла ошибка");

            }
            catch(Exception ex)
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
        [Authorize(Roles = "Director, User")]
        public async Task<IActionResult> CreateNotification([FromForm]Guid bookId, [FromForm]string url)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var result = await _libraryLogic.CreateNotification(bookId, CurrentUser());
                    if (result != null)
                        return Redirect(url);
                    else return new BadRequestObjectResult("При возврате книги возникла ошибкa " + bookId.ToString());
                }
                else return new BadRequestObjectResult("При возврате книги возникла ошибка");

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