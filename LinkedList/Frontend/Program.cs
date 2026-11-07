using Backend;

Console.WriteLine("Select the list data type:");
Console.WriteLine("1. Integers.");
Console.WriteLine("2. Strings.");
Console.Write("Enter your option: ");
var typeOption = Console.ReadLine();

if (typeOption == "1")
{
    RunMenu(new DoublyLinkedList<int>());
}
else
{
    RunMenu(new DoublyLinkedList<string>());
}

void RunMenu<T>(DoublyLinkedList<T> list) where T : IComparable<T>, IParsable<T>
{
    var option = string.Empty;
    do
    {
        option = Menu();
        switch (option)
        {
            case "1":
                if (TryReadValue(out T newValue))
                {
                    list.Add(newValue);
                    Console.WriteLine($"The item: {newValue} was added.");
                }
                break;
            case "2":
                Console.WriteLine(list);
                break;
            case "3":
                Console.WriteLine(list.ToStringReverse());
                break;
            case "4":
                list.SortDescending();
                Console.WriteLine("The list was sorted in descending order.");
                Console.WriteLine(list);
                break;
            case "5":
                ShowModes(list.GetModes());
                break;
            case "6":
                Console.WriteLine(list.GetGraph());
                break;
            case "7":
                if (TryReadValue(out T searchValue))
                {
                    Console.WriteLine(list.Contains(searchValue)
                        ? $"The item: {searchValue} exists."
                        : $"The item: {searchValue} doesn't exist.");
                }
                break;
            case "8":
                if (TryReadValue(out T valueToRemove))
                {
                    Console.WriteLine(list.RemoveFirst(valueToRemove)
                        ? $"The first occurrence of {valueToRemove} was removed."
                        : $"The item: {valueToRemove} doesn't exist.");
                }
                break;
            case "9":
                if (TryReadValue(out T valuesToRemove))
                {
                    var removedCount = list.RemoveAll(valuesToRemove);
                    Console.WriteLine(removedCount > 0
                        ? $"{removedCount} occurrence(s) of {valuesToRemove} were removed."
                        : $"The item: {valuesToRemove} doesn't exist.");
                }
                break;
            case "0":
                Console.WriteLine("Goodbye!");
                break;
            default:
                Console.WriteLine("Invalid option.");
                break;
        }
    } while (option != "0");
}
bool TryReadValue<T>(out T value) where T : IParsable<T>
{
    Console.Write("Enter value: ");
    var input = Console.ReadLine();

    if (string.IsNullOrWhiteSpace(input) || !T.TryParse(input, null, out value!))
    {
        Console.WriteLine("Invalid value.");
        value = default!;
        return false;
    }
    return true;
}
void ShowModes<T>(List<T> modes)
{
    if (modes.Count == 0)
    {
        Console.WriteLine("Empty list");
        return;
    }

    if (modes.Count == 1)
    {
        Console.WriteLine($"The mode is {modes[0]}.");
        return;
    }

    var output = "The modes are ";
    for (var i = 0; i < modes.Count; i++)
    {
        output += modes[i];
        if (i < modes.Count - 2)
        {
            output += ", ";
        }
        else if (i == modes.Count - 2)
        {
            output += " and ";
        }
    }
    Console.WriteLine($"{output}.");
}
string Menu()
{
    Console.WriteLine();
    Console.WriteLine("1. Add.");
    Console.WriteLine("2. Show forward.");
    Console.WriteLine("3. Show backward.");
    Console.WriteLine("4. Sort descending.");
    Console.WriteLine("5. Show mode(s).");
    Console.WriteLine("6. Show graph.");
    Console.WriteLine("7. Exists.");
    Console.WriteLine("8. Remove one occurrence.");
    Console.WriteLine("9. Remove all occurrences.");
    Console.WriteLine("0. Exit.");
    Console.Write("Enter your option: ");
    return Console.ReadLine() ?? string.Empty;
}