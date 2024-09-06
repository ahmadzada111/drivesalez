using DriveSalez.Domain.Entities;
using DriveSalez.Domain.RepositoryContracts;
using DriveSalez.Persistence.DbContext;

namespace DriveSalez.Persistence.Repositories;

internal class ImageUrlRepository : GenericRepository<ImageUrl>, IImageUrlRepository
{
    public ImageUrlRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        
    }
}