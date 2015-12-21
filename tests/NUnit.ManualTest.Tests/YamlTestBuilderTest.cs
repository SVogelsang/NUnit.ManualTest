using System.Collections;
using System.Linq;
using NUnit.Framework;

namespace NUnit.ManualTest.Tests
{
  [TestFixture, UserPresenter(typeof(FakeUserPresenter))]
  public class TestScenarionBuilderTest : ManualTestBase
  {
    [Test]
    public void When_building_from_yaml_file_should_generate_test_cases()
    {
      var scenarios = YamlTestBuilder.BuildFrom("Demo.yaml").Cast<TestCaseData>().ToList();
      var first = (TestScenario)scenarios.First().Arguments.First();

      Assert.AreEqual(1, first.Preparations.Count);
      Assert.AreEqual(1, first.Executions.Count);
      Assert.AreEqual(1, first.Expectations.Count);

      var second = (TestScenario)scenarios.ElementAt(1).Arguments.First();

      Assert.AreEqual(2, second.Preparations.Count);
      Assert.AreEqual(2, second.Executions.Count);
      Assert.AreEqual(2, second.Expectations.Count);
    }

    public static IEnumerable Source()
    {
      return YamlTestBuilder.BuildFrom("Demo.yaml");
    }

    [Test, TestCaseSource("Source")]
    public void When_using_yaml_file_as_test_case_source_should_generate_test_cases(TestScenario scenario)
    {
      RunScenario(scenario);
    }
  }
}