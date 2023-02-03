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
    public class EquipmentInfoController
    {
        IEquipmentInfoRepository _repository;

        public EquipmentInfoController(SqlCommand command) => _repository = new EquipmentInfoRepository(command);

        public async Task<bool> AddSingleAsync(EquipmentInfoDomain equipmentInfoDomain)
        {
            return await _repository.AddSingleAsync(equipmentInfoDomain);
        }

        public async Task<bool> AddRangeAsync(IEnumerable<EquipmentInfoDomain> list)
        {
            return await _repository.AddRangeAsync(list);
        }

        public async Task<IEnumerable<EquipmentInfoDomain>?> GetAllAsync(int EquipmentId)
        {
            return await _repository.GetAllAsync(EquipmentId);
        }

        public async Task<EquipmentInfoDomain?> GetSingleAsync(int equipmentInfoId)
        {
            return await _repository.GetSingleAsync(equipmentInfoId);
        }

        public async Task<bool> DeleteAsync(int equipmentInfoId)
        {
            return await _repository.RemoveAsync(equipmentInfoId);
        }

        public async Task<bool> UpdateAsync(EquipmentInfoDomain NewEquipmentInfo)
        {
            return await _repository.UpdateAsync(NewEquipmentInfo);
        }
    }
}
