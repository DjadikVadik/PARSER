using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PARSER.Data.Models
{
    public class Equipment
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Date { get; set; }
        public string AllCodes { get; set; }
        public int ModelId { get; set; }
    }
}
