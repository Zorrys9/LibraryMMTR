using Library.Common.ViewModels;
using MailKit.Net.Smtp;
using Serilog;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace Library.Services.Services.Implementations
{
    public class SettingsService : ISettingsService
    {
        private readonly ILogger _logger;

        public SettingsService(ILogger logger)
        {
            _logger = logger;
        }

        public Task<SettingsViewModel> GetSettings()
        {
            return GetSettingsFromJSON("mailingsettings.json");
        }

        public async Task ChangeSettingsAsync(SettingsViewModel model)
        {
            using (var client = new SmtpClient())
            {
                client.Connect(model.SMTPhost, int.Parse(model.SMTPport), bool.Parse(model.SSL));
                client.Authenticate(model.Email, model.Password);
                client.Disconnect(true);
            }

            string path = "mailingsettings.json";

            File.Delete(path);

            await SetSettingsToJson(path, model);

            _logger.Information($"Settings system changed: \n" + JsonSerializer.Serialize(model));
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
