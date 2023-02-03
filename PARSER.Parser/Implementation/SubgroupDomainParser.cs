using PARSER.Domain.ModelsDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PARSER.Parser.Implementation
{
    public class SubgroupDomainParser 
    {
        //ключ для находжения подгрупп
        string Key = @"class='name'>(.*?)target=''>(.*?)</a></div>";

        string URL;

        HttpClient client;

        List<SubgroupDomain> subgroupDomains = new List<SubgroupDomain>();

        public SubgroupDomainParser(ParserSetings parserSetings)
        {
            client = parserSetings.httpClient;
            URL = parserSetings.URL;
            Pars();
        }

        void Pars()
        {
            string response = client.GetStringAsync(URL).Result;

            var regex = new Regex(Key);
            var collection = regex.Matches(response);

            foreach (Match item in collection)
                subgroupDomains.Add(new SubgroupDomain() { Name = item.Groups[2].Value, GroupDomainId = 1 });
        }

        public List<SubgroupDomain> GetSubgroupDomains() => subgroupDomains;
    }
}
