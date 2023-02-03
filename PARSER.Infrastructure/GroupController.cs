using Microsoft.Data.SqlClient;
using PARSER.Data.IRepository;
using PARSER.Data.Repository;
using PARSER.Domain.ModelsDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PARSER.Infrastructure
{
    public class GroupController
    {
        IGroupRepository _repository;

        public GroupController(SqlCommand command) => _repository = new GroupRepository(command);

        public async Task<bool> AddRangeAsync(IEnumerable<GroupDomain> list)
        {
            return await _repository.AddRangeAsync(list);
        }

        public async Task<bool> AddSingleAsync(GroupDomain groupDomain)
        {
            return await _repository.AddSingleAsync(groupDomain);
        }

        public async Task<IEnumerable<GroupDomain>?> GetAllAsync()
        {
            return await _repository.GetAllAsync(); 
        }

        public async Task<GroupDomain?> GetSingleAsync(int GroupId)
        {
            return await _repository.GetSingleAsync(GroupId);
        }

        public async Task<bool> DeleteAsync(int GroupId)
        {
            return await _repository.RemoveAsync(GroupId);
        }

        public async Task<bool> UpdateAsync(GroupDomain NewGroup)
        {
            return await _repository.UpdateAsync(NewGroup);
        }
    }
}
