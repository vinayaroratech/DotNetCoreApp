using Expenses.Api.Models.Login;
using Expenses.Api.Models.Users;
using Expenses.Data.Model.EFClasses;
using Expenses.Queries.Models;
using System.Threading.Tasks;

namespace Expenses.Queries
{
    public interface ILoginQueryProcessor
    {
        UserWithToken Authenticate(string username, string password);
        Task<User> Register(RegisterModel model);
        Task ChangePassword(ChangeUserPasswordModel requestModel);
    }
}