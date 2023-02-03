using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PARSER.Domain.ModelsDomain
{
    public class EquipmentInfoDomain
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
        public string ENGINE { get; set; }
        public string BODY { get; set; }
        public string GRADE { get; set; }
        public string ATM_MTM { get; set; }
        public string GEAR_SHIFT_TYPE { get; set; }
        public string CAB { get; set; }
        public string TRANSMISSION_MODEL { get; set; }
        public string LOADING_CAPACITY { get; set; }
        public int EquipmentDomainId { get; set; }


        public override string ToString()
        {
            return $"{Id}) Name:{Name}, Date:{Date}, ENGINE:{ENGINE}, BODY:{BODY}, GRADE:{GRADE}, ATM_MTM:{ATM_MTM}, GEAR_SHIFT_TYPE:{GEAR_SHIFT_TYPE}, CAB:{CAB}, TRANSMISSION_MODEL:{TRANSMISSION_MODEL}, LOADING_CAPACITY:{LOADING_CAPACITY}";
        }
    }
}
