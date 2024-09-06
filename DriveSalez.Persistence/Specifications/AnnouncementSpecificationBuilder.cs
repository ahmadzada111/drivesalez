using DriveSalez.Application.Specifications;
using DriveSalez.Domain.Entities;
using DriveSalez.Persistence.Abstractions;
using DriveSalez.SharedKernel.DTO.AnnouncementDTO;

namespace DriveSalez.Persistence.Specifications;

internal sealed class AnnouncementSpecificationBuilder
{
    public static List<ISpecification<Announcement>> BuildSpecifications(FilterAnnouncementParameters filterAnnouncementParameters)
    {
        var specs = new List<ISpecification<Announcement>>();

        AddSpecificationIfNotNull(specs, filterAnnouncementParameters.IsBrandNew, value => new AnnouncementByIsBrandNewSpecification(value));
        AddSpecificationIfNotNull(specs, filterAnnouncementParameters.FromYearId, filterAnnouncementParameters.ToYearId, (from, to) => new AnnouncementByYearRangeSpecification(from, to));
        AddSpecificationIfNotNull(specs, filterAnnouncementParameters.MakeId, value => new AnnouncementByMakeSpecification(value));
        AddSpecificationIfNotEmpty(specs, filterAnnouncementParameters.ModelsIds, ids => new AnnouncementByModelsSpecification(ids));
        AddSpecificationIfNotEmpty(specs, filterAnnouncementParameters.FuelTypesIds, ids => new AnnouncementByFuelTypesSpecification(ids));
        AddSpecificationIfNotEmpty(specs, filterAnnouncementParameters.BodyTypesIds, ids => new AnnouncementByBodyTypesSpecification(ids));
        AddSpecificationIfNotEmpty(specs, filterAnnouncementParameters.ColorsIds, ids => new AnnouncementByColorsSpecification(ids));
        AddSpecificationIfNotNull(specs, filterAnnouncementParameters.FromHorsePower, filterAnnouncementParameters.ToHorsePower, (from, to) => new AnnouncementByHorsePowerRangeSpecification(from, to));
        AddSpecificationIfNotEmpty(specs, filterAnnouncementParameters.GearboxTypesIds, ids => new AnnouncementByGearboxesSpecification(ids));
        AddSpecificationIfNotEmpty(specs, filterAnnouncementParameters.DriveTrainTypesIds, ids => new AnnouncementByDrivetrainsSpecification(ids));
        AddSpecificationIfNotEmpty(specs, filterAnnouncementParameters.ConditionsIds, ids => new AnnouncementByConditionsSpecification(ids));
        AddSpecificationIfNotEmpty(specs, filterAnnouncementParameters.MarketVersionsIds, ids => new AnnouncementByMarketVersionsSpecification(ids));
        AddSpecificationIfNotNull(specs, filterAnnouncementParameters.SeatCount, value => new AnnouncementBySeatCountSpecification(value));
        AddSpecificationIfNotEmpty(specs, filterAnnouncementParameters.OptionsIds, ids => new AnnouncementByOptionsSpecification(ids));
        AddSpecificationIfNotNull(specs, filterAnnouncementParameters.FromEngineVolume, filterAnnouncementParameters.ToEngineVolume, (from, to) => new AnnouncementByEngineVolumeRangeSpecification(from, to));
        AddSpecificationIfNotNull(specs, filterAnnouncementParameters.FromMileage, filterAnnouncementParameters.ToMileage, (from, to) => new AnnouncementByMileageRangeSpecification(from, to));
        AddSpecificationIfNotEmpty(specs, filterAnnouncementParameters.MileageType, type => new AnnouncementByMileageTypeSpecification(type));
        AddSpecificationIfNotNull(specs, filterAnnouncementParameters.Barter, value => new AnnouncementByBarterSpecification(value));
        AddSpecificationIfNotNull(specs, filterAnnouncementParameters.OnCredit, value => new AnnouncementByOnCreditSpecification(value));
        AddSpecificationIfNotNull(specs, filterAnnouncementParameters.FromPrice, filterAnnouncementParameters.ToPrice, (from, to) => new AnnouncementByPriceRangeSpecification(from, to));
        AddSpecificationIfNotNull(specs, filterAnnouncementParameters.CountryId, value => new AnnouncementByCountrySpecification(value));
        AddSpecificationIfNotEmpty(specs, filterAnnouncementParameters.CitiesIds, ids => new AnnouncementByCitiesSpecification(ids));

        return specs;
    }

    private static void AddSpecificationIfNotNull<T>(List<ISpecification<Announcement>> specs, T? value, Func<T, ISpecification<Announcement>> createSpecification) where T : struct
    {
        if (value.HasValue)
        {
            specs.Add(createSpecification(value.Value));
        }
    }

    private static void AddSpecificationIfNotNull<T>(List<ISpecification<Announcement>> specs, T? value1, T? value2, Func<T?, T?, ISpecification<Announcement>> createSpecification) where T : struct
    {
        if (value1.HasValue || value2.HasValue)
        {
            specs.Add(createSpecification(value1, value2));
        }
    }

    private static void AddSpecificationIfNotEmpty<T>(List<ISpecification<Announcement>> specs, List<T>? values, Func<List<T>, ISpecification<Announcement>> createSpecification)
    {
        if (values != null && values.Any())
        {
            specs.Add(createSpecification(values));
        }
    }

    private static void AddSpecificationIfNotEmpty(List<ISpecification<Announcement>> specs, string? value, Func<string, ISpecification<Announcement>> createSpecification)
    {
        if (!string.IsNullOrEmpty(value))
        {
            specs.Add(createSpecification(value));
        }
    }
}
