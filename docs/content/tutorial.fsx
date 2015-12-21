(*** hide ***)
// This block of code is omitted in the generated HTML documentation. Use 
// it to define helpers that you do not want to show in the documentation.
#I "../../bin/NUnit.ManualTest"
//#r "NUnit.ManualTest.dll"
//#r "NUnit.Framework.dll"
//#r "../../packages/FSharp.Formatting/lib/net40/FSharp.Literate.dll"
//open NUnit.ManualTest
//open NUnit.Framework
//open FSharp.Literate


(**
NUnit integrated manual testing
===============================

NUnit.ManualTest is designed to include manual tests in the NUnit Framework infrastructure. The developer simply can add manual test scenarios to an NUnit test that guides a manual tester step-by-step through the testing scenario. The benefit is a NUnit compliant report.
Furthermore it offers the possibility that non-programmers e.g. product owners, product managers or marketing can define manual test cases through YAML files.

*)

//let source = YamlTestBuilder.BuildFrom("demo.yaml")

(**
# Installation

Install with nuget

```
nuget install NUnit.ManualTest
```

*)


(**
# Examples

## Generating tests from YAML files

    [lang=csharp]
    [TestFixture]
    public class SomeWorkflowSpec : ManualTestBase
    {
      public static IEnumerable Source()
      {
        return YamlTestBuilder.BuildFrom("demo.yaml");
      }

      [Test, TestCaseSource("Source")]
      public void SomeScenario(TestScenario scenario)
      {
        RunScenario(scenario);
      }
    }

## Choosing the presentation type
For each test fixture:

    [lang=csharp]
    [TestFixture, UserPresenter(typeof(ConsoleUserPresenter))]
    public class SomeSpec : ManualTestBase
    {
    }

Assembly-wide setup:


    [lang=csharp]
    [assembly: UserPresenter(typeof(ConsoleUserPresenter))]
    
**The default presenter is the console presenter.**

## Specifying presentation type
There are three types defined the tester is guided through the different test steps (preparation, execution and validation).

1. **SingleStep:** Each preparation, execution and verification step must be committed by the tester. This could be useful whenever single steps of complex testing scenarios might fail and should be reported in test report.
2. **Grouped:** Each group (preparation, execution and verification) must be committed by the tester.
2. **Once:** (default) The complete testing scenario is presented to the user and must be committed only once.

The presentation type can be specified:

1. **in YAML file:** using the type attribute inside the scenario.

``` yaml
- scenario:
    name: When pressing 'print' button should open print dialog with pre-selected current page.
    description: printing single page.
    type: SingleStep
``` 

2. **globally for whole test fixture:** passing to the type to base ctor.

``` C#
[TestFixture]
public class SomeSpec : ManualTestBase
{
  public SomeSpec() : base(PresentationType.Grouped)
  {
  }
}
```

3. **in coded tests:** by setting up the test builder.

``` C#
[Test]
public void When_doing_something_should_result_in_something()
{
  Test()
    .Prepare("Prepare something")
    .Do("Execute something")
    .Verify("Verify somthing happened")
    .AsGroupedUserInteraction()
    .Ok();
}
```
This overrules the 'higher-level' setups.

*)
