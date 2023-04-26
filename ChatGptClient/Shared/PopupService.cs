using Microsoft.AspNetCore.Components;

namespace ChatGptClient.Shared;

public delegate void PopupEvent(RenderFragment fragment);
public delegate void CloseEvent();

public interface IPopupService
{
    event PopupEvent OnPopup;
    event CloseEvent OnClose;
    void Show(RenderFragment<PopupService> fragment);
    void Close();
}

public class PopupService : IPopupService
{
    public event PopupEvent OnPopup;
    public event CloseEvent OnClose;
    
    public void Show(RenderFragment<PopupService> fragment)
    {
        RenderFragment rf = builder =>
        {
            int i = 0;
            builder.OpenElement(i++, "div");
            builder.AddAttribute(i++, "class", "block-background");
            builder.OpenElement(i++, "div");
            builder.AddAttribute(i++, "class", "popup");
            fragment(this)(builder);
            builder.CloseElement();
            builder.CloseElement();
        };
        OnPopup?.Invoke(rf);
    }

    public void Close() => OnClose?.Invoke();
}
