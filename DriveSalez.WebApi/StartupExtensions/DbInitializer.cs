using DriveSalez.Core.Domain.IdentityEntities;
using DriveSalez.Core.DTO.Enums;
using DriveSalez.Infrastructure.DbContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace DriveSalez.WebApi.StartupExtensions;

public static class DbInitializer
{
    public static async Task<WebApplication> SeedData(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        await using var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        
        await context.Database.EnsureCreatedAsync();

        var roles = await context.Roles.ToListAsync();

        if (roles.IsNullOrEmpty())
        {
            await context.Roles.AddRangeAsync(new ApplicationRole
                {
                    Id = Guid.Parse("8c36e548-f0cc-4ef5-b3b0-33b67b92d8ce"),
                    Name = UserType.DefaultAccount.ToString(),
                },
                new ApplicationRole
                {
                    Id = Guid.Parse("20da09a9-5f13-4df0-a44d-8f5b9a137332"),
                    Name = UserType.BusinessAccount.ToString(),
                },
                new ApplicationRole
                {
                    Id = Guid.Parse("4d329e8c-8953-4116-bc85-17c86e7c1602"),
                    Name = UserType.PremiumAccount.ToString(),
                },
                new ApplicationRole
                {
                    Id = Guid.Parse("6faa4cd6-6cb6-4ec8-a3c4-8e7e4a03e373"),
                    Name = UserType.Admin.ToString(),
                },
                new ApplicationRole
                {
                    Id = Guid.Parse("7e1727bb-c257-45b4-a723-4f759bcb0866"),
                    Name = UserType.Moderator.ToString(),
                });

            await context.Users.AddAsync(new DefaultAccount()
            {
                Id = Guid.Parse("e0a73daa-1c33-4567-99ab-fd87f12b5176"),
                Email = "admin",
                UserName = "admin",
                FirstName = "admin",
                LastName = "admin"
            });

            await context.UserRoles.AddAsync(new IdentityUserRole<Guid>()
            {
                UserId = Guid.Parse("e0a73daa-1c33-4567-99ab-fd87f12b5176"),
                RoleId = Guid.Parse("6faa4cd6-6cb6-4ec8-a3c4-8e7e4a03e373")
            });

            await context.SaveChangesAsync();
        }
        
        return app;
    }
}