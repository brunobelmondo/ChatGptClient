using System.Net.Http.Json;

namespace ChatGptClient.ChatGptModel;

public class ChatSession
{
    private readonly ChatGpt chat;
    private const string url = "https://api.openai.com/v1/chat/completions";

    public ChatSession(ChatGpt chat)
    {
        this.chat = chat;
    }

    public List<ChatGptMessage> Messages { get; } = new();
    
    public async Task Ask(string query)
    {
        Messages.Add(new ChatGptMessage(){role="user", content = query});
        var request = new ChatGptRequest
        {
            model="gpt-3.5-turbo",
            messages = Messages.ToArray(),
            temperature = 0.1 
        };
        var response = await chat.Client.PostAsJsonAsync(url, request);
        if (response.IsSuccessStatusCode)
        {
            Messages.Add((await response.Content.ReadFromJsonAsync<ChatGptAnswer>()).choices.First().message);
        }
        else
        {
            Messages.Add(new ChatGptMessage(){role="user", content = await response.Content.ReadAsStringAsync()});
        }
    }
    
    public void RemoveLast()
    {
        if (Messages.Count > 0)
        {
            Messages.RemoveAt(Messages.Count - 1);
        }
    }

    public void ClearAll()
    {
        Messages.Clear();
    }
}
