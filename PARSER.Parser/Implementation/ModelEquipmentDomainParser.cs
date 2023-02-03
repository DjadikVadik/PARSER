using PARSER.Domain.ModelsDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PARSER.Parser.Implementation
{
    // в этом классе мы получим колекцию моделей а также коллекцию связянных с этой моделью модификаций
    public class ModelEquipmentDomainParser 
    {
        //ключ для находжения модели и списка модефикаций
        string Key1 = @"<div class='name'>([\w\d()\-.,\s/+]+)</div>(.*?)</div></div></div>";

        //ключ для нахождения списка модификаций
        string Key2 = @"target=''>(\d+)</a></div><div class='dateRange'>([\d.]+)(.*?)([\d.]+)(.*?)'modelCode'>([\d.\w#,]+)";

        string URL;

        HttpClient client;

        List<ModelDomain> ModelLst = new List<ModelDomain>(); 

        List<EquipmentDomain> EquipmentLst = new List<EquipmentDomain>();

        public ModelEquipmentDomainParser(ParserSetings parserSetings)
        {
            client = parserSetings.httpClient;
            URL = parserSetings.URL;
            Pars();
        }

        void Pars()
        {
            string response = client.GetStringAsync(URL).Result;

            var regex = new Regex(Key1);
            var collection = regex.Matches(response);

            int n = 1;
            foreach (Match item in collection)
            {
                ModelLst.Add(new ModelDomain() { Name = item.Groups[1].Value });

                var regex1 = new Regex(Key2);
                var collection1 = regex1.Matches(item.Groups[2].Value);

                foreach (Match item1 in collection1)
                    EquipmentLst.Add(new EquipmentDomain() { Code = item1.Groups[1].Value, Date = $"{item1.Groups[2].Value} - {item1.Groups[4].Value}", AllCodes = item1.Groups[6].Value, ModelDomainId = n });
                n++;
            }
        }

        public List<ModelDomain> GetModelDomains() => ModelLst;
        public List<EquipmentDomain> GetEquipmentDomains() => EquipmentLst;
    }
}
