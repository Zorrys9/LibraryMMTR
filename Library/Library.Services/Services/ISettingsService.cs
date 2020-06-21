using Library.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Library.Services.Services
{
    /// <summary>
    /// Сервис управления настройками системы
    /// </summary>
    public interface ISettingsService
    {

        Task<SettingsViewModel> GetSettings();

        Task ChangeSettingsAsync(SettingsViewModel model);

    }
}
