using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Library.Common.Models
{
    /// <summary>
    /// Модель с настройками от api
    /// </summary>
    public class SettingApiModel
    {
        [JsonProperty("ApplicationName")]
        public string AppName { get; set; }

        [JsonProperty("ApiKey")]
        public string Key { get; set; }

    }
}
