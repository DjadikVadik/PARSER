using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PARSER.Domain.ModelsDomain
{
    public class SubgroupDomain
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int GroupDomainId { get; set; }

        public override string ToString()
        {
            return $"{Id}) Name:{Name}";
        }
    }
}
