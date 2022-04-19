using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RapidNewsReportWebApp.Models
{
    public class Report
    {
        public int Id { get; set; }

        [   Required(ErrorMessage="Your News Report Title must be between 10 and 100 characters."), 
            MinLength(10, ErrorMessage = "Your News Report Title must be between 10 and 100 characters."), 
            MaxLength(100, ErrorMessage = "Your News Report Title must be between 10 and 100 characters.")
        ] 
        public string Title { get; set; } = "";

        [   Required(ErrorMessage="Your News Report cannot be empty."), 
            MinLength(50, ErrorMessage = "Your News Report Title must be between 50 and 5000 characters."), 
            MaxLength(1000,ErrorMessage = "Your News Report Title must be between 50 and 5000 characters.")
        ]
        public string Content { get; set; } = "";

        [Required, DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; }

        [Required, DataType(DataType.DateTime)]
        public DateTime UpdatedDate { get; set; }

        public bool IsPublished { get; set; } = false;

        [DataType(DataType.DateTime)]
        public DateTime PublishedDate { get; set; }

	[Required(ErrorMessage="Please select a valid Category")]
        public CategoryType Category { get; set; }

        [Required]
        public Guid CreatedBy { get; set; }
    }


    public enum CategoryType
    {
        [Display(Name = "Local News")]
        LocalNews = 1,
        [Display(Name = "World News")]
        WorldNews = 2,
        Sport = 3,
        Entertainment = 4,
        Weather = 5,
        Politics = 6,
        Opinion = 7,
        [Display(Name = "Food & Drink")]
        FoodDrink = 8,
    }
}
