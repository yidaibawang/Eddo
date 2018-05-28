using System.Data.Entity;

namespace Eddo.EntityFramework
{
    public interface IDbContextProvider<out TDbContext> where TDbContext : DbContext
    {
        TDbContext DbContext
        { get; }

    }
}
