using ExpenseTracker;
using System.CommandLine;
using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;
class Program
{
    private static void Main(string[] args)
    {
        Console.WriteLine("Welcome to Expense Tracker!");

        Commands();
    }

    private static void Commands() {
        while (true) {

            Console.WriteLine();
            string command = Console.ReadLine() ?? "";

            Option<string> descOption = new(name: "--description", aliases: "-d")
            {
                Description = "description"
            };

            Option<int> amountOption = new(name: "--amount", aliases: "-a")
            {
                Description = "Amount"
            };

            Argument<int> idArgiment = new("id") { Description = "Expense id" };

            Command exitCommand = new("exit", "Close the project");
            exitCommand.SetAction(parseResult => Environment.Exit(0));

            Command listCommand = new("list", "View all expenses");
            listCommand.SetAction(parseResulr => FileRepository.ListExpenses());

            Command delCommand = new("delete",  "Delete an expense")
            {
                idArgiment
            };
            delCommand.SetAction(parseResult => FileRepository.DeleteExpense(parseResult.GetValue(idArgiment)));

            Command updateCpmmand = new("update", "Update an expense with a description and amount")
            {
                idArgiment,
                descOption,
                amountOption,
            };
            updateCpmmand.SetAction(parseResult => FileRepository.UpdateExpense(parseResult.GetValue(idArgiment), new Expense(parseResult.GetValue(descOption) ?? "", parseResult.GetValue(amountOption))));

            Command addCommand = new("add", "Add an expense with a description and amount") //подкоманда
            {
               descOption,
               amountOption,
            };
            addCommand.Aliases.Add("create");
            addCommand.SetAction(parseResult => FileRepository.AddExpense(new Expense(parseResult.GetValue(descOption)??"", parseResult.GetValue(amountOption))));


            RootCommand rootCommand = new("Simple expense tracker application to manage your finances")
            {
                addCommand,
                updateCpmmand,
                exitCommand,
                listCommand,
                delCommand
            };

            rootCommand.Parse(command).Invoke();
        }
    }
}