using AutoMapper;
using DriveSalez.Application.Contracts.ServiceContracts;
using DriveSalez.Domain.Enums;
using DriveSalez.Domain.Exceptions;
using DriveSalez.Domain.IdentityEntities;
using DriveSalez.Domain.RepositoryContracts;
using DriveSalez.SharedKernel.DTO;
using DriveSalez.SharedKernel.DTO.UserDTO;
using DriveSalez.SharedKernel.Utilities;
using Microsoft.AspNetCore.Identity;

namespace DriveSalez.Application.Services;

internal sealed class AdminService : IAdminService
{
    private readonly IAdminRepository _adminRepository;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    
    public AdminService(IAdminRepository adminRepository, UserManager<ApplicationUser> userManager, 
        IMapper mapper, IUnitOfWork unitOfWork)
    {
        _adminRepository = adminRepository;
        _userManager = userManager;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<RegisterModeratorResponseDto?> AddModeratorAsync(RegisterModeratorDto request)
    {
        var identityUser = new ApplicationUser()
        {
            Email = request.Email,
            UserName = request.Email,
            EmailConfirmed = true
        };

        var moderator = new Moderator()
        {
            IdentityId = identityUser.Id,
            FirstName = request.FirstName,
            LastName = request.LastName
        };
        
        IdentityResult result = await _userManager.CreateAsync(identityUser, request.Password);

        if (!result.Succeeded) return null;
        
        _unitOfWork.Users.Add(moderator);
        await _unitOfWork.SaveChangesAsync();
        await _userManager.AddToRoleAsync(identityUser, UserType.Moderator.ToString());

        return new RegisterModeratorResponseDto()
        {
            FirstName = moderator.FirstName,
            LastName = moderator.LastName,
            Email = identityUser.Email
        };
    }

    public async Task<PaginatedList<GetModeratorDto>> GetAllModeratorsAsync(PagingParameters pagingParameters)
    {
        var moderators = await _userManager.GetUsersInRoleAsync(UserType.Moderator.ToString());

        var result = _mapper.Map<List<GetModeratorDto>>(moderators);
        
        result = result
            .Skip((pagingParameters.PageIndex - 1) * pagingParameters.PageSize)
            .Take(pagingParameters.PageSize)
            .ToList();

        var totalCount = result.Count();
        
        return new PaginatedList<GetModeratorDto>(result, pagingParameters.PageIndex, pagingParameters.PageSize, totalCount);
    }

    public async Task<GetModeratorDto?> DeleteModeratorAsync(Guid moderatorId)
    {
        var response = await _adminRepository.DeleteModeratorFromDbAsync(moderatorId);
        return _mapper.Map<GetModeratorDto>(response);
    }

    public async Task<PaginatedList<GetUserDto>> GetAllUsers(PagingParameters pagingParameters)
    {
        var response = await _adminRepository.GetAllUsersFromDbAsync(pagingParameters);
        return _mapper.Map<PaginatedList<GetUserDto>>(response);
    }

    public async Task<bool> BanUserAsync(Guid userId, TimeSpan duration)
    {
        var identityUser = await _userManager.FindByIdAsync(userId.ToString()) 
                           ?? throw new UserNotFoundException("User not found");
        
        await _userManager.SetLockoutEndDateAsync(identityUser, DateTimeOffset.Now.Add(duration));
        return true;
    }

    public async Task<bool> UnbanUserAsync(Guid userId)
    {
        var identityUser = await _userManager.FindByIdAsync(userId.ToString()) 
                           ?? throw new UserNotFoundException("User not found");

        await _userManager.SetLockoutEndDateAsync(identityUser, DateTimeOffset.UtcNow);
        await _userManager.ResetAccessFailedCountAsync(identityUser);
        await _userManager.UpdateAsync(identityUser);
        return true;
    }
}