namespace ChatGptClient.ChatGptModel;

public class ChatGptRequest
{
    public string model { get; set; }
    public ChatGptMessage[] messages { get; set; }
    public double temperature { get; set; }
}