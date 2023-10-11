using DriveSalez.Core.DTO;
using DriveSalez.Core.IdentityEntities;
using DriveSalez.Core.RepositoryContracts;
using DriveSalez.Core.ServiceContracts;

namespace DriveSalez.Core.Services;

public class AccountService : IAccountService
{
    private readonly IAccountRepository _accountRepository;
    
    public AccountService(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    public async Task<AuthenticationResponseDto> Register(RegisterDto request)
    {
        ApplicationUser user = new ApplicationUser()
        {
            Email = request.Email,
            PhoneNumber = request.Phone,
            UserName = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            EmailConfirmed = false
        };

        var response = await _accountRepository.SendRegistrationDataToDb(user, request.Password);
        return response;
    }

    public async Task<AuthenticationResponseDto> Login(LoginDto request)
    {
        var response = await _accountRepository.SendLoginDataToDb(request);
        return response;
    }

    public async Task<AuthenticationResponseDto> Refresh(RefreshJwtDto request)
    {
        var response = await _accountRepository.SendRefreshTokenDataToDb(request);
        return response;
    }
}