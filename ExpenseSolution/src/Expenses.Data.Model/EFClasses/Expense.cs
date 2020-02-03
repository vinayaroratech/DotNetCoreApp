﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Expenses.Data.Model.EFClasses
{
    public class Expense
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public string Comment { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public bool IsDeleted { get; set; }
    }

}
