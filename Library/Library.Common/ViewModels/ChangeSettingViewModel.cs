using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Common.ViewModels
{
    /// <summary>
    /// Модель представления изменения настроек системы
    /// </summary>
    public class ChangeSettingViewModel
    {
        public string NameSetting { get; set; }
        public string PrevSetting { get; set; }
        public string NewSetting { get; set; }

    }
}
