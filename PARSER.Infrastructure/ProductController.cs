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
    public class ProductController
    {
        IProductRepository _repository;

        public ProductController(SqlCommand command) => _repository = new ProductRepository(command);

        public async Task<bool> AddRangeAsync(IEnumerable<ProductDomain> list, int EquipmentInfoId)
        {
            return await _repository.AddRangeAsync(list, EquipmentInfoId);
        }

        public async Task<bool> AddSingleAsync(ProductDomain productDomain, int EquipmentInfoId)
        {
            return await _repository.AddSingleAsync(productDomain, EquipmentInfoId);
        }

        public async Task<IEnumerable<ProductDomain>?> GetAllAsync(int SubgroupId, int EquipmentInfoId)
        {
            return await _repository.GetAllAsync(SubgroupId, EquipmentInfoId);
        }

        public async Task<ProductDomain?> GetSingleAsync(int ProductId)
        {
            return await _repository.GetSingleAsync(ProductId);
        }

        public async Task<bool> DeleteAsync(int ProductId)
        {
            return await _repository.RemoveAsync(ProductId);
        }

        public async Task<bool> UpdateAsync(ProductDomain NewProduct)
        {
            return await _repository.UpdateAsync(NewProduct);
        }
    }
}
