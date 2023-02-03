using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PARSER.Domain.ModelsDomain
{
    public class ImageDomain
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SubgroupDomainId { get; set; }

        public override string ToString()
        {
            return $"{Id}) Name:{Name}";
        }
    }
}
