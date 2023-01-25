using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SentimentRazor.Models
{
    public class JsonBodyResponse
    {
        public string ArticlesCounts { get; set; }
        public List<Docs> response { get; set; }
    }
}
