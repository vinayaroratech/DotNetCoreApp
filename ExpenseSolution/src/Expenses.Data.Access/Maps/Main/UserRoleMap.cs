using Expenses.Data.Access.Maps.Common;
using Expenses.Data.Model.EFClasses;
using Microsoft.EntityFrameworkCore;

namespace Expenses.Data.Access.Maps.Main
{
    public class UserRoleMap : IMap
    {
        public void Visit(ModelBuilder builder)
        {
            builder.Entity<UserRole>()
                .HasKey(x => x.Id);
        }
    }
}