﻿using Obras.Business.ConstructionInvestorDomain.Response;
using Obras.Business.DocumentationDomain.Models;
using Obras.Business.ExpenseDomain.Models;
using System;
namespace Obras.Business.ConstructionDocumentationDomain.Response
{
    public class ConstructionExpenseResponse
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int ExpenseId { get; set; }
        public virtual ExpenseModel Expense { get; set; }
        public double Value { get; set; }
        public int ConstructionId { get; set; }
        public int ConstructionInvestorId { get; set; }
        public virtual ConstructionInvestorResponse ConstructionInvestor { get; set; }
        public bool Active { get; set; }
    }
}

