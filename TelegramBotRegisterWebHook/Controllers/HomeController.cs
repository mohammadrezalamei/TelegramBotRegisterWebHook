using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace TelegramBotRegisterWebHook.Controllers
{
    public class HomeController : Microsoft.AspNetCore.Mvc.Controller
    {
        private readonly System.Net.Http.HttpClient _httpClient;
        public HomeController(System.IServiceProvider serviceProvider)
        {
            Microsoft.Extensions.DependencyInjection.IServiceScope serviceScope =
                serviceProvider.CreateScope();
            _httpClient =
                serviceScope.ServiceProvider.GetRequiredService<System.Net.Http.HttpClient>();
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        public Microsoft.AspNetCore.Mvc.IActionResult Index()
        {
            return View();
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        public async System.Threading.Tasks.Task<Microsoft.AspNetCore.Mvc.IActionResult> Index([Microsoft.AspNetCore.Mvc.FromForm]
        TelegramBotRegisterWebHook.ViewModels.SetWebHookViewModel viewModel)
        {
            string jsonResult =
                await _httpClient.GetStringAsync($"https://api.telegram.org/bot{viewModel.Token}/setwebhook?url={viewModel.Url}");
            TelegramBotRegisterWebHook.Models.TelegramResultModel model =
                Newtonsoft.Json.JsonConvert.DeserializeObject<TelegramBotRegisterWebHook.Models.TelegramResultModel>(jsonResult) ??
                new Models.TelegramResultModel();
            return View("Result", model);
        }
    }
}
