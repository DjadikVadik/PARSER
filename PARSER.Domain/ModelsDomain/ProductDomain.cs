using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PARSER.Domain.ModelsDomain
{
    public class ProductDomain
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public int Count { get; set; }
        public string Date { get; set; }
        public string Info { get; set; }
        public string Tree_code { get; set; }
        public string Tree { get; set; }
        public int SubgroupDomainId { get; set; }

        public override string ToString()
        {
            return $"{Id}) Code:{Code}, Count:{Count}, Date:{Date}, Info:{Info}, Tree_code:{Tree_code}, Tree:{Tree}";
        }
    }
}
