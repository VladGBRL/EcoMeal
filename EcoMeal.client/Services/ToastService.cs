using System;
using EcoMeal.client.Models;

namespace EcoMeal.client.Services;

public class ToastService
{
    public event Action<ToastMessage>? OnShow;

    public void ShowSuccess(string message, string title = "Success") => Notify(message, title, ToastType.Success);
    public void ShowError(string message, string title = "Error") => Notify(message, title, ToastType.Error);
    public void ShowWarning(string message, string title = "Warning") => Notify(message, title, ToastType.Warning);
    public void ShowInfo(string message, string title = "Info") => Notify(message, title, ToastType.Info);

    private void Notify(string message, string title, ToastType type)
    {
        OnShow?.Invoke(new ToastMessage
        {
            Title = title,
            Message = message,
            Type = type
        });
    }
}
