using System.Data.Entity;

namespace Aio.Db.MSSqlEF
{
    public class AioDbContext : DbContext
    {
        public AioDbContext() : base("name=AioSqlDb_1st")
        {
        }
    }
}
