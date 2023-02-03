using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PARSER.Parser
{
    public class ParserSetings
    {
        public string URL { get; set; }

        public readonly HttpClient httpClient;

        public ParserSetings(HttpClient httpClient, string URL = "https://www.ilcats.ru/toyota/?function=getModels&market=EU")
        {
            this.URL = URL;
            this.httpClient = httpClient;
        }
    }
}
