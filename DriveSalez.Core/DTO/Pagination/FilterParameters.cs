using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DriveSalez.Core.Enums;

namespace DriveSalez.Core.DTO.Pagination
{
    public class FilterParameters
    {
        public int? FromYearId { get; set; }
        
        public int? ToYearId { get; set; }
        
        public int? MakeId { get; set; }

        public int? ModelId { get; set; }

        public int? FuelTypeId { get; set; }

        public bool? IsBrandNew { get; set; }

        public int? BodyTypeId { get; set; }

        public int? ColorId { get; set; }
        
        public int? FromHorsePower { get; set; }
        
        public int? ToHorsePower { get; set; }
        
        public int? GearboxTypeId { get; set; }

        public int? DriveTrainTypeId { get; set; }

        public List<int>? ConditionsIds { get; set; }

        public int? MarketVersionId { get; set; }

        public int? SeatCount { get; set; }

        public List<int>? OptionsIds { get; set; }

        public int? EngineVolume { get; set; }

        public int? Mileage { get; set; }

        public DistanceUnit? DistanceUnit { get; set; }

        public bool? Barter { get; set; }

        public bool? OnCredit { get; set; }

        public decimal? FromPrice { get; set; }
       
        public decimal? ToPrice { get; set; }

        public Currency? Currency { get; set; }

        public int? CountryId { get; set; }

        public int? CityId { get; set; }
    }
}
