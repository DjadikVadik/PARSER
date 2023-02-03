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
    public class ImageController
    {
        IImageRepository _repository;

        public ImageController(SqlCommand command) => _repository = new ImageRepository(command);

        public async Task<bool> AddRangeAsync(IEnumerable<ImageDomain> list)
        {
            return await _repository.AddRangeAsync(list);
        }

        public async Task<bool> AddSingleAsync(ImageDomain imageDomain)
        {
            return await _repository.AddSingleAsync(imageDomain);
        }

        public async Task<IEnumerable<ImageDomain>?> GetAllAsync()
        {
            return await _repository.GetAllAsync(); 
        }

        public async Task<ImageDomain?> GetSingleAsync(int SubgroupId)
        {
            return await _repository.GetSingleAsync(SubgroupId);
        }

        public async Task<bool> DeleteAsync(int ImageId)
        {
            return await _repository.RemoveAsync(ImageId);
        }

        public async Task<bool> UpdateAsync(ImageDomain imageDomain)
        {
            return await _repository.UpdateAsync(imageDomain);
        }
    }
}
