using DriveSalez.Domain.Entities;
using DriveSalez.Domain.RepositoryContracts;
using DriveSalez.Persistence.DbContext;

namespace DriveSalez.Persistence.Repositories;

internal class PhoneNumberRepository : GenericRepository<PhoneNumber>, IPhoneNumberRepository
{
    public PhoneNumberRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
        
    }
}