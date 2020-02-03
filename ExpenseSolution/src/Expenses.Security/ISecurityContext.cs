using Expenses.Data.Model;
using Expenses.Data.Model.EFClasses;

namespace Expenses.Security
{
    public interface ISecurityContext
    {
        User User { get; }

        bool IsAdministrator { get; }
    }
}