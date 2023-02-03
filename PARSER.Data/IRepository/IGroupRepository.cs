using PARSER.Domain.ModelsDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PARSER.Data.IRepository
{
    public interface IGroupRepository
    {
        Task<bool> AddSingleAsync(GroupDomain groupDomain);
        Task<bool> AddRangeAsync(IEnumerable<GroupDomain> list);
        Task<IEnumerable<GroupDomain>?> GetAllAsync();
        Task<GroupDomain?> GetSingleAsync(int GroupId);
        Task<bool> RemoveAsync(int GroupId);
        Task<bool> UpdateAsync(GroupDomain NewGroup);
    }
}
