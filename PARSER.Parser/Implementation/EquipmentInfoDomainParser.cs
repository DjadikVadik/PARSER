using PARSER.Domain.ModelsDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PARSER.Parser.Implementation
{
    public class EquipmentInfoDomainParser 
    {
        //ключ для находжения кода и даты модификации
        string Key1 = @"([\w\d\-]+)</a></div></td><td><div class='dateRange'>([\d.]+)(.*?)([\d.]+)(.*?)<td></td><td></td><td></td><td></td></tr>";

        //ключ для нахождения остальных характеристик
        string Key2 = @"<td>(.*?)</td><td>(.*?)</td><td>(.*?)</td><td>(.*?)</td><td>(.*?)</td><td>(.*?)</td><td>(.*?)</td><td>(.*?)</td>";

        string URL;

        HttpClient client;

        List<EquipmentInfoDomain> EquipmentInfosList = new List<EquipmentInfoDomain>();

        public EquipmentInfoDomainParser(ParserSetings parserSetings)
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

            foreach (Match item in collection)
            {
                var regex1 = new Regex(Key2);
                var match = regex1.Match(item.Groups[5].Value);
                EquipmentInfosList.Add(new EquipmentInfoDomain()
                {
                    Name = item.Groups[1].Value,
                    Date = $"{item.Groups[2].Value} - {item.Groups[4].Value}",
                    ENGINE = Regex.Match(match.Groups[1].Value, "<div class='01'>(.*?)</div>").Groups[1].Value,
                    BODY = Regex.Match(match.Groups[2].Value, "<div class='03'>(.*?)</div>").Groups[1].Value,
                    GRADE = Regex.Match(match.Groups[3].Value, "<div class='04'>(.*?)</div>").Groups[1].Value,
                    ATM_MTM = Regex.Match(match.Groups[4].Value, "<div class='05'>(.*?)</div>").Groups[1].Value,
                    GEAR_SHIFT_TYPE = Regex.Match(match.Groups[5].Value, "<div class='06'>(.*?)</div>").Groups[1].Value,
                    CAB = Regex.Match(match.Groups[6].Value, "<div class='07'>(.*?)</div>").Groups[1].Value,
                    TRANSMISSION_MODEL = Regex.Match(match.Groups[7].Value, "<div class='08'>(.*?)</div>").Groups[1].Value,
                    LOADING_CAPACITY = Regex.Match(match.Groups[8].Value, "<div class='09'>(.*?)</div>").Groups[1].Value,
                    EquipmentDomainId = 1
                });
            }
        }

        public List<EquipmentInfoDomain> GetEquipmentInfos() => EquipmentInfosList;
    }
}
