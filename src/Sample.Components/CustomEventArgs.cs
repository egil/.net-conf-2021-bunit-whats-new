using Microsoft.AspNetCore.Components;

namespace Sample.Components;

public class CustomEventArgs : EventArgs
{
    public string CustomProperty1 { get; set; } = string.Empty;
    public string CustomProperty2 { get; set; } = string.Empty;
}

[EventHandler("oncustomevent", typeof(CustomEventArgs), enableStopPropagation: true, enablePreventDefault: true)]
public static class EventHandlers
{
}