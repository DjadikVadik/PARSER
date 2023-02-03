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
    public class GroupRepository : IGroupRepository
    {
        SqlCommand _command;

        public GroupRepository(SqlCommand command) => _command = command;

        public async Task<bool> AddRangeAsync(IEnumerable<GroupDomain> list)
        {
            foreach (var entity in list)
            {
                if (await AddSingleAsync(entity) == false) return false;
            }

            return true;
        }

        public async Task<bool> AddSingleAsync(GroupDomain groupDomain)
        {
            var entity = Maper.ToModel(groupDomain);
            _command.CommandType = System.Data.CommandType.StoredProcedure;
            _command.CommandText = "AddGroups";
            _command.Parameters.Add(new SqlParameter("@name", entity.Name));
            int result = await _command.ExecuteNonQueryAsync();
            _command.Parameters.Clear();

            return result > 0 ? true : false;
        }

        public async Task<IEnumerable<GroupDomain>?> GetAllAsync()
        {
            _command.CommandType = System.Data.CommandType.StoredProcedure;
            _command.CommandText = "GetAllGroups";
            var reder = await _command.ExecuteReaderAsync();
            if (!reder.HasRows) return null;

            var list = new List<Group>();

            while (await reder.ReadAsync())
            {
                var entity = new Group();
                entity.Id = reder.GetInt32(0);
                entity.Name = reder.GetString(1);
                list.Add(entity);
            }

            reder.Close();

            return Maper.ToDomain(list);
        }

        public async Task<GroupDomain?> GetSingleAsync(int GroupId)
        {
            _command.CommandType = System.Data.CommandType.StoredProcedure;
            _command.CommandText = "GetSingleGroups";
            _command.Parameters.Add(new SqlParameter("@Id", GroupId));
            var reder = await _command.ExecuteReaderAsync();
            if (!reder.HasRows) return null;

            var entity = new Group();
            entity.Id = reder.GetInt32(0);
            entity.Name = reder.GetString(1);

            reder.Close();
            _command.Parameters.Clear();

            return Maper.ToDomain(entity);
        }

        public async Task<bool> RemoveAsync(int GroupId)
        {
            _command.CommandType = System.Data.CommandType.StoredProcedure;
            _command.CommandText = "DeleteGroups";
            _command.Parameters.Add(new SqlParameter("@Id", GroupId));
            int result = await _command.ExecuteNonQueryAsync();
            _command.Parameters.Clear();

            return result > 0 ? true : false;
        }

        public async Task<bool> UpdateAsync(GroupDomain NewGroup)
        {
            var entity = Maper.ToModel(NewGroup);
            _command.CommandType = System.Data.CommandType.StoredProcedure;
            _command.CommandText = "UpdateGroups";
            _command.Parameters.Add(new SqlParameter("@Id", entity.Id));
            _command.Parameters.Add(new SqlParameter("@name", entity.Name));
            int result = await _command.ExecuteNonQueryAsync();
            _command.Parameters.Clear();

            return result > 0 ? true : false;
        }
    }
}
