TodoApp todoApp = new TodoApp();
todoApp.Start();

public class Todo
{
    public string Title { get; init; }
    public int Priority { get; init; }
}

public sealed class TodoApp
{
    private List<Todo> todoList = new List<Todo>();

    public void Start()
    {
        while (true)
        {
            Console.WriteLine("Choose an option from the following list:");
            Console.WriteLine("\ta - Add Todo");
            Console.WriteLine("\tr - Remove Todo");
            Console.WriteLine("\tl - Lists all Todos");
            Console.WriteLine("\tq - Quit App");
            Console.Write("Your option? ");

            switch (Console.ReadLine())
            {
                case "a":
                    AddTodo();
                    break;
                case "r":
                    RemoveTodo();
                    break;
                case "l":
                    ListsTodos();
                    break;            
                case "q":
                    Environment.Exit(-1);
                    break;
                default:
                    Console.WriteLine("Option not recognized...restarting");
                    break;
            }
        }

    }

    private void ListsTodos()
    {
        if (todoList.Count is 0)
        {
            Console.WriteLine("Hooray all Todos are done!");
        }
        int index = 0;
        foreach (var todo in todoList)
        {
            Console.WriteLine($"{index} - {todo.Title} - {todo.Priority}");
            index++;
        }
    }

    private void RemoveTodo()
    {
        ListsTodos();
        Console.Write("Write a Todo Number to Delete ");
        int index = Convert.ToInt32(Console.ReadLine());
        try
        {
            todoList.RemoveAt(index);
        }
        catch (Exception e)
        {
            Console.WriteLine("There was an error removing the todo, would you like to retry? Y/N");
            if (Console.ReadLine().Trim().ToLower() == 'y'.ToString())
            {
                RemoveTodo();
            }
        }
    }

    private void AddTodo()
    {
        try
        {
            Console.Write("Write a Todo Title ");
            string title = Console.ReadLine();
            Console.Write("Write a Priority ");
            int priority = Convert.ToInt32(Console.ReadLine());
            Todo newTodo = new Todo()
            {
                Title = title,
                Priority = priority
            };
            todoList.Add(newTodo);
        }
        catch (Exception e)
        {
            Console.WriteLine("There was an error adding the todo, would you like to retry? Y/N");
            if (Console.ReadLine().Trim().ToLower() == 'y'.ToString())
            {
                AddTodo();
            }
        }
    }
}