using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker
{
    public class Expense
    {
        public int Id { get; set; }
        public string? Description { get; set; }
        public int Amount { get; set; } //возможно лучше double
        public DateTime CreateAt { get; set; }
        public string Category { get; set; }

        public Expense() { }
        public Expense(string description, int amount)
        {
            Description = description;
            Amount = amount;
            CreateAt = DateTime.Now;
        }

        public Expense(string description, int amount, string category) : this(description, amount) {
            Category = category;
        }
    }
}
