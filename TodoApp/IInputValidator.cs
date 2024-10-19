namespace TodoApp;

public interface IInputValidator
{
    void ValidateString(string? input);
    void ValidateInt(string? input, string errorMessage = "Format is not accepted");
    bool IsInputEqual(string? input, string value);
}
