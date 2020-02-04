using Expenses.Api.Models.Users;
using Expenses.Data.Model.EFClasses;
using System.Linq;
using System.Threading.Tasks;

namespace Expenses.Queries
{
    public interface IUsersQueryProcessor
    {
        IQueryable<User> Get();
        User Get(int id);
        Task<User> Create(CreateUserModel model);
        Task<User> Update(int id, UpdateUserModel model);
        Task Delete(int id);
        Task ChangePassword(int id, ChangeUserPasswordModel model);
    }
}