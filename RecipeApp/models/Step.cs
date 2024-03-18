namespace RecipeApp.Models;

/// <summary>
/// Step inside a recipe
/// </summary>
public class Step {
    public int TimeInMinutes { get; private set; }
    public string Instruction { get; private set; }

    /// <summary>
    /// Constructor with time in minutes and instruction
    /// </summary>
    /// <param name="timeInMinutes">Time to complete step</param>
    /// <param name="instruction">The actual step eg: Do potato</param>
    public Step(int timeInMinutes, string instruction) {
        if (timeInMinutes < 0) throw new ArgumentException("Time in minutes cannot be negative");
        if (instruction == null) throw new ArgumentException("Instruction cannot be null");
        if (instruction.Length == 0) throw new ArgumentException("Instruction cannot be empty");

        this.TimeInMinutes = timeInMinutes;
        this.Instruction = instruction;
    }
}
