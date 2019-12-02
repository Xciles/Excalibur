using Microsoft.EntityFrameworkCore;

namespace Excalibur.AspNetCore.Business
{
    public abstract class BaseBusiness
    {
        protected DbContext DbContext { get; set; }

        protected BaseBusiness(DbContext dbContext)
        {
            DbContext = dbContext;
        }
    }
}
