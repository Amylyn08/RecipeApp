using Microsoft.EntityFrameworkCore;

namespace RecipeApp.Models;

/// <summary>
/// Step inside a recipe
/// </summary>
/// [Ke]
/// 
public class Step {
    private int _timeInMinutes;
    private string _instruction;

    public int StepId {
        get; 
        set;
    }

    public Recipe Recipe {
        get; 
        set;
    }

    public int TimeInMinutes { 
        get => _timeInMinutes; 
        set {
            if (value < 0) 
                throw new ArgumentException("Time in minutes cannot be negative");
            _timeInMinutes = value;
        } 
    }

    public string Instruction { 
        get => _instruction; 
        set {
            if (value == null) 
                throw new ArgumentException("Instruction cannot be null");
            if (value.Length == 0) 
                throw new ArgumentException("Instruction cannot be empty");
            _instruction = value;
        } 
    }

    /// <summary>
    /// Constructor with time in minutes and instruction
    /// </summary>
    /// <param name="timeInMinutes">Time to complete step</param>
    /// <param name="instruction">The actual step eg: Do potato</param>
    public Step(int timeInMinutes, string instruction) {
        TimeInMinutes = timeInMinutes;
        Instruction = instruction;
    }

    /// <summary>
    /// Empty constructor for entity framework
    /// </summary>
    public Step() {

    }

    /// <summary>
    /// Overriden ToString()
    /// </summary>
    /// <returns>String representation of a step</returns>
    public override string ToString() {
        return "Time: " + TimeInMinutes + "minutes, Instruction: " + Instruction;
    }
}
