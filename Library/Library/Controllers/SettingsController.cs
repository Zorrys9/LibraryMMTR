using Library.Common.ViewModels;
using Library.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using System.Threading.Tasks;

namespace Library.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SettingsController : Controller
    {

        private readonly ISettingsService _settingsService;
        private readonly ILogger _logger;
        public SettingsController(ISettingsService settingsService, ILogger logger)
        {
            _settingsService = settingsService;
            _logger = logger;
        }

        /// <summary>
        /// Показывает все настройки системы, связанные с рассылкой почты
        /// </summary>
        /// <returns>Результат вывода настроек</returns>
        [HttpGet("[action]")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AllSettings()
        {
            var settings = await _settingsService.GetSettings();
            return View(settings);
        }

        /// <summary>
        /// Изменение настроек системы
        /// </summary>
        /// <param name="model">Модель представления настроек системы</param>
        /// <returns>Результат изменения настроек</returns>
        [HttpPut("[action]")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ChangeSettings([FromForm] SettingsViewModel model)
        {
            await _settingsService.ChangeSettingsAsync(model);
            return Ok("Настройки успешно сохранены");
        }
    }
}