using System.Linq.Expressions;
using DriveSalez.Domain.Entities;
using DriveSalez.Persistence.Abstractions;

namespace DriveSalez.Application.Specifications;

public class AnnouncementByOnCreditSpecification : ISpecification<Announcement>
{
    private readonly bool? _onCredit;

    public AnnouncementByOnCreditSpecification(bool? onCredit)
    {
        _onCredit = onCredit;
    }

    public Expression<Func<Announcement, bool>> ToExpression()
    {
        return a => _onCredit.HasValue || a.OnCredit == _onCredit;
    }
}