namespace TodoApp;

public interface ITodoActions
{
    void Add(Todo todo);
    void Remove(int index);
    IEnumerable<Todo> ListAllTodos();
}
