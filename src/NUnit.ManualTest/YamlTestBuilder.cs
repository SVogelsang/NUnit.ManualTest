using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

namespace NUnit.ManualTest
{
  /// <summary>
  /// The builder for building test scenarios from YAML files.
  /// </summary>
  public static class YamlTestBuilder
  {
    /// <summary>
    /// Builds a sequence of test scenarios from the given YAML file.
    /// </summary>
    /// <param name="filename">The YAML file.</param>
    /// <returns>The list of <see cref="TestCaseData"/> that can be passed a <see cref="TestCaseSourceAttribute"/>.</returns>
    public static IEnumerable BuildFrom(string filename)
    {
      using (var yamlReader = File.OpenText(filename))
      {
        return new Deserializer(namingConvention: new CamelCaseNamingConvention(), ignoreUnmatched: true).Deserialize<IEnumerable<TestScenarios>>(yamlReader)
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