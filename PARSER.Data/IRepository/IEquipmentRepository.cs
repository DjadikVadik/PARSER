using PARSER.Domain.ModelsDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PARSER.Data.IRepository
{
    public interface IEquipmentRepository
    {
        Task<bool> AddSingleAsync(EquipmentDomain equipmentDomain);
        Task<bool> AddRangeAsync(IEnumerable<EquipmentDomain> list);
        Task<IEnumerable<EquipmentDomain>?> GetAllAsync(int modelId);
        Task<EquipmentDomain?> GetSingleAsync(int equipmentId);
        Task<bool> RemoveAsync(int equipmentId);
        Task<bool> UpdateAsync(EquipmentDomain NewEquipment);
    }
}
