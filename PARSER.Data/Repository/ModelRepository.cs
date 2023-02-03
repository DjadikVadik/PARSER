using Microsoft.Data.SqlClient;
using PARSER.Data.IRepository;
using PARSER.Data.Models;
using PARSER.Domain.ModelsDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PARSER.Data.Repository
{
    public class ModelRepository : IModelRepository
    {
        SqlCommand _command;

        public ModelRepository(SqlCommand command) => _command = command;

        public async Task<bool> AddSingleAsync(ModelDomain modelDomain)
        {
            var entity = Maper.ToModel(modelDomain);
            _command.CommandType = System.Data.CommandType.StoredProcedure;
            _command.CommandText = "AddModel";
            _command.Parameters.Add(new SqlParameter("@name", entity.Name));
            int result = await _command.ExecuteNonQueryAsync();
            _command.Parameters.Clear();

            return result > 0 ? true : false;
        }

        public async Task<bool> AddRangeAsync(IEnumerable<ModelDomain> list)
        {
            foreach (var entity in list)
            {
                if (await AddSingleAsync(entity) == false) return false;
            }

            return true;
        }

        public async Task<IEnumerable<ModelDomain>?> GetAllAsync()
        {
            _command.CommandType = System.Data.CommandType.StoredProcedure;
            _command.CommandText = "GetAllModel";
            var reder = await _command.ExecuteReaderAsync();
            if (!reder.HasRows) return null;

            var list = new List<Model>();

            while(await reder.ReadAsync())
            {
                var entity = new Model();
                entity.Id = reder.GetInt32(0);
                entity.Name = reder.GetString(1);
                list.Add(entity);
            }

            reder.Close();

            return Maper.ToDomain(list);
        }

        public async  Task<ModelDomain?> GetSyngleAsync(int modelId)
        {
            _command.CommandType = System.Data.CommandType.StoredProcedure;
            _command.CommandText = "GetSingleModel";
            _command.Parameters.Add(new SqlParameter("@Id", modelId));
            var reder = await _command.ExecuteReaderAsync();
            if (!reder.HasRows) return null;

            var entity = new Model();
            entity.Id = reder.GetInt32(0);
            entity.Name = reder.GetString(1);

            reder.Close();
            _command.Parameters.Clear();

            return Maper.ToDomain(entity);
        }

        public async Task<bool> RemoveAsync(int modelId)
        {
            _command.CommandType = System.Data.CommandType.StoredProcedure;
            _command.CommandText = "DeleteModel";
            _command.Parameters.Add(new SqlParameter("@Id", modelId));
            int result = await _command.ExecuteNonQueryAsync();
            _command.Parameters.Clear();

            return result > 0 ? true : false;
        }

        public async Task<bool> UpdateAsync(ModelDomain newModel)
        {
            var entity = Maper.ToModel(newModel);
            _command.CommandType = System.Data.CommandType.StoredProcedure;
            _command.CommandText = "UpdateModel";
            _command.Parameters.Add(new SqlParameter("@Id", entity.Id));
            _command.Parameters.Add(new SqlParameter("@name", entity.Name));
            int result = await _command.ExecuteNonQueryAsync();
            _command.Parameters.Clear();

            return result > 0 ? true : false;
        }
    }
}
