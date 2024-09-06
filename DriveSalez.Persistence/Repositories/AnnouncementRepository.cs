using DriveSalez.Domain.Entities;
using DriveSalez.Domain.Enums;
using DriveSalez.Domain.RepositoryContracts;
using DriveSalez.Persistence.DbContext;
using DriveSalez.Persistence.Abstractions;
using DriveSalez.Persistence.Specifications;
using DriveSalez.SharedKernel.DTO.AnnouncementDTO;
using DriveSalez.SharedKernel.Utilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DriveSalez.Persistence.Repositories;

internal sealed class AnnouncementRepository : GenericRepository<Announcement>, IAnnouncementRepository
{
    private readonly ILogger _logger;
    private readonly ApplicationDbContext _dbContext;
    
    public AnnouncementRepository(ApplicationDbContext dbContext, ILogger<AnnouncementRepository> logger) : base(dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;
    }
    
    public async Task<PaginatedList<Announcement>> GetFilteredAnnouncementsFromDbAsync(
        FilterAnnouncementParameters filterParameters, PagingParameters pagingParameters)
    {
        var query = _dbContext.Announcements
            .AsNoTracking()
            .Where(x => x.AnnouncementState == AnnouncementState.Active);
        
        var specs = AnnouncementSpecificationBuilder.BuildSpecifications(filterParameters);
        var filter = new Filter<Announcement>();
            
        foreach (var spec in specs)
        {
            query = filter.ApplyFilter(query, spec);
        }
            
        var totalCount = await query.CountAsync();
        var announcements = await query
            .OrderBy(o => o.IsPremium)
            .Skip((pagingParameters.PageIndex - 1) * pagingParameters.PageSize)
            .Take(pagingParameters.PageSize)
            .ToListAsync();
            
        return new PaginatedList<Announcement>(announcements, totalCount, pagingParameters.PageIndex, pagingParameters.PageSize);
    }

    public async Task<Tuple<IEnumerable<Announcement>, PaginatedList<Announcement>>> GetAllActiveAnnouncementsFromDbAsync(PagingParameters pagingParameters)
    {
        var allPremiumAnnouncements = _dbContext.Announcements
            .AsNoTracking()
            .Where(x => x.AnnouncementState == AnnouncementState.Active);
        
        var random = new Random();
        var premiumAnnouncements = allPremiumAnnouncements.OrderBy(_ => random.Next()).Take(8).ToList();
        
        var nonPremiumQuery = _dbContext.Announcements
            .AsNoTracking()
            .Where(x => x.AnnouncementState == AnnouncementState.Inactive);
        var totalNonPremiumCount = await nonPremiumQuery.CountAsync();
        var nonPremiumAnnouncements = await nonPremiumQuery
            .Skip((pagingParameters.PageIndex - 1) * pagingParameters.PageSize)
            .Take(pagingParameters.PageSize)
            .ToListAsync();

        var paginatedNonPremiumAnnouncements = PaginatedList<Announcement>.ToPaginatedList(nonPremiumAnnouncements, pagingParameters.PageIndex, pagingParameters.PageSize, totalNonPremiumCount);

        return Tuple.Create<IEnumerable<Announcement>, PaginatedList<Announcement>>(premiumAnnouncements, paginatedNonPremiumAnnouncements);
    }
}