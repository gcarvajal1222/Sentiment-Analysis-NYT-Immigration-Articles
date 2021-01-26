using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SentimentRazor.Models
{
    public class response
    {

        [JsonProperty("docs")]
        public List<Docs> docs;
    }

    public class Docs
    {
        public string lead_paragraph;
    }

}
