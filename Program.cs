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
        while(true){

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

            Command exitCommand = new("exit", "Close the project");
            exitCommand.SetAction(parseResult => Environment.Exit(0));

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
                exitCommand
            };

            rootCommand.Parse(command).Invoke();
        }
    }
}