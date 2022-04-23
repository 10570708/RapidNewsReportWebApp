using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RapidNewsReportWebApp.Services;
using RapidNewsReportWebApp.Models;
using System.ComponentModel.DataAnnotations;
using RapidNewsReportWebApp.Common;
using Microsoft.AspNetCore.Authorization;

namespace RapidNewsReportWebApp.Pages.Reports
{
    [Authorize]
    public class IndexModel : PageModel
    {

        [TempData]
        public string? FormResult { get; set; }
        [TempData]
        public string? CommentFormResult { get; set; }


        private readonly NewsReportAPIClient _newsReportApiClient;
        private readonly NewsCommentAPIClient _newsCommentApiClient;

        public IndexModel(NewsReportAPIClient newsReportApiClient, NewsCommentAPIClient newsCommentApiClient)
        {
            _newsReportApiClient = newsReportApiClient;
            _newsCommentApiClient = newsCommentApiClient;
        }

        public string? responseContent { get; set; }


        public async Task<IActionResult> OnPostAddComment()
        {

            if (newComment.CommentText != null)
            {
                try
                {
                    bool success = await _newsCommentApiClient.PostComment(newComment);
                    AddCommentText = String.Empty;

                    return RedirectToPage("Index", new { Id = newComment.ReportId, view = true });
                }
                catch (Exception ex)
                {
                    myReport = await _newsReportApiClient.GetReport(newComment.ReportId);
                    errorCMessage = "WARNING: " + ReportErrorHandler.GetErrorMessages(ex.Message, false);
                    return Page();

                }
            }
            else
            {
                try
                {
                    errorCMessage = "Your comment has not been added";

                    myReport = await _newsReportApiClient.GetReport(newComment.ReportId);
                    Comments = await _newsCommentApiClient.GetComments(newComment.ReportId);
                }
                catch (Exception e)
                {
                    errorCMessage = ReportErrorHandler.GetErrorMessages(e.Message);
                }

                return Page();
            }
        }


        public async Task<IActionResult> OnPostPutComment(int id, int reportId, Guid createdBy)
        {

            Comment updated = new Comment();
            if (CommentText == null) CommentText = "";
            updated.ReportId = reportId;
            updated.CreatedBy = createdBy;
            updated.Id = id;
            updated.CommentText = CommentText;


            if (CommentText.Length > 0)
            {
                try
                {
                    bool success = await _newsCommentApiClient.PutComment(updated);
                    progressMessage = "Your comment has been updated";

                    return RedirectToPage("Index", new { Id = reportId, view = true });
                }
                catch (Exception ex)
                {
                    myReport = await _newsReportApiClient.GetReport(newComment.ReportId);
                    errorCMessage = "WARNING: " + ReportErrorHandler.GetErrorMessages(ex.Message, false);
                    return Page();
                }
            }
            else
            {
                try
                {
                    myReport = await _newsReportApiClient.GetReport(reportId);
                    Comments = await _newsCommentApiClient.GetComments(reportId);

                    commenterror = $"{id}";
                    errorCMessage = "Your comment has not been updated ...";
                    return Page();
                }
                catch (Exception e)
                {
                    errorCMessage = ReportErrorHandler.GetErrorMessages(e.Message);
                    return Page();
                }
            }

        }


        public async Task<IActionResult> OnPostViewComments(int id)
        {
            try
            {
                Comments = await _newsCommentApiClient.GetComments(id);
                myReport = await _newsReportApiClient.GetReport(id);
            }
            catch (Exception ex)
            {
                errorCMessage = ReportErrorHandler.GetErrorMessages(ex.Message);
            }
            return Page();
        }



        public async Task<IActionResult> OnPostDelete(int id, string? call)
        {
            try
            {
                bool success1 = await _newsReportApiClient.DeleteReport(id);
                bool success2 = false;

                // Delete the Report 
                if (success1)
                {
                    progressMessage = "Your Report has been successfully deleted.";
                    FormResult = progressMessage;

                    try
                    {
                        // Delete the Comments 
                        success2 = await _newsCommentApiClient.DeleteComments(id);
                        if (!success2)
                        {
                            progressMessage = "Your Report has been successfully deleted";
                            CommentFormResult = "Any Comments have not been deleted.";
                            FormResult = progressMessage;
                            return (call == "home" ? RedirectToPage("../Index") : Page());
                        }
                    }
                    catch (Exception ex)
                    {
                        if (!ex.Message.Contains("No connection"))
                        {
                            FormResult += progressMessage;
                            progressMessage = ex.Message;
                            return (call == "home" ? RedirectToPage("../Index") : Page());
                        }
                        else
                        {
                            CommentFormResult = "WARNING: " + ReportErrorHandler.GetErrorMessages(ex.Message, false);
                            return RedirectToPage("../Index");
                        }
                    }
                }
                else
                {
                    // If Report delete failed, load the main index page 
                    progressMessage = "We have encountered a problem deleting your News Report.";
                    CommentFormResult = progressMessage;
                    return RedirectToPage("../Index");
                }
            }
            catch (Exception ex)
            {
                // If Exceptions caught and NOT due to no connection
                // load page based on call parameter
                if (!ex.Message.Contains("No connection"))
                {
                    FormResult = progressMessage;
                    progressMessage = ex.Message;
                    return (call == "home" ? RedirectToPage("../Index") : Page());
                }
                else
                {
                    return RedirectToPage("../Index");
                }
            }

            // If all deletes successful and no Exceptions caught load main index page 
            return RedirectToPage("../Index");

        }

        public async Task<IActionResult> OnPostDeleteComment(int id, int reportId)
        {
            bool success = false;

            try
            {
                success = await _newsCommentApiClient.DeleteComment(id);
            }
            catch (Exception e)
            {
                errorCMessage = ReportErrorHandler.GetErrorMessages(e.Message, false);
                return Page();
            }

            if (!success)
            {
                errorCMessage = "Your comment has not beed deleted.";
            }
            else
            {
                FormResult = "Your Comment has been successfully deleted.";
            }

            try
            {
                myReport = await _newsReportApiClient.GetReport(reportId);
                Comments = await _newsCommentApiClient.GetComments(reportId);
            }
            catch (Exception e)
            {
                errorCMessage += ReportErrorHandler.GetErrorMessages(e.Message);
            }
            return Page();

        }

        public async Task<IActionResult> OnGetAsync(int ID, bool view = false)
        {
            if (ID == 0)
            {
                FormResult = "Please select a Report to View";
                return RedirectToPage("../Index");
            }
            else
            {
                try
                {
                    myReport = await _newsReportApiClient.GetReport(ID);
                    if (view) Comments = await _newsCommentApiClient.GetComments(ID);
                    return Page();
                }
                catch (Exception e)
                {
                    FormResult = ReportErrorHandler.GetErrorMessages(e.Message);
                    return RedirectToPage("../Index");
                }
            }
        }


        public async Task<IActionResult> OnPostFilter()
        {
            try
            {
                if (viewAll)
                {
                    Comments = await _newsCommentApiClient.GetComments(ID, viewDesc);
                }
                else
                {
                    Comments = await _newsCommentApiClient.GetComments(ID, createdBy, viewDesc);
                }

                myReport = await _newsReportApiClient.GetReport(ID);

            }
            catch (Exception e)
            {
                errorCMessage = "WARNING: " + ReportErrorHandler.GetErrorMessages(e.Message, false);
            }

            return Page();
        }




        [BindProperty]
        public string? commenterror { get; set; }

        [BindProperty]
        public string? CommentText { get; set; }

        [BindProperty]
        public string? AddCommentText { get; set; }

        [BindProperty]
        public Comment newComment { get; set; }

        [BindProperty]
        public bool viewAll { get; set; } = true;

        [BindProperty]
        public int ID { get; set; }

        [BindProperty]
        public Guid createdBy { get; set; }

        [BindProperty]
        public bool viewDesc { get; set; } = true;

        [BindProperty]
        public int reportId { get; set; } = 0;

        [BindProperty]
        public Report myReport { get; set; }

        public string? errorRMessage { get; set; }

        public string? progressMessage { get; set; }

        public string errorCMessage { get; set; }

        [BindProperty]
        public IEnumerable<Comment> Comments { get; set; } = Enumerable.Empty<Comment>();


    }
}
