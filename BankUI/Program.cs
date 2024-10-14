using BankUI.Services;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

namespace BankUI
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddScoped(sp => new HttpClient
            { 
                BaseAddress = new Uri(builder.Configuration.GetValue<string>("BaseAPIUrl"))
            });


            builder.Services.AddScoped<IAccountHoldersService, AccountHolderService>();
            builder.Services.AddMudServices();

            await builder.Build().RunAsync();
        }
    }
}
