using System.ComponentModel;

namespace Expenses.Data.Access.Constants
{
    enum Roles
    {
        [Description("Administrator")]
        Administrator = 1,
        [Description("Manager")]
        Manager = 2,
        [Description("Administrator Manager")]
        AdministratorOrManager = 3
    }
}