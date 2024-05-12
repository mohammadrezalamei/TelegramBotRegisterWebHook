using Microsoft.Extensions.DependencyInjection;

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
            System.Net.Http.HttpResponseMessage httpResponse =
                await _httpClient.SendAsync(new System.Net.Http.HttpRequestMessage()
                {
                    Method = System.Net.Http.HttpMethod.Get,
                    RequestUri = new System.Uri($"https://api.telegram.org/bot{viewModel.Token}/setwebhook?url={viewModel.Url}")
                });
            string jsonResult = await httpResponse.Content.ReadAsStringAsync();
            TelegramBotRegisterWebHook.Models.TelegramResultModel model =
                Newtonsoft.Json.JsonConvert.DeserializeObject<TelegramBotRegisterWebHook.Models.TelegramResultModel>(jsonResult) ??
                new Models.TelegramResultModel();
            return View("Result", model);
        }
    }
}
