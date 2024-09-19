using DriveSalez.Domain.IdentityEntities;
using DriveSalez.SharedKernel.DTO;

namespace DriveSalez.Application.Contracts.ServiceContracts;

public interface IWorkHourService
{
    void AddWorkHoursToUser(User user, ICollection<WorkHourDto> workHours);
}