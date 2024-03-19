namespace RecipeApp;

public class MainDummy {
    public static void Main(string[] args) {
        Console.WriteLine("Enter 1 to login or 2 to register");
        int decision = GetDecision();

        Console.WriteLine("Enter username");
        string username = GetInput();

        Console.WriteLine("Enter password");
        string password = GetInput();
    }

    private static int GetDecision() {
        int decision = 0;
        do {
            try {
                decision = int.Parse(Console.ReadLine());
            } catch (FormatException) {
                Console.WriteLine("Please enter a valid number");
            }
            if (decision != 1 && decision != 2) {
                Console.WriteLine("Please enter either 1 or 2");
            }
        } while (decision != 1 && decision != 2);
        return decision;
    }

    private static string GetInput() {
        string input = null;
        do {
            input = Console.ReadLine();
        } while (input == null);
        return input;
    }
}