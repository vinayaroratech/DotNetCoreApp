using Expenses.Data.Access.Maps.Common;
using Expenses.Data.Model.EFClasses;
using Microsoft.EntityFrameworkCore;

namespace Expenses.Data.Access.Maps.Main
{
    public class RoleMap : IMap
    {
        public void Visit(ModelBuilder builder)
        {
            builder.Entity<Role>()
                .HasKey(x => x.Id);
        }
    }
}