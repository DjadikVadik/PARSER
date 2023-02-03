using Microsoft.Data.SqlClient;
using PARSER.Data.IRepository;
using PARSER.Data.Models;
using PARSER.Domain.ModelsDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PARSER.Data.Repository
{
    public class EquipmentInfoRepository : IEquipmentInfoRepository
    {
        SqlCommand _command;

        public EquipmentInfoRepository(SqlCommand command) => _command = command;

        public async Task<bool> AddSingleAsync(EquipmentInfoDomain equipmentInfoDomain)
        {
            var entity = Maper.ToModel(equipmentInfoDomain);
            _command.CommandType = System.Data.CommandType.StoredProcedure;
            _command.CommandText = "AddEquipmentInfo";
            _command.Parameters.Add(new SqlParameter("@name", entity.Name));
            _command.Parameters.Add(new SqlParameter("@date", entity.Date));
            _command.Parameters.Add(new SqlParameter("@eNGINE", entity.ENGINE));
            _command.Parameters.Add(new SqlParameter("@bODY", entity.BODY));
            _command.Parameters.Add(new SqlParameter("@gRADE", entity.GRADE));
            _command.Parameters.Add(new SqlParameter("@aTM_MTM", entity.ATM_MTM));
            _command.Parameters.Add(new SqlParameter("@gEAR_SHIFT_TYPE", entity.GEAR_SHIFT_TYPE));
            _command.Parameters.Add(new SqlParameter("@cAB", entity.CAB));
            _command.Parameters.Add(new SqlParameter("@tRANSMISSION_MODEL", entity.TRANSMISSION_MODEL));
            _command.Parameters.Add(new SqlParameter("@lOADING_CAPACITY", entity.LOADING_CAPACITY));
            _command.Parameters.Add(new SqlParameter("@equipmentId", entity.EquipmentId));
            int result = await _command.ExecuteNonQueryAsync();
            _command.Parameters.Clear();

            return result > 0 ? true : false;
        }

        public async Task<bool> AddRangeAsync(IEnumerable<EquipmentInfoDomain> list)
        {
            foreach (var entity in list)
            {
                if (await AddSingleAsync(entity) == false) return false;
            }

            return true;
        }

        public async Task<IEnumerable<EquipmentInfoDomain>?> GetAllAsync(int EquipmentId)
        {
            _command.CommandType = System.Data.CommandType.StoredProcedure;
            _command.CommandText = "GetEquipmentInfoByEquipmentId";
            _command.Parameters.Add(new SqlParameter("@Id", EquipmentId));
            var reder = await _command.ExecuteReaderAsync();
            if (!reder.HasRows) return null;

            var list = new List<EquipmentInfo>();

            while (await reder.ReadAsync())
            {
                var entity = new EquipmentInfo();

                entity.Id = reder.GetInt32(0);
                entity.Name = reder.GetString(1);
                entity.Date = reder.GetString(2);
                entity.ENGINE = reder.GetString(3);
                entity.BODY = reder.GetString(4);
                entity.GRADE = reder.GetString(5);
                entity.ATM_MTM = reder.GetString(6);
                entity.GEAR_SHIFT_TYPE = reder.GetString(7);
                entity.CAB = reder.GetString(8);
                entity.TRANSMISSION_MODEL = reder.GetString(9);
                entity.LOADING_CAPACITY= reder.GetString(10);
                entity.EquipmentId= reder.GetInt32(11);

                list.Add(entity);
            }
            reder.Close();
            _command.Parameters.Clear();

            return Maper.ToDomain(list);
        }

        public async Task<EquipmentInfoDomain?> GetSingleAsync(int equipmentInfoId)
        {
            _command.CommandType = System.Data.CommandType.StoredProcedure;
            _command.CommandText = "GetSingleEquipmentInfo";
            _command.Parameters.Add(new SqlParameter("@Id", equipmentInfoId));
            var reder = await _command.ExecuteReaderAsync();
            if (!reder.HasRows) return null;

            var entity = new EquipmentInfo();

            entity.Id = reder.GetInt32(0);
            entity.Name = reder.GetString(1);
            entity.Date = reder.GetString(2);
            entity.ENGINE = reder.GetString(3);
            entity.BODY = reder.GetString(4);
            entity.GRADE = reder.GetString(5);
            entity.ATM_MTM = reder.GetString(6);
            entity.GEAR_SHIFT_TYPE = reder.GetString(7);
            entity.CAB = reder.GetString(8);
            entity.TRANSMISSION_MODEL = reder.GetString(9);
            entity.LOADING_CAPACITY = reder.GetString(10);
            entity.EquipmentId = reder.GetInt32(11);

            reder.Close();
            _command.Parameters.Clear();

            return Maper.ToDomain(entity);
        }

        public async Task<bool> RemoveAsync(int equipmentInfoId)
        {
            _command.CommandType = System.Data.CommandType.StoredProcedure;
            _command.CommandText = "DeleteEquipmentInfo";
            _command.Parameters.Add(new SqlParameter("@Id", equipmentInfoId));
            int result = await _command.ExecuteNonQueryAsync();
            _command.Parameters.Clear();

            return result > 0 ? true : false;
        }

        public async Task<bool> UpdateAsync(EquipmentInfoDomain NewEquipmentInfo)
        {
            var entity = Maper.ToModel(NewEquipmentInfo);
            _command.CommandType = System.Data.CommandType.StoredProcedure;
            _command.CommandText = "UpdateEquipmentInfo";
            _command.Parameters.Add(new SqlParameter("@Id", entity.Id));
            _command.Parameters.Add(new SqlParameter("@name", entity.Name));
            _command.Parameters.Add(new SqlParameter("@date", entity.Date));
            _command.Parameters.Add(new SqlParameter("@eNGINE", entity.ENGINE));
            _command.Parameters.Add(new SqlParameter("@bODY", entity.BODY));
            _command.Parameters.Add(new SqlParameter("@gRADE", entity.GRADE));
            _command.Parameters.Add(new SqlParameter("@aTM_MTM", entity.ATM_MTM));
            _command.Parameters.Add(new SqlParameter("@gEAR_SHIFT_TYPE", entity.GEAR_SHIFT_TYPE));
            _command.Parameters.Add(new SqlParameter("@cAB", entity.CAB));
            _command.Parameters.Add(new SqlParameter("@tRANSMISSION_MODEL", entity.TRANSMISSION_MODEL));
            _command.Parameters.Add(new SqlParameter("@lOADING_CAPACITY", entity.LOADING_CAPACITY));
            _command.Parameters.Add(new SqlParameter("@equipmentId", entity.EquipmentId));
            int result = await _command.ExecuteNonQueryAsync();
            _command.Parameters.Clear();

            return result > 0 ? true : false;
        }
    }
}
