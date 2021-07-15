using TextSnippetDemo.Domain.Models;

namespace TextSnippetDemo.Application.Constants
{
    public static class ErrorResponseConstants
    {
        // These data should retrived from database for centralization and customization
        public static readonly ErrorResponse UnexpectedError = new()
        {
            Error = "unexpected-0001",
            Message = "There is something wrong happens",
            Detail = "There is something wrong happens. Please try again. If it doesn't work, please contact with us."
        };
    }
}
