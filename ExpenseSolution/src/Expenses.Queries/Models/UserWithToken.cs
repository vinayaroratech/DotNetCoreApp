using System;
using Expenses.Data.Model;
using Expenses.Data.Model.EFClasses;

namespace Expenses.Queries.Models
{
    public class UserWithToken
    {
        public string Token { get; set; }
        public User User { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}