using System;
using ACI.Webapp.Application.Enums;
using Microsoft.AspNetCore.Mvc;

namespace ACI.Webapp.Presentation.Extensions;

public static class ControllerExtensions
{
    public static void AddToastMessage(this Controller controller, string toastMessage, ToastType toastType, string toastLabel)
    {
        controller.TempData["ToastLabel"] = toastLabel;
        controller.TempData["ToastMessage"] = toastMessage;
        controller.TempData["ToastType"] = toastType.ToString();
    }
}
