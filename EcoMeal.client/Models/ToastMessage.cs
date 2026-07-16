using System;

namespace EcoMeal.client.Models;

public class ToastMessage
{
    public Guid Id { get; } = Guid.NewGuid();
    public string Title { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public ToastType Type { get; set; } = ToastType.Info;
    public DateTime Timestamp { get; } = DateTime.Now;
}
