using System.Linq;
using NUnit.Framework;

namespace NUnit.ManualTest.Tests
{
  [TestFixture]
  public class TestScenarionBuilderTest
  {
    [Test]
    public void When_building_from_yaml_file_should_generate_test_cases()
    {
      var scenarios = TestScenarionBuilder.BuildFromYaml("demo.yaml").Cast<TestCaseData>().ToList();
      var first = (TestScenario)scenarios.First().Arguments.First();

      Assert.AreEqual(1, first.Preparations.Count);
      Assert.AreEqual(1, first.Executions.Count);
      Assert.AreEqual(1, first.Expectations.Count);

      var second = (TestScenario)scenarios.ElementAt(1).Arguments.First();

      Assert.AreEqual(2, second.Preparations.Count);
      Assert.AreEqual(2, second.Executions.Count);
      Assert.AreEqual(2, second.Expectations.Count);
    }
  }
}