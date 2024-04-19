using RecipeApp.Models;

namespace RecipeAppTest.Models;

[TestClass]
public class StepTest {
    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void NegativeTime_Throws_ArgumentException() {
        Step step = new(-1, "Potate");
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void NullInstruction_Throws_ArgumentException() {
        Step step = new(0, null);
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void EmptyInstruction_Throws_ArgumentException() {
        Step step = new(0, "");
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void TimeInMinutesSetter_Negative_Throws_ArgumentException() {
        Step step = new(5, "Boil potato");
        step.TimeInMinutes = -1;
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void InstructionSetter_Null_Throws_ArgumentException() {
        Step step = new(5, "Boil potato");
        step.Instruction = null;
    }

    [TestMethod]
    [ExpectedException(typeof(ArgumentException))]
    public void InstructionSetter_Empty_Throws_ArgumentException() {
        Step step = new(5, "Boil potato");
        step.Instruction = "";
    }

    [TestMethod]
    public void Constructor_Init() {
        Step step = new(5, "Boil potato");
        Assert.AreEqual(5, step.TimeInMinutes);
        Assert.AreEqual("Boil potato", step.Instruction);
    }
}
