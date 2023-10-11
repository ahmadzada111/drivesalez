using DriveSalez.Core.Entities;
using DriveSalez.Core.Enums;

namespace DriveSalez.Core.DTO
{
    public class AnnouncementDto
    {
        //Vehicle   --------------------------------------------------------------

        public int? Year { get; set; }   // 2009

        public int? MakeID { get; set; }

        public int? ModelID { get; set; }

        public int? FuelTypeID { get; set; }

        // List of IMAGE URLS NEED TO ADD 

        public int? GearboxID { get; set; }

        public int? DriveTrainTypeID { get; set; }

        public int? BodyTypeID { get; set; }

        public List<int>? ConditionsIDs { get; set; }
        
        public List<int>? OptionsIDs { get; set; }

        public int? ColorID { get; set; }

        public int? MarketVersionID { get; set; }

        public int HorsePower { get; set; }     // 150 hp

        public bool? IsBrandNew { get; set; }       //NEW Or USED

        public int? OwnerQuantity { get; set; }

        public int? SeatCount { get; set; }

        public string? VinCode { get; set; }

        public int MileAge { get; set; }    //149000 km

        public DistanceUnit MileageType { get; set; }

        public int? EngineVolume { get; set; }




        // -----------------------------------------------------------------------





        // Owner Details --------------------------------------------------------------------

        public Country Country { get; set; }

        public City City { get; set; }

        public Guid ApplicationUserID { get; set; }
        //-----------------------------------------------------------------------------------

        public bool? Barter { get; set; }

        public bool? OnCredit { get; set; }

        public string? Description { get; set; }

        public decimal Price { get; set; }  // 10900      

        public Currency Currency { get; set; }  // USD

    }
}
