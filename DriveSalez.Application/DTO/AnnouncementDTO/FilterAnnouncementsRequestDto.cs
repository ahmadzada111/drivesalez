using DriveSalez.SharedKernel.Pagination;

namespace DriveSalez.Application.DTO.AnnouncementDTO;

public record FilterAnnouncementsRequestDto
{
    public PagingParameters PagingParameters { get; init; }
    
    public FilterParameters FilterParameters { get; init; }
}