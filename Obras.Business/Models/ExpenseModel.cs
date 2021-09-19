﻿using Obras.Data.Enums;

namespace Obras.Business.Models
{
    public class ExpenseModel
    {
        public string Description { get; set; }
        public bool Active { get; set; }
        public TypeExpense TypeExpense { get; set; }
        public int? CompanyId { get; set; }
        public string RegistrationUserId { get; set; }
        public string ChangeUserId { get; set; }
    }
}
