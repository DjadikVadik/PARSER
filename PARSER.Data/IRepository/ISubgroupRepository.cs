using PARSER.Domain.ModelsDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PARSER.Data.IRepository
{
    public interface ISubgroupRepository
    {
        Task<bool> AddSingleAsync(SubgroupDomain subGroupDomain);
        Task<bool> AddRangeAsync(IEnumerable<SubgroupDomain> list);
        Task<IEnumerable<SubgroupDomain>?> GetAllAsync(int GroupId);
        Task<SubgroupDomain?> GetSingleAsync(int SubgroupId);
        Task<bool> RemoveAsync(int SubgroupId);
        Task<bool> UpdateAsync(SubgroupDomain NewSubgroup);
    }
}
