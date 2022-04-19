using System.ComponentModel.DataAnnotations;


namespace RapidNewsReportWebApp.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int ReportId { get; set; }
        public Guid CreatedBy { get; set; }
        
        [   Required(ErrorMessage="Your Comment must be between 10 and 100 characters."), 
            MinLength(10, ErrorMessage = "Your News Report Title must be between 10 and 100 characters."), 
            MaxLength(100, ErrorMessage = "Your News Report Title must be between 10 and 100 characters.")
        ] 
        public string CommentText { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime UpdatedDate { get; set; }

    }
}
