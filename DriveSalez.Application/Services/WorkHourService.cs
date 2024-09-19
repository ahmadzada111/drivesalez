using DriveSalez.Application.Contracts.ServiceContracts;
using DriveSalez.Domain.Entities;
using DriveSalez.Domain.IdentityEntities;
using DriveSalez.Domain.RepositoryContracts;
using DriveSalez.SharedKernel.DTO;

namespace DriveSalez.Application.Services;

public class WorkHourService : IWorkHourService
{
    private readonly IUnitOfWork _unitOfWork;

    public WorkHourService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public void AddWorkHoursToUser(User user, ICollection<WorkHourDto> workHours)
    {
        if (workHours.Count == 0) return;
        
        foreach (var value in workHours)
        {
            var workHour = new WorkHour
            {
                CloseTime = value.CloseTime,
                OpenTime = value.OpenTime,
                User = user,
                DayOfWeek = Enum.Parse<DayOfWeek>(value.DayOfWeek)
            };

            _unitOfWork.WorkHours.Add(workHour);
        }
    }
}