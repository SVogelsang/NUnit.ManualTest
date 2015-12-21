using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace NUnit.ManualTest
{
  /// <summary>
  /// A single test scenario created by e.g. <see cref="YamlTestBuilder"/>.
  /// </summary>
  public class TestScenario
  {
    /// <summary>
    /// A single test preparation created by e.g. <see cref="YamlTestBuilder"/>.
    /// </summary>
    public class Preparation
    {
      [YamlMember(Alias = "prepare")]
      public string Prepare
      {
        get;
        set;
      }
    }

    /// <summary>
    /// A single test execution created by e.g. <see cref="YamlTestBuilder"/>.
    /// </summary>
    public class Execution
    {
      [YamlMember(Alias = "execute")]
      public string Execute
      {
        get;
        set;
      }
    }

    /// <summary>
    /// A single test expectation created by e.g. <see cref="YamlTestBuilder"/>.
    /// </summary>
    public class Expectation
    {
      [YamlMember(Alias = "expects")]
      public string Expects
      {
        get;
        set;
      }
    }

    /// <summary>
    /// Creates a new instance.
    /// </summary>
    public TestScenario()
    {
      Preparations = new List<Preparation>();
      Executions = new List<Execution>();
      Expectations = new List<Expectation>();
      PresentationType = PresentationType.Once;
    }

    /// <summary>
    /// The name of the test scenario.
    /// </summary>
    public string Name
    {
      get;
      set;
    }

    /// <summary>
    /// An optional description of the test scenario.
    /// </summary>
    public string Description
    {
      get;
      set;
    }

    /// <summary>
    /// The presentation type. Default is Once.
    /// </summary>
    [YamlMember(Alias = "type")]
    public PresentationType PresentationType
    {
      get;
      set;
    }

    /// <summary>
    /// The list of preparations.
    /// </summary>
    public List<Preparation> Preparations
    {
      get;
      set;
    }

    /// <summary>
    /// The list of executions.
    /// </summary>
    public List<Execution> Executions
    {
      get;
      set;
    }

    /// <summary>
    /// The list of expecetations.
    /// </summary>
    public List<Expectation> Expectations
    {
      get;
      set;
    }
  }
}