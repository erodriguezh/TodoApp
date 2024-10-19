namespace TodoApp;

using System.Globalization;
using Ardalis.GuardClauses;

public class UserInterface(ITodoActions todoActions, IInputValidator inputValidator) : IUserInterface
{
    // Menu options
    private const string AddOption = "a";
    private const string RemoveOption = "r";
    private const string ListAllOption = "l";

    // Affirmative response
    private const string Yes = "y";

    // Menu prompts
    private const string MainMenuPrompt = "Choose an option from the following list:";
    private const string AddTodoPrompt = "\ta - Add Todo";
    private const string RemoveTodoPrompt = "\tr - Remove Todo";
    private const string ListTodosPrompt = "\tl - Lists all Todos";
    private const string QuitAppPrompt = "\tq - Quit App";
    private const string YourOptionPrompt = "Your option? ";

    // Input prompts
    private const string WriteTodoTitlePrompt = "Write a Todo Title ";
    private const string WritePriorityPrompt = "Write a Priority ";
    private const string WriteTodoNumberToDeletePrompt = "Write a Todo Number to Delete ";

    // Error messages
    private const string FormatNotAcceptedError = "Format is not accepted";
    private const string AddTodoError = "There was an error adding the todo. Would you like to retry? (Y/N)";
    private const string RemoveTodoError = "There was an error removing the todo, would you like to retry? Y/N";

    // Success messages
    private const string AllTodosDoneMessage = "Hooray all Todos are done!";

    public void DisplayMainMenu()
    {
        Console.WriteLine(MainMenuPrompt);
        Console.WriteLine(AddTodoPrompt);
        Console.WriteLine(RemoveTodoPrompt);
        Console.WriteLine(ListTodosPrompt);
        Console.WriteLine(QuitAppPrompt);
        Console.Write(YourOptionPrompt);
    }

    public void HandleUserInput(string? input)
    {
        inputValidator.ValidateString(input);

        var normalizedInput = input!.ToLower(CultureInfo.InvariantCulture).Trim();

        switch (normalizedInput)
        {
            case AddOption:
                this.AddTodo();
                break;
            case RemoveOption:
                this.RemoveTodo();
                break;
            case ListAllOption:
                this.ListTodos();
                break;
            default:
                Environment.Exit(0);
                break;
        }

    }

    private void AddTodo()
    {
        try
        {
            var newTodo = this.CreateTodo();
            todoActions.Add(newTodo);
        }
        catch
        {
            this.HandleAddTodoError();
        }
    }

    private Todo CreateTodo()
    {
        var title = this.RequestTodoTitle();
        var priorityValue = this.RequestTodoPriority();
        var priority = (Priority)priorityValue;

        return new Todo { Title = title, Priority = priority };
    }

    private void HandleAddTodoError()
    {
        Console.WriteLine(AddTodoError);
        if (this.ShouldRetry())
        {
            this.AddTodo();
        }
    }

    private bool ShouldRetry()
    {
        var response = Console.ReadLine();

        return inputValidator.IsInputEqual(response, Yes);
    }

    private int RequestTodoPriority()
    {
        var priorityNumber = this.RequestInputNumber(WritePriorityPrompt, FormatNotAcceptedError);

        Guard.Against.OutOfRange(priorityNumber, nameof(priorityNumber), 1, 4, "Value must be between 1 and 4");

        return priorityNumber;
    }

    private string RequestTodoTitle()
    {
        Console.Write(WriteTodoTitlePrompt);
        var title = Console.ReadLine();

        inputValidator.ValidateString(title);

        return title ?? string.Empty;
    }

    private void RemoveTodo()
    {
        this.ListTodos();
        var index = RequestTodoNumber();

        try
        {
            todoActions.Remove(index);
        }
        catch
        {
            this.HandleRemoveTodoError();
        }
    }

    private int RequestInputNumber(string requestMessage, string invalidInputMessage)
    {
        Console.Write($"{requestMessage} ");
        var input = Console.ReadLine();

        inputValidator.ValidateInt(input, invalidInputMessage);

        return int.Parse(input ?? string.Empty, CultureInfo.InvariantCulture);
    }

    private int RequestTodoNumber()
    {
        return this.RequestInputNumber(WriteTodoNumberToDeletePrompt, FormatNotAcceptedError);
    }

    private void HandleRemoveTodoError()
    {
        Console.WriteLine(RemoveTodoError);

        if (this.ShouldRetry())
        {
            this.RemoveTodo();
        }
    }

    private void ListTodos()
    {
        this.CheckIfTodoListIsEmpty();

        var todoIndex = 0;
        foreach (var todo in todoActions.ListAllTodos())
        {
            Console.WriteLine($"{todoIndex} - {todo.Title} - {todo.Priority}");
            todoIndex++;
        }
    }

    private void CheckIfTodoListIsEmpty()
    {
        if (todoActions.ListAllTodos() is not { } todos || todos.Any())
        {
            return;
        }

        Console.WriteLine(AllTodosDoneMessage);
        Console.WriteLine(Environment.NewLine);
    }
}
