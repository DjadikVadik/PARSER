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
    public class EquipmentController
    {
        IEquipmentRepository _repository;

        public EquipmentController(SqlCommand command) => _repository = new EquipmentRepository(command);

        public async Task<bool> AddSingleAsync(EquipmentDomain equipmentDomain)
        {
            return await _repository.AddSingleAsync(equipmentDomain);
        }

        public async Task<bool> AddRangeAsync(IEnumerable<EquipmentDomain> list)
        {
            return await _repository.AddRangeAsync(list);
        }

        public async Task<IEnumerable<EquipmentDomain>?> GetAllAsync(int modelId)
        {
            return await _repository.GetAllAsync(modelId);
        }

        public async Task<EquipmentDomain?> GetSingleAsync(int equipmentId)
        {
            return await _repository.GetSingleAsync(equipmentId);
        }

        public async Task<bool> DeleteAsync(int equipmentId)
        {
            return await _repository.RemoveAsync(equipmentId);
        }

        public async Task<bool> UpdateAsync(EquipmentDomain NewEquipment)
        {
            return await _repository.UpdateAsync(NewEquipment);
        }
    }
}
