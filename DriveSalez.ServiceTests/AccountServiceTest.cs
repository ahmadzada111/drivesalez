using AutoFixture;
using DriveSalez.Core.Domain.IdentityEntities;
using DriveSalez.Core.Domain.RepositoryContracts;
using DriveSalez.Core.DTO;
using DriveSalez.Core.DTO.Enums;
using DriveSalez.Core.ServiceContracts;
using DriveSalez.Core.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;

namespace DriveSalez.ServiceTests;

public class AccountServiceTest
{
    private readonly Mock<UserManager<ApplicationUser>> _userManagerMock;
    private readonly Mock<SignInManager<ApplicationUser>> _signInManagerMock;
    private readonly Mock<RoleManager<ApplicationRole>> _roleManagerMock;
    private readonly Mock<IJwtService> _jwtServiceMock;
    private readonly Mock<IHttpContextAccessor> _httpContextAccessorMock;
    private readonly Mock<IAccountRepository> _accountRepositoryMock;
    private readonly Mock<IFileService> _fileServiceMock;
    private readonly AccountService _accountService;
    private readonly IFixture _fixture;
    
    public AccountServiceTest()
    {
        _jwtServiceMock = new Mock<IJwtService>();
        _httpContextAccessorMock = new Mock<IHttpContextAccessor>();
        _accountRepositoryMock = new Mock<IAccountRepository>();
        _fileServiceMock = new Mock<IFileService>();
        
        _userManagerMock = new Mock<UserManager<ApplicationUser>>(
            new Mock<IUserStore<ApplicationUser>>().Object,
            new Mock<IOptions<IdentityOptions>>().Object,
            new Mock<IPasswordHasher<ApplicationUser>>().Object,
            new IUserValidator<ApplicationUser>[0],
            new IPasswordValidator<ApplicationUser>[0],
            new Mock<ILookupNormalizer>().Object,
            new Mock<IdentityErrorDescriber>().Object,
            new Mock<IServiceProvider>().Object,
            new Mock<ILogger<UserManager<ApplicationUser>>>().Object);
        
        _signInManagerMock = new Mock<SignInManager<ApplicationUser>>(
            _userManagerMock.Object,
            _httpContextAccessorMock.Object,
            new Mock<IUserClaimsPrincipalFactory<ApplicationUser>>().Object,
            new Mock<IOptions<IdentityOptions>>().Object,
            new Mock<ILogger<SignInManager<ApplicationUser>>>().Object,
            new Mock<IAuthenticationSchemeProvider>().Object,
            new Mock<IUserConfirmation<ApplicationUser>>().Object);
        
        _roleManagerMock = new Mock<RoleManager<ApplicationRole>>(
            new Mock<IRoleStore<ApplicationRole>>().Object,
            new IRoleValidator<ApplicationRole>[0],
            new Mock<ILookupNormalizer>().Object,
            new Mock<IdentityErrorDescriber>().Object,
            new Mock<ILogger<RoleManager<ApplicationRole>>>().Object);
        
        _fixture = new Fixture();
        
        _accountService = new AccountService(
            _userManagerMock.Object,
            _signInManagerMock.Object,
            _roleManagerMock.Object,
            _jwtServiceMock.Object,
            _httpContextAccessorMock.Object,
            _accountRepositoryMock.Object,
            _fileServiceMock.Object
        );
    }
    
    [Fact]
    public async Task RegisterDefaultAccountAsync_SuccessfulRegistration_ReturnsIdentityResult()
    {
        // Arrange
        _userManagerMock.Setup(temp => temp.CreateAsync(It.IsAny<DefaultAccount>(), It.IsAny<string>()))
            .ReturnsAsync(IdentityResult.Success);

        _roleManagerMock.Setup(temp => temp.FindByNameAsync(It.IsAny<string>()))
            .ReturnsAsync((string roleName) =>
            {
                var role = new ApplicationRole()
                {
                    Name = roleName
                };

                return role;
            });

        _userManagerMock.Setup(temp => temp.UpdateAsync(It.IsAny<DefaultAccount>()))
            .ReturnsAsync(IdentityResult.Success);

        _accountRepositoryMock
            .Setup(temp => temp.AddLimitsToAccountInDbAsync(It.IsAny<DefaultAccount>(), UserType.DefaultAccount))
            .Returns(Task.CompletedTask);
            
        var request = _fixture.Build<RegisterDefaultAccountDto>()
            .With(x => x.FirstName, "John")
            .With(x => x.LastName, "Doe")
            .With(x => x.Email, "johndoe@mail.com")
            .With(x => x.Password, "P@ss1234")
            .Create();
        
        // Act
        var result = await _accountService.RegisterDefaultAccountAsync(request);

        // Assert
        result.Succeeded.Should().Be(true);
        
        _userManagerMock.Verify(temp => temp.CreateAsync(It.IsAny<DefaultAccount>(), It.IsAny<string>()), Times.Once);
        _userManagerMock.Verify(temp => temp.AddToRoleAsync(It.IsAny<DefaultAccount>(), UserType.DefaultAccount.ToString()), Times.Once);
        _userManagerMock.Verify(temp => temp.UpdateAsync(It.IsAny<DefaultAccount>()), Times.Once);
        _accountRepositoryMock.Verify(temp => temp.AddLimitsToAccountInDbAsync(It.IsAny<DefaultAccount>(), UserType.DefaultAccount), Times.Once);
    }
    
    [Fact]
    public async Task RegisterDefaultAccountAsync_UnsuccessfulRegistration_ReturnsIdentityResult()
    {
        // Arrange
        _userManagerMock.Setup(temp => temp.CreateAsync(It.IsAny<DefaultAccount>(), It.IsAny<string>()))
            .ReturnsAsync(IdentityResult.Success);

        _roleManagerMock.Setup(temp => temp.FindByNameAsync(It.IsAny<string>()))
            .ReturnsAsync((string roleName) =>
            {
                var role = new ApplicationRole()
                {
                    Name = roleName
                };

                return role;
            });

        _userManagerMock.Setup(temp => temp.UpdateAsync(It.IsAny<DefaultAccount>()))
            .ReturnsAsync(IdentityResult.Success);

        _accountRepositoryMock
            .Setup(temp => temp.AddLimitsToAccountInDbAsync(It.IsAny<DefaultAccount>(), UserType.DefaultAccount))
            .Returns(Task.CompletedTask);
            
        var request = _fixture.Build<RegisterDefaultAccountDto>()
            .With(x => x.FirstName, "John")
            .With(x => x.LastName, "Doe")
            .With(x => x.Email, "johndoe@mail.com")
            .With(x => x.Password, "Pass1234")
            .Create();
        
        // Act
        var result = await _accountService.RegisterDefaultAccountAsync(request);

        // Assert
        result.Succeeded.Should().Be(false);
        
        _userManagerMock.Verify(temp => temp.CreateAsync(It.IsAny<DefaultAccount>(), It.IsAny<string>()), Times.Once);
        _userManagerMock.Verify(temp => temp.AddToRoleAsync(It.IsAny<DefaultAccount>(), UserType.DefaultAccount.ToString()), Times.Once);
        _userManagerMock.Verify(temp => temp.UpdateAsync(It.IsAny<DefaultAccount>()), Times.Once);
        _accountRepositoryMock.Verify(temp => temp.AddLimitsToAccountInDbAsync(It.IsAny<DefaultAccount>(), UserType.DefaultAccount), Times.Once);
    }
}