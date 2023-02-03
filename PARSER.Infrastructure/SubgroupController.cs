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
    public class SubgroupController
    {
        ISubgroupRepository _repository;

        public SubgroupController(SqlCommand command) => _repository = new SubgroupRepository(command);

        public async Task<bool> AddRangeAsync(IEnumerable<SubgroupDomain> list)
        {
            return await _repository.AddRangeAsync(list);
        }

        public async Task<bool> AddSingleAsync(SubgroupDomain subGroupDomain)
        {
            return await _repository.AddSingleAsync(subGroupDomain);
        }

        public async Task<IEnumerable<SubgroupDomain>?> GetAllAsync(int GroupId)
        {
            return await _repository.GetAllAsync(GroupId);  
        }

        public async Task<SubgroupDomain?> GetSingleAsync(int SubgroupId)
        {
            return await _repository.GetSingleAsync(SubgroupId);
        }

        public async Task<bool> DeleteAsync(int SubgroupId)
        {
            return await _repository.RemoveAsync(SubgroupId);
        }

        public async Task<bool> UpdateAsync(SubgroupDomain NewSubgroup)
        {
            return await _repository.UpdateAsync(NewSubgroup);
        }
    }
}
