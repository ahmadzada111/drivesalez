﻿using System.Text.Json.Serialization;
using DriveSalez.Core.Domain.Entities.VehicleDetailsFiles;

namespace DriveSalez.Core.Domain.Entities.VehicleParts
{
    public class VehicleDrivetrainType
    {
        public int Id { get; set; }

        public string DrivetrainType { get; set; }

        [JsonIgnore]
        public List<VehicleDetails> VehicleDetails { get; set; }        //EF CORE FOREIGN KEY
    }
}
