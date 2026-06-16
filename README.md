#  Simple expense tracker application to manage your finances. The application allow users to add, delete, and view their expenses. The application provide a summary of the expenses.

Application run from the command line and have the following features:
- Users can add an expense with a description and amount.
- Users can update an expense.
- Users can delete an expense.
- Users can view all expenses.
- Users can view a summary of all expenses.
- Users can view a summary of expenses for a specific month (of current year).
- Users can filter expenses by category

I would add:

- Export expenses to a CSV file.
- Allow to set a budget for each month and show a warning when the user exceeds the budget

 [Link to roadmap project "Expense Tracker"](https://roadmap.sh/projects/expense-tracker)

# How to use

```csharp
//Help
-h -? --help

//add new expense
add --description "milk" --amount 10 --category "food"

//update an expense
update 1 -d "bread" -a 5

//delete an expense
delete 1

//show all expenses
list

//show an category of expenses
list -c food

//summary of all expenses
summary

//summary of expenses for a specific month
summary -m 6
```
