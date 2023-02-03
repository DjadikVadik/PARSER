using PARSER.Domain.ModelsDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PARSER.Data.IRepository
{
    public interface IModelRepository
    {
        Task<bool> AddSingleAsync(ModelDomain modelDomain);
        Task<bool> AddRangeAsync(IEnumerable<ModelDomain> list);
        Task<IEnumerable<ModelDomain>?> GetAllAsync();
        Task<ModelDomain?> GetSyngleAsync(int modelId);
        Task<bool> RemoveAsync(int modelId);
        Task<bool> UpdateAsync(ModelDomain newModel);
    }
}
