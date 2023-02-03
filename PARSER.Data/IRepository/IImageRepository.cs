using PARSER.Domain.ModelsDomain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PARSER.Data.IRepository
{
    public interface IImageRepository
    {
        Task<bool> AddSingleAsync(ImageDomain imageDomain);
        Task<bool> AddRangeAsync(IEnumerable<ImageDomain> list);
        Task<IEnumerable<ImageDomain>?> GetAllAsync();
        Task<ImageDomain?> GetSingleAsync(int SubgroupId);
        Task<bool> RemoveAsync(int ImageId);
        Task<bool> UpdateAsync(ImageDomain imageDomain);
    }
}
