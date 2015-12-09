using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace NUnit.ManualTest
{
  public static class TestScenarionBuilder
  {
    public static IEnumerable BuildFromYaml(string filename)
    {
      using (var yamlReader = File.OpenText(filename))
      {
        return new Deserializer(namingConvention: new CamelCaseNamingConvention(), ignoreUnmatched: true).Deserialize<IEnumerable<YamlTestScenarios>>(yamlReader)
                                                                                                         .Select(scenario => MakeTestCase(scenario.Scenario)).ToList();
      }
    }

    private static TestCaseData MakeTestCase(TestScenario scenario)
    {
      var testCaseData = new TestCaseData(scenario);
      testCaseData.SetDescription(scenario.Description);
      testCaseData.SetName(scenario.Name ?? "unnamed test");
      return testCaseData;
    }
  }
}