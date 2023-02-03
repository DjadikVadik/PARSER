using Microsoft.Data.SqlClient;
using PARSER.Data.IRepository;
using PARSER.Data.Models;
using PARSER.Domain.ModelsDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PARSER.Data.Repository
{
    public class SubgroupRepository : ISubgroupRepository
    {
        SqlCommand _command;

        public SubgroupRepository(SqlCommand command) => _command = command;

        public async Task<bool> AddRangeAsync(IEnumerable<SubgroupDomain> list)
        {
            foreach (var entity in list)
            {
                if (await AddSingleAsync(entity) == false) return false;
            }

            return true;
        }

        public async Task<bool> AddSingleAsync(SubgroupDomain subGroupDomain)
        {
            var entity = Maper.ToModel(subGroupDomain);
            _command.CommandType = System.Data.CommandType.StoredProcedure;
            _command.CommandText = "AddSubgroup";
            _command.Parameters.Add(new SqlParameter("@name", entity.Name));
            _command.Parameters.Add(new SqlParameter("@groupsId", entity.GroupId));
            int result = await _command.ExecuteNonQueryAsync();
            _command.Parameters.Clear();

            return result > 0 ? true : false;
        }

        public async Task<IEnumerable<SubgroupDomain>?> GetAllAsync(int GroupId)
        {
            _command.CommandType = System.Data.CommandType.StoredProcedure;
            _command.CommandText = "GetSubgroupByGroupsId";
            _command.Parameters.Add(new SqlParameter("@Id", GroupId));
            var reder = await _command.ExecuteReaderAsync();
            if (!reder.HasRows) return null;

            var list = new List<Subgroup>();

            while (await reder.ReadAsync())
            {
                var entity = new Subgroup();
                entity.Id = reder.GetInt32(0);
                entity.Name = reder.GetString(1);
                entity.GroupId = reder.GetInt32(2);
                list.Add(entity);
            }

            reder.Close();
            _command.Parameters.Clear();

            return Maper.ToDomain(list);
        }

        public async Task<SubgroupDomain?> GetSingleAsync(int SubgroupId)
        {
            _command.CommandType = System.Data.CommandType.StoredProcedure;
            _command.CommandText = "GetSingleSubgroup";
            _command.Parameters.Add(new SqlParameter("@Id", SubgroupId));
            var reder = await _command.ExecuteReaderAsync();
            if (!reder.HasRows) return null;

            var entity = new Subgroup();
            entity.Id = reder.GetInt32(0);
            entity.Name = reder.GetString(1);
            entity.GroupId = reder.GetInt32(2);

            reder.Close();
            _command.Parameters.Clear();

            return Maper.ToDomain(entity);
        }

        public async Task<bool> RemoveAsync(int SubgroupId)
        {
            _command.CommandType = System.Data.CommandType.StoredProcedure;
            _command.CommandText = "DeleteSubgroup";
            _command.Parameters.Add(new SqlParameter("@Id", SubgroupId));
            int result = await _command.ExecuteNonQueryAsync();
            _command.Parameters.Clear();

            return result > 0 ? true : false;
        }

        public async Task<bool> UpdateAsync(SubgroupDomain NewSubgroup)
        {
            var entity = Maper.ToModel(NewSubgroup);
            _command.CommandType = System.Data.CommandType.StoredProcedure;
            _command.CommandText = "UpdateSubgroup";
            _command.Parameters.Add(new SqlParameter("@Id", entity.Id));
            _command.Parameters.Add(new SqlParameter("@name", entity.Name));
            _command.Parameters.Add(new SqlParameter("@groupsId", entity.GroupId));
            int result = await _command.ExecuteNonQueryAsync();
            _command.Parameters.Clear();

            return result > 0 ? true : false;
        }
    }
}
