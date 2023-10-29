using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DriveSalez.Core.DTO;
using DriveSalez.Core.Entities;
using DriveSalez.Core.Enums;

namespace DriveSalez.Core.ServiceContracts
{
    public interface IModeratorService
    {
        public Task<AnnouncementResponseDto> ChangeAnnouncementStateAsync(int announcementId, AnnouncementState announcementState);
    }
}
