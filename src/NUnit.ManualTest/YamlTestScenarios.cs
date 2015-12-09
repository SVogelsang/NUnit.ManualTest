using YamlDotNet.Serialization;

namespace NUnit.ManualTest
{
  public class YamlTestScenarios
  {
    [YamlMember(Alias = "scenario")]
    public TestScenario Scenario
    {
      get;
      set;
    }
  }
}