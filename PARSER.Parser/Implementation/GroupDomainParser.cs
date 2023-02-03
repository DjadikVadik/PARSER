using PARSER.Domain.ModelsDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PARSER.Parser.Implementation
{
    public class GroupDomainParser 
    {
        //ключ для находжения групп
        string Key = @"group=(\d)' title='' target=''>(.*?)</a>";

        string URL;

        HttpClient client;

        List<GroupDomain> GroupDomainList = new List<GroupDomain>();

        public GroupDomainParser(ParserSetings parserSetings)
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
                GroupDomainList.Add(new GroupDomain() { Name = item.Groups[2].Value });
        }

        public List<GroupDomain> GetGroupDomains() => GroupDomainList;
    }
}
