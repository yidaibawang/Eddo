using Eddo.Domain.UnitOfWorks;

namespace Eddo.EntityFramework
{
    public class DbContextTypeMatcher : DbContextTypeMatcher<EddoDbContext>
    {
        public DbContextTypeMatcher(IUnitOfWorkProvider currentUnitOfWorkProvider)
            : base(currentUnitOfWorkProvider)
        {
        }
    }
}
