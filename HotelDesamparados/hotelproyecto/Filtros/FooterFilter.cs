using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class FooterFilter : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        var controller = context.Controller as Controller;
        if (controller != null)
        {
            controller.ViewBag.Facebook = "https://www.facebook.com/profile.php?id=100083148709754";
            controller.ViewBag.Instagram = "https://www.instagram.com/granhotel_desamparados";
            controller.ViewBag.WhatsApp = "https://wa.me/50686467728";
            controller.ViewBag.TelefonoP = "+(506)8646-7728";
            controller.ViewBag.CorreoP = "recuperacionhotelproy@gmail.com";
            controller.ViewBag.UbicacionP = "https://maps.app.goo.gl/7kjLjGKguCc3Nmtt8";
            controller.ViewBag.NombreUbicacion = "San José, Costa Rica";
        }
    }

    public void OnActionExecuted(ActionExecutedContext context) { }
}
