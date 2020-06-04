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

namespace Library.Services.Services.Implementations
{
    public class SettingsService:ISettingsService
    {


        public SettingsViewModel GetSettingsAsync()
        {

            return JsonSerializer.Deserialize<SettingsViewModel>(File.ReadAllText("mailingsettings.json"));

        }

        public async Task ChangeSetting(ChangeSettingViewModel model)
        {
            string path = "mailingsettings.json";
            var settings = await File.ReadAllTextAsync(path);
            var nameSetting = model.NameSetting + '"' + ":" + '"';

            settings = settings.Replace(nameSetting + model.PrevSetting, nameSetting + model.NewSetting);

            await File.WriteAllTextAsync(path, settings);

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
