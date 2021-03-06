﻿using Newtonsoft.Json;
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
        [JsonPropertyName("Email")]
        public string Email { get; set; }

        [JsonPropertyName("Password")]
        public string Password { get; set; }

        [JsonPropertyName("RabbitMQ")]
        public string RabbitMQ { get; set; }

        [JsonPropertyName("SMTP_host")]
        public string SMTPhost{ get; set; }

        [JsonPropertyName("SMTP_port")]
        public string SMTPport { get; set; }

        [JsonPropertyName("UseSSL")]
        public string SSL { get; set; }

    }
}
