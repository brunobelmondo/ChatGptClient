using System.Net.Http.Headers;
using Microsoft.JSInterop;

namespace ChatGptClient.ChatGptModel;

public class ChatGpt
{
    private const string ApikeyName = "apiKey";
    private readonly IJSRuntime jsRuntime;
    private HttpClient client = new();

    public ChatGpt(IJSRuntime jsRuntime)
    {
        this.jsRuntime = jsRuntime;
        Sessions.Add(new ChatSession(this));
    }
    
    public string ApiKey { get; private set; }
    public List<ChatSession> Sessions { get; } = new();
    public HttpClient Client => client;

    public async Task LoadKey()
    {
        ApiKey = await this.jsRuntime.InvokeAsync<string>("localStorage.getItem", ApikeyName);
        UpdateAuth();
    }

    private void UpdateAuth()
    {
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ApiKey);
    }

    public async Task SetKey(string key)
    {
        ApiKey = key;
        await this.jsRuntime.InvokeVoidAsync("localStorage.setItem", ApikeyName, key);
        UpdateAuth();
    }
}