using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Library.Common.ViewModels
{
    /// <summary>
    ///  Модель основных настроек системы
    /// </summary>
    public class SettingsViewModel
    {
        /// <summary>
        /// Электронная почта 
        /// </summary>
        [JsonPropertyName("Email")]
        public string Email { get; set; }

        /// <summary>
        /// Пароль от электронной почты
        /// </summary>
        [JsonPropertyName("Password")]
        public string Password { get; set; }

        /// <summary>
        /// Хостинг RabbitMQ
        /// </summary>
        [JsonPropertyName("RabbitMQ")]
        public string RabbitMQ { get; set; }

        /// <summary>
        /// Хост от RabbitMQ
        /// </summary>
        [JsonPropertyName("SMTP_host")]
        public string SMTPhost{ get; set; }

        /// <summary>
        /// Порт от RabbitMQ
        /// </summary>
        [JsonPropertyName("SMTP_port")]
        public string SMTPport { get; set; }

        /// <summary>
        /// Использовать ли SSL?
        /// </summary>
        [JsonPropertyName("UseSSL")]
        public string SSL { get; set; }
    }
}
