using YamlDotNet.Serialization;

namespace NUnit.ManualTest
{
  /// <summary>
  /// A test sequence of multiple <see cref="TestScenario"/>.
  /// </summary>
  public class TestScenarios
  {
    /// <summary>
    /// The list of scenarios.
    /// </summary>
    [YamlMember(Alias = "scenario")]
    public TestScenario Scenario
    {
      get;
      set;
    }
  }
}