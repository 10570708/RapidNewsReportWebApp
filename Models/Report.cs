﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace RapidNewsReportWebApp.Models
{
    public class Report
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime UpdatedDate { get; set; }

        public bool IsPublished { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime PublishedDate { get; set; }

        public string Category { get; set; }

        public Guid CreatedBy { get; set; }

    }

    public class PReport
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime CreatedDate { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime UpdatedDate { get; set; }

        public bool IsPublished { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime PublishedDate { get; set; }

        public CategoryType Category { get; set; }

        public Guid CreatedBy { get; set; }

    }

    public enum CategoryType
    {
        [Display(Name = "Local News")]
        LocalNews = 0,
        [Display(Name = "World News")]
        WorldNews = 1,
        Sport = 2,
        Entertainment = 3,
        Weather = 4,
        Politics = 5,
        Opinion = 6,
        [Display(Name = "Food & Drink")]
        FoodDrink = 7,
    }
}