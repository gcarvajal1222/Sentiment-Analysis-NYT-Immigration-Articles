using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NYT.Sentiment.Infrastrcture.Clients
{

    
    public class NYTClient
    {
        private readonly IHttpClientFactory? _httpclientFactory;
        public NYTClient(IHttpClientFactory httpclientfactory)
        {
            _httpclientFactory = httpclientfactory;
        }



    }
}
