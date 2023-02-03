using Microsoft.Data.SqlClient;
using PARSER.Data.IRepository;
using PARSER.Data.Models;
using PARSER.Domain.ModelsDomain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace PARSER.Data.Repository
{
    public class ProductRepository : IProductRepository
    {
        SqlCommand _command;

        public ProductRepository(SqlCommand command) => _command = command;

        public async Task<bool> AddRangeAsync(IEnumerable<ProductDomain> list, int EquipmentInfoId)
        {
            foreach (var entity in list)
            {
                if (await AddSingleAsync(entity, EquipmentInfoId) == false) return false;
            }

            return true;
        }

        public async Task<bool> AddSingleAsync(ProductDomain productDomain, int EquipmentInfoId)
        {
            var entity = Maper.ToModel(productDomain);
            _command.CommandType = System.Data.CommandType.StoredProcedure;
            _command.CommandText = "AddProduct";
            _command.Parameters.Add(new SqlParameter("@code", entity.Code));
            _command.Parameters.Add(new SqlParameter("@count", entity.Count));
            _command.Parameters.Add(new SqlParameter("@date", entity.Date));
            _command.Parameters.Add(new SqlParameter("@info", entity.Info));
            _command.Parameters.Add(new SqlParameter("@tree_code", entity.Tree_code));
            _command.Parameters.Add(new SqlParameter("@tree", entity.Tree));
            _command.Parameters.Add(new SqlParameter("@subgroupId", entity.SubgroupId));
            _command.Parameters.Add(new SqlParameter("@equipmentInfoId", EquipmentInfoId));
            int result = await _command.ExecuteNonQueryAsync();
            _command.Parameters.Clear();

            return result > 0 ? true : false;
        }

        public async Task<IEnumerable<ProductDomain>?> GetAllAsync(int SubgroupId, int EquipmentInfoId)
        {
            _command.CommandType = System.Data.CommandType.StoredProcedure;
            _command.CommandText = "GetProductBySubgroupId_EquipmentInfoId";
            _command.Parameters.Add(new SqlParameter("@subgroupId", SubgroupId));
            _command.Parameters.Add(new SqlParameter("@equipmentInfoId", EquipmentInfoId));
            var reder = await _command.ExecuteReaderAsync();
            if (!reder.HasRows) return null;

            var list = new List<Product>();

            while (await reder.ReadAsync())
            {
                var entity = new Product();
                entity.Id = reder.GetInt32(0);
                entity.Code = reder.GetString(1);
                entity.Count = reder.GetInt32(2);
                entity.Date = reder.GetString(3);
                entity.Info = reder.GetString(4);
                entity.Tree_code = reder.GetString(5);
                entity.Tree = reder.GetString(6);
                entity.SubgroupId = reder.GetInt32(7);

                list.Add(entity);
            }

            reder.Close();
            _command.Parameters.Clear();

            return Maper.ToDomain(list);
        }

        public async Task<ProductDomain?> GetSingleAsync(int ProductId)
        {
            _command.CommandType = System.Data.CommandType.StoredProcedure;
            _command.CommandText = "GetSingleProduct";
            _command.Parameters.Add(new SqlParameter("@Id", ProductId));
            var reder = await _command.ExecuteReaderAsync();
            if (!reder.HasRows) return null;
            
            var entity = new Product();
            entity.Id = reder.GetInt32(0);
            entity.Code = reder.GetString(1);
            entity.Count = reder.GetInt32(2);
            entity.Date = reder.GetString(3);
            entity.Info = reder.GetString(4);
            entity.Tree_code = reder.GetString(5);
            entity.Tree = reder.GetString(6);
            entity.SubgroupId = reder.GetInt32(7);

            reder.Close();
            _command.Parameters.Clear();

            return Maper.ToDomain(entity);
        }

        public async Task<bool> RemoveAsync(int ProductId)
        {
            _command.CommandType = System.Data.CommandType.StoredProcedure;
            _command.CommandText = "DeleteProduct";
            _command.Parameters.Add(new SqlParameter("@Id", ProductId));
            int result = await _command.ExecuteNonQueryAsync();
            _command.Parameters.Clear();

            return result > 0 ? true : false;
        }

        public async Task<bool> UpdateAsync(ProductDomain NewProduct)
        {
            var entity = Maper.ToModel(NewProduct);
            _command.CommandType = System.Data.CommandType.StoredProcedure;
            _command.CommandText = "UpdateProduct";
            _command.Parameters.Add(new SqlParameter("@Id", entity.Id));
            _command.Parameters.Add(new SqlParameter("@code", entity.Code));
            _command.Parameters.Add(new SqlParameter("@count", entity.Count));
            _command.Parameters.Add(new SqlParameter("@date", entity.Date));
            _command.Parameters.Add(new SqlParameter("@info", entity.Info));
            _command.Parameters.Add(new SqlParameter("@tree_code", entity.Tree_code));
            _command.Parameters.Add(new SqlParameter("@tree", entity.Tree));
            _command.Parameters.Add(new SqlParameter("@subgroupId", entity.SubgroupId));
            int result = await _command.ExecuteNonQueryAsync();
            _command.Parameters.Clear();

            return result > 0 ? true : false;
        }
    }
}
