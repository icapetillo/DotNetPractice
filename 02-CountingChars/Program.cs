namespace _02_CountingChars;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, what is the input string? ");
        var inputString = Console.ReadLine();
        Console.WriteLine($"The string {inputString} is {inputString.Length} characters long");
    }
}
