using System.ComponentModel.DataAnnotations;


namespace RapidNewsReportWebApp.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int ReportId { get; set; }
        public Guid CreatedBy { get; set; }
        public string CommentText { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime UpdatedDate { get; set; }

    }
}
