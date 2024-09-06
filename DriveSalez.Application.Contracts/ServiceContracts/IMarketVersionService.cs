using DriveSalez.SharedKernel.DTO;
using DriveSalez.SharedKernel.DTO.MarketVersionDTO;

namespace DriveSalez.Application.Contracts.ServiceContracts;

public interface IMarketVersionService
{
    Task<MarketVersionDto> CreateMarketVersion(string version);

    Task<IEnumerable<MarketVersionDto>> GetAllMarketVersion();

    Task<MarketVersionDto> FindMarketVersionById(int id);

    Task<MarketVersionDto> UpdateMarketVersion(MarketVersionDto versionDto);
    
    Task<bool> DeleteMarketVersion(int id);
}