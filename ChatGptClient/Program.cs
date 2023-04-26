using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ChatGptClient;
using ChatGptClient.ChatGptModel;
using ChatGptClient.Shared;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddSingleton<ChatGpt>();
builder.Services.AddScoped<IPopupService>(x=> new PopupService());

await builder.Build().RunAsync();