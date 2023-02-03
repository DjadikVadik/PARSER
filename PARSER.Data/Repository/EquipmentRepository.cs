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
    public class EquipmentRepository : IEquipmentRepository
    {

        SqlCommand _command;

        public EquipmentRepository(SqlCommand command) => _command = command;

        public async Task<bool> AddSingleAsync(EquipmentDomain equipmentDomain)
        {
            var entity = Maper.ToModel(equipmentDomain);
            _command.CommandType = System.Data.CommandType.StoredProcedure;
            _command.CommandText = "AddEquipment";
            _command.Parameters.Add(new SqlParameter("@сode", entity.Code));
            _command.Parameters.Add(new SqlParameter("@date", entity.Date));
            _command.Parameters.Add(new SqlParameter("@allCodes", entity.AllCodes));
            _command.Parameters.Add(new SqlParameter("@modelId", entity.ModelId));
            int result = await _command.ExecuteNonQueryAsync();
            _command.Parameters.Clear();

            return result > 0 ? true : false;
        }

        public async Task<bool> AddRangeAsync(IEnumerable<EquipmentDomain> list)
        {
            foreach (var entity in list)
            {
                if (await AddSingleAsync(entity) == false) return false;
            }

            return true;
        }

        public async Task<IEnumerable<EquipmentDomain>?> GetAllAsync(int modelId)
        {
            _command.CommandType = System.Data.CommandType.StoredProcedure;
            _command.CommandText = "GetEquipmentByModelId";
            _command.Parameters.Add(new SqlParameter("@Id", modelId));
            var reder = await _command.ExecuteReaderAsync();
            if (!reder.HasRows) return null;

            var list = new List<Equipment>();

            while(await reder.ReadAsync())
            {
                var entity = new Equipment();

                entity.Id = reder.GetInt32(0);
                entity.Code = reder.GetString(1);
                entity.Date = reder.GetString(2);
                entity.AllCodes = reder.GetString(3);
                entity.ModelId = reder.GetInt32(4);

                list.Add(entity);
            }

            reder.Close();
            _command.Parameters.Clear();

            return Maper.ToDomain(list);
        }
       
        public async Task<EquipmentDomain?> GetSingleAsync(int equipmentId)
        {
            _command.CommandType = System.Data.CommandType.StoredProcedure;
            _command.CommandText = "GetSingleEquipment";
            _command.Parameters.Add(new SqlParameter("@Id", equipmentId));
            var reder = await _command.ExecuteReaderAsync();
            if (!reder.HasRows) return null;

            var entity = new Equipment();

            entity.Id = reder.GetInt32(0);
            entity.Code = reder.GetString(1);
            entity.Date = reder.GetString(2);
            entity.AllCodes = reder.GetString(3);
            entity.ModelId = reder.GetInt32(4);

            reder.Close();
            _command.Parameters.Clear();

            return Maper.ToDomain(entity);
        }

        public async Task<bool> RemoveAsync(int equipmentId)
        {
            _command.CommandType = System.Data.CommandType.StoredProcedure;
            _command.CommandText = "DeleteEquipment";
            _command.Parameters.Add(new SqlParameter("@Id", equipmentId));
            int result = await _command.ExecuteNonQueryAsync();
            _command.Parameters.Clear();

            return result > 0 ? true : false;
        }

        public async Task<bool> UpdateAsync(EquipmentDomain NewEquipment)
        {
            var entity = Maper.ToModel(NewEquipment);
            _command.CommandType = System.Data.CommandType.StoredProcedure;
            _command.CommandText = "UpdateEquipment";
            _command.Parameters.Add(new SqlParameter("@Id", entity.Id));
            _command.Parameters.Add(new SqlParameter("@code", entity.Code));
            _command.Parameters.Add(new SqlParameter("@date", entity.Date));
            _command.Parameters.Add(new SqlParameter("@allCodes", entity.AllCodes));
            _command.Parameters.Add(new SqlParameter("@modelId", entity.ModelId));
            int result = await _command.ExecuteNonQueryAsync();
            _command.Parameters.Clear();

            return result > 0 ? true : false;
        }
    }
}
