using PARSER.Domain.ModelsDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PARSER.Data.IRepository
{
    public interface IEquipmentInfoRepository
    {
        Task<bool> AddSingleAsync(EquipmentInfoDomain equipmentInfoDomain);
        Task<bool> AddRangeAsync(IEnumerable<EquipmentInfoDomain> list);
        Task<IEnumerable<EquipmentInfoDomain>?> GetAllAsync(int EquipmentId);
        Task<EquipmentInfoDomain?> GetSingleAsync(int equipmentInfoId);
        Task<bool> RemoveAsync(int equipmentInfoId);
        Task<bool> UpdateAsync(EquipmentInfoDomain NewEquipmentInfo);
    }
}
