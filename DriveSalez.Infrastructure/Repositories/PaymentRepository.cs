using DriveSalez.Core.Entities;
using DriveSalez.Core.ServiceContracts;
using DriveSalez.Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace DriveSalez.Infrastructure.Repositories;

public class PaymentRepository : IPaymentRepository
{
    private readonly ApplicationDbContext _dbContext;
    
    public async Task<bool> RecordPaymentInDbAsync(Guid userId)
    {
        var user = await _dbContext.Users.
            Where(x => x.Id == userId).
            FirstOrDefaultAsync();

        return true;
        // if(use)
    }
}