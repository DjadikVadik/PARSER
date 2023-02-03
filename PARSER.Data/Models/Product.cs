using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PARSER.Data.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public int Count { get; set; }
        public string Date { get; set; }
        public string Info { get; set; }
        public string Tree_code { get; set; }
        public string Tree { get; set; }
        public int SubgroupId { get; set; }
    }
}
