﻿using DriveSalez.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DriveSalez.Core.Enums;

namespace DriveSalez.Core.RepositoryContracts
{
    public interface IModeratorRepository
    {
        public Task<Announcement> ChangeAnnouncementStateInDb(Guid userId, int announcementId,
            AnnouncementState announcementState);
    }
}
