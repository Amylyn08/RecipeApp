using Microsoft.EntityFrameworkCore;

namespace RecipeApp.Models;

/// <summary>
/// Step inside a recipe
/// </summary>
/// [Ke]
/// 
[Keyless]
public class Step {
    private int _timeInMinutes;
    private string _instruction;

    public Step() {}

    public int TimeInMinutes { 
        get => _timeInMinutes; 
        set {
            CheckTime(value);
            _timeInMinutes = value;
        } 
    }
    public string Instruction { 
        get => _instruction; 
        set {
            CheckInstruction(value);
            _instruction = value;
        } 
    }

    /// <summary>
    /// Constructor with time in minutes and instruction
    /// </summary>
    /// <param name="timeInMinutes">Time to complete step</param>
    /// <param name="instruction">The actual step eg: Do potato</param>
    public Step(int timeInMinutes, string instruction) {
        CheckTime(timeInMinutes);
        CheckInstruction(instruction);
        _timeInMinutes = timeInMinutes;
        _instruction = instruction;
    }

    /// <summary>
    /// Validate the time 
    /// </summary>
    /// <param name="timeInMinutes">Time in minutes</param>
    /// <exception cref="ArgumentException">If negative</exception>
    private static void CheckTime(int timeInMinutes) {
        if (timeInMinutes < 0) 
            throw new ArgumentException("Time in minutes cannot be negative");
    }

    /// <summary>
    /// Checks the instruction
    /// </summary>
    /// <param name="instruction">Instruction of step</param>
    /// <exception cref="ArgumentException">If null or empty</exception>
    private static void CheckInstruction(string instruction) {
        if (instruction == null) 
            throw new ArgumentException("Instruction cannot be null");
        if (instruction.Length == 0) 
            throw new ArgumentException("Instruction cannot be empty");
    }

    public override string ToString() {
        return "Time: " + TimeInMinutes + ", " + Instruction;
    }
}
