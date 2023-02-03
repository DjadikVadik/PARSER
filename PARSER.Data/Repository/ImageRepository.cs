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
    public class ImageRepository : IImageRepository
    {
        SqlCommand _command;

        public ImageRepository(SqlCommand command) => _command = command;

        public async Task<bool> AddRangeAsync(IEnumerable<ImageDomain> list)
        {
            foreach (var entity in list)
            {
                if (await AddSingleAsync(entity) == false) return false;
            }

            return true;
        }

        public async Task<bool> AddSingleAsync(ImageDomain imageDomain)
        {
            var entity = Maper.ToModel(imageDomain);
            _command.CommandType = System.Data.CommandType.StoredProcedure;
            _command.CommandText = "AddImage";
            _command.Parameters.Add(new SqlParameter("@name", entity.Name));
            _command.Parameters.Add(new SqlParameter("@subgroupId", entity.SubgroupId));
            int result = await _command.ExecuteNonQueryAsync();
            _command.Parameters.Clear();

            return result > 0 ? true : false;
        }

        public async Task<IEnumerable<ImageDomain>?> GetAllAsync()
        {
            _command.CommandType = System.Data.CommandType.StoredProcedure;
            _command.CommandText = "GetAllImage";
            var reder = await _command.ExecuteReaderAsync();
            if (!reder.HasRows) return null;

            var list = new List<Image>();

            while (await reder.ReadAsync())
            {
                var entity = new Image();
                entity.Id = reder.GetInt32(0);
                entity.Name = reder.GetString(1);
                entity.SubgroupId = reder.GetInt32(2);
                list.Add(entity);
            }

            reder.Close();
            return Maper.ToDomain(list);
        }

        public async Task<ImageDomain?> GetSingleAsync(int SubgroupId)
        {
            _command.CommandType = System.Data.CommandType.StoredProcedure;
            _command.CommandText = "GetImageBySubgroupId";
            _command.Parameters.Add(new SqlParameter("@Id", SubgroupId));
            var reder = await _command.ExecuteReaderAsync();
            if (!reder.HasRows) return null;

            var entity = new Image();
            entity.Id = reder.GetInt32(0);
            entity.Name = reder.GetString(1);
            entity.SubgroupId = reder.GetInt32(2);

            reder.Close();
            _command.Parameters.Clear();

            return Maper.ToDomain(entity);
        }

        public async Task<bool> RemoveAsync(int ImageId)
        {
            _command.CommandType = System.Data.CommandType.StoredProcedure;
            _command.CommandText = "DeleteImage";
            _command.Parameters.Add(new SqlParameter("@Id", ImageId));
            int result = await _command.ExecuteNonQueryAsync();
            _command.Parameters.Clear();

            return result > 0 ? true : false;
        }

        public async Task<bool> UpdateAsync(ImageDomain imageDomain)
        {
            var entity = Maper.ToModel(imageDomain);
            _command.CommandType = System.Data.CommandType.StoredProcedure;
            _command.CommandText = "UpdateImage";
            _command.Parameters.Add(new SqlParameter("@Id", entity.Id));
            _command.Parameters.Add(new SqlParameter("@name", entity.Name));
            _command.Parameters.Add(new SqlParameter("@subgroupId", entity.SubgroupId));
            int result = await _command.ExecuteNonQueryAsync();
            _command.Parameters.Clear();

            return result > 0 ? true : false;
        }
    }
}
