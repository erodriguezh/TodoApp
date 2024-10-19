using Ardalis.GuardClauses;

namespace TodoApp;

public partial class TodoActions : ITodoActions
{
    private readonly List<Todo> _todos = [];
    public void Add(Todo todo)
    {
        this._todos.Add(todo);
    }

    public void Remove(int index)
    {
        Guard.Against.OutOfRange(index, nameof(index), 1, this._todos.Count, "Invalid todo index.");
        this._todos.RemoveAt(index);
    }

    public IEnumerable<Todo> ListAllTodos() => this._todos.AsReadOnly();
}
