using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PARSER.Domain.ModelsDomain
{
    public class EquipmentDomain
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Date { get; set; }
        public string AllCodes { get; set; }
        public int ModelDomainId { get; set; }

        public override string ToString()
        {
            return $"{Id}) Code:{Code}, Date:{Date}, AllCodes:{AllCodes}";
        }
    }
}
