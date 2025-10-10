using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FitTrackPro.Pages
{
    public class HelloWorldModel : PageModel
    {
        // Define a public property to hold the message for the page.
        // It can be accessed from the .cshtml file.
        public string Message { get; private set; }

        // The OnGet() method is executed when the page is requested via an HTTP GET request.
        // This is the ideal place to prepare the initial data for the page.
        public void OnGet()
        {
            // Assign a value to the Message property.
            Message = "Hello, World! This message is from the PageModel.";
        }
    }

}
