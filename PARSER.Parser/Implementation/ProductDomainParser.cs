using PARSER.Domain.ModelsDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PARSER.Parser.Implementation
{
    public class ProductDomainParser
    {
        //ключ для находжения продуктов
        string Key = @"colspan=4>([\w\d]+)&nbsp;(.*?)</th>(.*?)_blank'>(\d+)(.*?)class='count'>(\d+)(.*?)class='dateRange'>([\d.]+)(.*?)([\d.]+)(.*?)'usage'>(.*?)</div></td></tr>";

        string URL;

        HttpClient client;

        List<ProductDomain> productDomainsList = new List<ProductDomain>();

        public ProductDomainParser(ParserSetings parserSetings)
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
                productDomainsList.Add(new ProductDomain()
                {
                    Tree_code = item.Groups[1].Value,
                    Tree = item.Groups[2].Value,
                    Code = item.Groups[4].Value,
                    Count = Convert.ToInt32(item.Groups[6].Value),
                    Date = $"{item.Groups[8].Value} - {item.Groups[10].Value}",
                    Info = item.Groups[12].Value,
                    SubgroupDomainId = 1
                }); ;
        }

        public List<ProductDomain> GetProductDomains() => productDomainsList;
    }
}
