using System.Collections.Generic;
using YamlDotNet.Serialization;

namespace NUnit.ManualTest
{
  public class TestScenario
  {
    public class Preparation
    {
      [YamlMember(Alias = "prepare")]
      public string Prepare
      {
        get;
        set;
      }
    }

    public class Execution
    {
      [YamlMember(Alias = "execute")]
      public string Execute
      {
        get;
        set;
      }
    }

    public class Expectation
    {
      [YamlMember(Alias = "expects")]
      public string Expects
      {
        get;
        set;
      }
    }

    public TestScenario()
    {
      Preparations = new List<Preparation>();
      Executions = new List<Execution>();
      Expectations = new List<Expectation>();
    }

    public string Name
    {
      get;
      set;
    }
    public string Description
    {
      get;
      set;
    }
    [YamlMember(Alias = "type")]
    public PresentationType PresentationType
    {
      get;
      set;
    }
    public List<Preparation> Preparations
    {
      get;
      set;
    }
    public List<Execution> Executions
    {
      get;
      set;
    }
    public List<Expectation> Expectations
    {
      get;
      set;
    }
  }
}