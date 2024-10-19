
using TodoApp;

var todoManager = new TodoActions();
var inputValidator = new InputValidator();
var userInterface = new UserInterface(todoManager, inputValidator);

while (true)
{
    userInterface.DisplayMainMenu();
    var input = Console.ReadLine();
    userInterface.HandleUserInput(input);
}
