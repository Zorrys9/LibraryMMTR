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
        /// <summary>
        /// Возвращает настройки системы, связанные с рассылкой почты
        /// </summary>
        /// <returns>Модель представления настроек</returns>
        Task<SettingsViewModel> GetSettings();

        /// <summary>
        /// Изменение настроек системы
        /// </summary>
        /// <param name="model">Модель представления настроек системы</param>
        Task ChangeSettingsAsync(SettingsViewModel model);

    }
}
