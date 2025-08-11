// See https://aka.ms/new-console-template for more information
public class Program
{
    public bool IsPalindrome(int x)
    {
        // Fix: Convert the reversed enumerable to a string using string.Concat
        string reversed = string.Concat(x.ToString().ToCharArray().Reverse());
        return x.ToString() == reversed;
    }
    public static void Main(string[] args)
    {
        int number = 356; // Example number
        bool result = new Program().IsPalindrome(number);
        Console.WriteLine($"{number} is palindrome: {result}");
    }
}
