using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SentimentRazor.Models
{
    public class ResponseAPI
    {
        [JsonProperty("response")]
        public Response response { get; set; }
    }

    public class Response
    {
        [JsonProperty("docs")]
        public List<Docs> docs;
    }

    public class Docs
    {
        public string lead_paragraph;
    }
}
