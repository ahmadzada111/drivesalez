using AutoMapper;
using DriveSalez.Application.Contracts.ServiceContracts;
using DriveSalez.Domain.Entities;
using DriveSalez.Domain.RepositoryContracts;
using DriveSalez.SharedKernel.DTO.MarketVersionDTO;

namespace DriveSalez.Application.Services;

internal class MarketVersionService : IMarketVersionService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    
    public MarketVersionService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<MarketVersionDto> CreateMarketVersion(string version)
    {
        var result = _unitOfWork.MarketVersions.Add(new MarketVersion() { Version = version });
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<MarketVersionDto>(result);
    }

    public async Task<IEnumerable<MarketVersionDto>> GetAllMarketVersion()
    {
        var versions = await _unitOfWork.MarketVersions.GetAll();
        return _mapper.Map<IEnumerable<MarketVersionDto>>(versions);
    }

    public async Task<MarketVersionDto> FindMarketVersionById(int id)
    {
        var version = await _unitOfWork.MarketVersions.FindById(id);
        return _mapper.Map<MarketVersionDto>(version);
    }

    public async Task<MarketVersionDto> UpdateMarketVersion(MarketVersionDto versionDto)
    {
        var versionToUpdate = await _unitOfWork.MarketVersions.FindById(versionDto.Id);
        versionToUpdate.Version = versionDto.Version;
        _unitOfWork.MarketVersions.Update(versionToUpdate);
        await _unitOfWork.SaveChangesAsync();
        return _mapper.Map<MarketVersionDto>(versionToUpdate);
    }

    public async Task<bool> DeleteMarketVersion(int id)
    {
        var versionToDelete = await _unitOfWork.MarketVersions.FindById(id);
        _unitOfWork.MarketVersions.Delete(versionToDelete);
        await _unitOfWork.SaveChangesAsync();
        return true;
    }
}