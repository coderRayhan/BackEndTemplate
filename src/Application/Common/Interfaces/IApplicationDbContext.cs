
namespace Application.Common.Interfaces;
public interface IApplicationDbContext
{
    DbSet<CommonLookup> CommonLookups { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
