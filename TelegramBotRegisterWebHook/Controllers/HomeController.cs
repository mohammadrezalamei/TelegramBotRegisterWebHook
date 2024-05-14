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
        public Microsoft.AspNetCore.Mvc.IActionResult Index(System.Threading.CancellationToken cancellationToken)
        {
            return View();
        }

        [Microsoft.AspNetCore.Mvc.HttpPost]
        public async System.Threading.Tasks.Task<Microsoft.AspNetCore.Mvc.IActionResult> Index([Microsoft.AspNetCore.Mvc.FromForm]
        TelegramBotRegisterWebHook.ViewModels.SetWebHookViewModel viewModel, System.Threading.CancellationToken cancellationToken)
        {
            System.Net.Http.HttpRequestMessage httpRequestMessage =
                new System.Net.Http.HttpRequestMessage()
                {
                    Method = System.Net.Http.HttpMethod.Get,
                    RequestUri = new System.Uri(uriString: $"https://api.telegram.org/bot{viewModel.Token}/setwebhook?url={viewModel.Url}")
                };

            System.Net.Http.HttpResponseMessage httpResponse =
                await _httpClient.SendAsync(request: httpRequestMessage, cancellationToken: cancellationToken);

            string jsonResult = await httpResponse.Content.ReadAsStringAsync(cancellationToken: cancellationToken);

            TelegramBotRegisterWebHook.Models.TelegramResultModel model =
                Newtonsoft.Json.JsonConvert.DeserializeObject<TelegramBotRegisterWebHook.Models.TelegramResultModel>(value: jsonResult) ??
                new Models.TelegramResultModel();

            return View("Result", model);
        }
    }
}
