using CoolPhotosAPI.Data.Entities;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace CoolPhotosAPI.Data.Repositories
{
    public class ExtendedPhotoRepository : CoolRepository<Photo>
    {
        public ExtendedPhotoRepository(DbContext dbContext): base(dbContext) { }

        public async Task<ICollection<Photo>> GetAllUserPhotosAsync(string userSocnetworkId)
        {
            return await _context.Set<Photo>()
                .FromSql(new RawSqlString(string.Format("EXEC SP_GetAllPhotos '{0}'", userSocnetworkId)))
                .ToListAsync();
        }
    }
}
