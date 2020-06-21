using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Library.Common.ViewModels;
using Library.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Library.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class SettingsController : Controller
    {

        private readonly ISettingsService _settingsService;
        public SettingsController(ISettingsService settingsService)
        {
            _settingsService = settingsService;
        }

        [HttpGet("[action]")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> AllSettings()
        {

            try
            {

                var settings = await _settingsService.GetSettings();

                return View(settings);

            }
            catch(Exception ex)
            {

                return BadRequest(ex.Message);

            }
        }

        [HttpPost("[action]")]
        [Authorize(Roles="Admin")]
        public async Task<IActionResult> ChangeSettings([FromForm]SettingsViewModel model)
        {

            try
            {

                if (ModelState.IsValid)
                {
                    
                    await _settingsService.ChangeSettingsAsync(model);

                    return Ok("Настройки успешно сохранены");
                }
                else
                {

                    throw new Exception("Данные заполнены неверно");

                }

            }
            catch(Exception ex)
            {

                return BadRequest(ex.Message);

            }

        }

    }
}