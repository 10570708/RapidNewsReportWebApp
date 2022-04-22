namespace RapidNewsReportWebApp.Common
{
    public static class ReportErrorHandler
    {
        public static string GetErrorMessages(string message, bool report=true)
        {
            string strService = (report == true ? "News Report " : "Comments ");
            if (message.Contains("No connection"))
            {
                return $"The {strService} Service is not running. "  + (report == true ? "Please try again." : "");
            }
            else if (message.Contains("404"))
            {
                return "This item could not be found.";
            }
            else if (message.Contains("400"))
            {
                return "This request was not correctly formed.";
            }
            else if (message.Contains("500"))
            {
                return $"The {strService} Service could not process your request.";
            }
            else
            {
                return message;
            }

        }
    }
}
