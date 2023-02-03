using PARSER.Domain.ModelsDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PARSER.Data.IRepository
{
    public interface IProductRepository
    {
        Task<bool> AddSingleAsync(ProductDomain productDomain, int EquipmentInfoId);
        Task<bool> AddRangeAsync(IEnumerable<ProductDomain> list, int EquipmentInfoId);
        Task<IEnumerable<ProductDomain>?> GetAllAsync(int SubgroupId, int EquipmentInfoId);
        Task<ProductDomain?> GetSingleAsync(int ProductId);
        Task<bool> RemoveAsync(int ProductId);
        Task<bool> UpdateAsync(ProductDomain NewProduct);
    }
}
