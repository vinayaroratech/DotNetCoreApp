using Expenses.Data.Access.Maps.Common;
using Expenses.Data.Model.EFClasses;
using Microsoft.EntityFrameworkCore;

namespace Expenses.Data.Access.Maps.Main
{
    public class ExpenseMap : IMap
    {
        public void Visit(ModelBuilder builder)
        {
            builder.Entity<Expense>()
                .HasKey(x => x.Id);
        }
    }
}