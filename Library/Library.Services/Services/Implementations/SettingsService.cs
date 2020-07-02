using Library.Common.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using Newtonsoft.Json.Linq;
using System.Linq;
using MailKit.Net.Smtp;

namespace Library.Services.Services.Implementations
{
    public class SettingsService:ISettingsService
    {


        public Task<SettingsViewModel> GetSettings()
        {

            return GetSettingsFromJSON("mailingsettings.json");

        }

        public async Task ChangeSettingsAsync(SettingsViewModel model)
        {

            try
            {

                using (var client = new SmtpClient())
                {

                    client.Connect(model.SMTPhost, int.Parse(model.SMTPport), bool.Parse(model.SSL));
                    client.Authenticate(model.Email, model.Password);
                    client.Disconnect(true);

                }

            }
            catch(Exception ex)
            {

                throw new Exception("Ошибка при изменении настроек! Данные введены неверно, проверьте заполненные поля и повторите попытку");

            }

            string path = "mailingsettings.json";

            File.Delete(path);

            await SetSettingsToJson(path, model);

        }

        static async Task<SettingsViewModel> GetSettingsFromJSON(string path)
        {
            SettingsViewModel settings = new SettingsViewModel();

            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {

                settings = await JsonSerializer.DeserializeAsync<SettingsViewModel>(fs);

            }

            return settings;
        }

        static async Task SetSettingsToJson(string path, SettingsViewModel settings)
        {

            using (FileStream fs = new FileStream(path, FileMode.OpenOrCreate))
            {

                await JsonSerializer.SerializeAsync(fs, settings);

                await fs.FlushAsync();
            }

        }


    }
}
