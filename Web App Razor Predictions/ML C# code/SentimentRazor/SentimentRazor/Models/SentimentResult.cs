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

        public string Prediction { get; set; }
    }
}
