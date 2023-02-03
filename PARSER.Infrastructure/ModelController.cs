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
    public class ModelController
    {
        IModelRepository _repository;

        public ModelController(SqlCommand command) => _repository = new ModelRepository(command);

        public async Task<bool> AddSingleAsync(ModelDomain modelDomain)
        {
            return await _repository.AddSingleAsync(modelDomain);
        }

        public async Task<bool> AddRangeAsync(IEnumerable<ModelDomain> list)
        {
            return await _repository.AddRangeAsync(list);
        }

        public async Task<IEnumerable<ModelDomain>?> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<ModelDomain?> GetSyngleAsync(int modelId)
        {
            return await _repository.GetSyngleAsync(modelId);
        }

        public async Task<bool> DeleteAsync(int modelId)
        {
            return await _repository.RemoveAsync(modelId);
        }

        public async Task<bool> UpdateAsync(ModelDomain newModel)
        {
            return await _repository.UpdateAsync(newModel);
        }
    }
}
