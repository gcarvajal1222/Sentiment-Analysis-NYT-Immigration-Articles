using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace SentimentRazor.Models
{
    public class SentimentResult
    {

        [Key]
        public int Id { get; set; }

        [Required]

        public string Text { get; set; }

        public string SentimentPrediction { get; set; }

        public string ConfidenceScore { get; set; }

        public string ActualSentiment { get; set; }

        public DateTime? SavedDate { get; set; } = DateTime.Now;


    }
}
