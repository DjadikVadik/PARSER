using PARSER.Domain.ModelsDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PARSER.Parser.Implementation
{
    public class ImageDomainParser 
    {
        // Ключ для получения ссылки на картинку
        string Key = @"<img src='(.*?)' alt=''";

        string URL;

        HttpClient client;

        ImageDomain imageDomain;

        public ImageDomainParser(ParserSetings parserSetings)
        {
            client = parserSetings.httpClient;
            URL = parserSetings.URL;
            Pars();
        }

        void Pars()
        {
            imageDomain = new ImageDomain() { Name = $"Images\\{Guid.NewGuid()}.png", SubgroupDomainId = 1 };

            string response = client.GetStringAsync(URL).Result;

            var regex = new Regex(Key);
            var match = regex.Match(response);

            var response1 = client.GetByteArrayAsync($"https:{match.Groups[1].Value}").Result;

            Directory.CreateDirectory("Images");
            using (var stream = new FileStream(imageDomain.Name, FileMode.Create))
            {
                stream.Write(response1, 0, response1.Length);
            }
        }

        public ImageDomain GetImageDomain() => imageDomain;
    }
}
