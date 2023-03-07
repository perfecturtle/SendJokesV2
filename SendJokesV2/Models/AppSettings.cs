using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SendJokesV2.Models
{
    public class AppSettings
    {
        public string BaseURLJokes { get; set; }
        public string BaseURLComics { get; set; }
        public string BaseURLRandomFacts { get; set; }
        public string SendGridAPIKey { get; set; }
        public string RandomFactsAPIKey { get; set; }
    }
}
