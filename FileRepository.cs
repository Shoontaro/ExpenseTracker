using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ExpenseTracker
{
    public class FileRepository
    {

        private static readonly string FilePath = Path.Combine(Directory.GetCurrentDirectory(), "expenses.json");

        public static void AddExpense(Expense expense)
        {
            List<Expense> expenses = LoadExpenses();
            expense.Id = expenses.Count > 0 ? expenses.Last().Id + 1 : 1;

            expenses.Add(expense);
            SaveExpenses(expenses);

            Console.WriteLine($"Expense added successfully (ID: {expense.Id})");
        }

        private static List<Expense> LoadExpenses()
        {
            if (!File.Exists(FilePath)) return new List<Expense>();//возвращаем пустой лист, если файла не существует

            var json = File.ReadAllText(FilePath);
            return JsonSerializer.Deserialize<List<Expense>>(json) ?? new List<Expense>();
        }

        private static void SaveExpenses(List<Expense> todos)
        {
            var json = JsonSerializer.Serialize(todos, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(FilePath, json);
        }
    }
}
