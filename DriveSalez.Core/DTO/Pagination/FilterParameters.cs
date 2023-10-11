using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DriveSalez.Core.DTO.Pagination
{
    public class FilterParameters
    {

        public int? FromYear { get; set; }
        public int? ToYear { get; set; }
        public int? MakeId { get; set; }

        public int? ModelId { get; set; }

        public int? FuelTypeId { get; set; }

        public bool? isBrandNew { get; set; }

        public int? BodyType { get; set; }

        public int? Color { get; set; }
        
        public int? FromHorsePower { get; set; }
        
        public int? ToHorsePower { get; set; }
        
        public int? GearboxTypeId { get; set; }

        public int? DriveTrainTypeId { get; set; }

        public List<int>? ConditionsIds { get; set; }

        public int? MarketVersion { get; set; }

        public int? SeatCount { get; set; }

        public List<int>? OptionsIds { get; set; }

        public int? EngineVolume { get; set; }

        public int? Mileage { get; set; }

        public int? DisctanceUnitId { get; set; }

        public bool? Barter { get; set; }

        public bool? OnCredit { get; set; }

        public decimal? FromPrice { get; set; }
        public decimal? ToPrice { get; set; }

        public int? CurrencyId { get; set; }

        public int? CountryId { get; set; }

        public int? CityId { get; set; }

    }
}
