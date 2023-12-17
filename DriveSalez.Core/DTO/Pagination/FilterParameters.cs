﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DriveSalez.Core.Entities;
using DriveSalez.Core.Enums;

namespace DriveSalez.Core.DTO.Pagination
{
    public class FilterParameters
    {
        public int? FromYearId { get; set; }
        
        public int? ToYearId { get; set; }
        
        public int? MakeId { get; set; }

        public List<int>? ModelsIds { get; set; }

        public List<int>? FuelTypesIds { get; set; }

        public bool? IsBrandNew { get; set; }

        public List<int>? BodyTypesIds { get; set; }

        public List<int>? ColorsIds { get; set; }
        
        public int? FromHorsePower { get; set; }
        
        public int? ToHorsePower { get; set; }
        
        public List<int>? GearboxTypesIds { get; set; }

        public List<int>? DriveTrainTypesIds { get; set; }

        public List<int>? ConditionsIds { get; set; }

        public List<int>? MarketVersionsIds { get; set; }

        public int? SeatCount { get; set; }

        public List<int>? OptionsIds { get; set; }

        public int? FromEngineVolume { get; set; }

        public int? ToEngineVolume { get; set; }
        
        public int? FromMileage { get; set; }

        public int? ToMileage { get; set; }
        
        public DistanceUnit? MileageType { get; set; }

        public bool? Barter { get; set; }

        public bool? OnCredit { get; set; }

        public decimal? FromPrice { get; set; }
       
        public decimal? ToPrice { get; set; }

        public int? CurrencyId { get; set; }

        public int? CountryId { get; set; }

        public List<int>? CitiesIds { get; set; }
    }
}
