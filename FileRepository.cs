using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Serialization;
using ConsoleTables;
using System.Globalization;

namespace ExpenseTracker
{
    public class FileRepository
    {

        private static readonly string FilePath = Path.Combine(Directory.GetCurrentDirectory(), "expenses.json");
        private static readonly string FilePathCSV = Path.Combine(Directory.GetCurrentDirectory(), "expenses.csv");

        public static void Summary(int mnth) {
            if (mnth > 0 && mnth < 13)
            {
                string[] monthNames = CultureInfo.InvariantCulture.DateTimeFormat.MonthNames;

                Console.WriteLine($"Total expenses {monthNames[mnth - 1]}: {LoadExpenses().Where(v=>v.CreateAt.Month == mnth).Sum(v => v.Amount)}");
            }
            else if (mnth == 0) {
                Console.WriteLine($"Total expenses: {LoadExpenses().Sum(v=>v.Amount)}"); 
            }
            else
            {
                Console.WriteLine("Wrong month");
                return;
            }
        }

        public static void DeleteExpense(int id) {

            List<Expense> expenses = LoadExpenses();

            Expense expense = expenses.Find(x => x.Id == id);

            if (expense == null)
            {
                Console.WriteLine("Wrong id");
                return;
            }

            expenses.Remove(expense);

            SaveExpenses(expenses);

            Console.WriteLine("Expense deleted successfully");
        }

        public static void AddExpense(Expense expense)
        {
            List<Expense> expenses = LoadExpenses();
            expense.Id = expenses.Count > 0 ? expenses.Last().Id + 1 : 1;

            expenses.Add(expense);
            SaveExpenses(expenses);

            Console.WriteLine($"Expense added successfully (ID: {expense.Id})");
        }

        public static void ListExpenses(string category) {

            List<Expense> espenses = LoadExpenses().Where(v=>v.Category == category).ToList();

            if (espenses.Count == 0) {
                Console.WriteLine("Нет данных!");
                return;
            }

            var table = new ConsoleTable("ID", "Date", "Description", "Amount", "Category");

            foreach (Expense exp in espenses)
            {
                table.AddRow(exp.Id, exp.CreateAt.ToShortDateString(), exp.Description, exp.Amount, exp.Category);
            }
            table.Write();
        }
        public static void ListExpenses() {

            List<Expense> espenses = LoadExpenses();

            if (espenses.Count == 0) {
                Console.WriteLine("Нет данных!");
                return;
            }

            var table = new ConsoleTable("ID", "Date", "Description", "Amount", "Category");

            foreach (Expense exp in espenses)
            {
                table.AddRow(exp.Id, exp.CreateAt.ToShortDateString(), exp.Description, exp.Amount, exp.Category);
            }
            table.Write();
        }

        public static void UpdateExpense(int id, Expense expense) 
        {
            List<Expense> expenses = LoadExpenses();
            Expense exp =expenses.Find(v => v.Id == id);

            if (exp == null)
            {
                Console.WriteLine("Wrong id");
                return;
            }

            if (!string.IsNullOrEmpty(expense.Description)) { exp.Description = expense.Description; }
            if (expense.Amount>0) { exp.Amount = expense.Amount; }

            SaveExpenses(expenses);

            Console.WriteLine($"Expense was update (ID: {id})");
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

        public static void ExportToCSV()
        {
            List<Expense> expenses = LoadExpenses();
            var csvBuilder = new StringBuilder();

            csvBuilder.AppendLine("Id,Description,Amount,CreateAt,Category");

            foreach (Expense expense in expenses) {
                csvBuilder.AppendLine($"{expense.Id},{expense.Description},{expense.Amount},{expense.CreateAt},{expense.Category}");
            }

            File.WriteAllText(FilePathCSV, csvBuilder.ToString(), Encoding.UTF8);

            Console.WriteLine("Success export expenses to a CSV file");
        }
    }
}
