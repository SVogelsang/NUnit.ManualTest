.NET|Mono
----|----
[![Build status .NET](https://ci.appveyor.com/api/projects/status/9cwd0dgla00jpy3q?svg=true)](https://ci.appveyor.com/project/SVogelsang/nunit-manualtest)|[![Build Status Mono](https://travis-ci.org/SVogelsang/NUnit.ManualTest.svg?branch=master)](https://travis-ci.org/SVogelsang/NUnit.ManualTest)

[![Issue Stats](http://issuestats.com/github/SVogelsang/NUnit.ManualTest/badge/issue)](http://issuestats.com/github/SVogelsang/NUnit.ManualTest)

# NUnit.ManualTest
NUnit.ManualTest is designed to include manual tests in the NUnit Framework infrastructure. The developer simply can add manual test scenarios to an NUnit test that guides a manual tester step-by-step through the testing scenario. The benefit is a NUnit compliant report.
Furthermore it offers the possibility that non-programmers e.g. product owners, product managers or marketing can define manual test cases through YAML files. See [Github Pages](https://svogelsang.github.io/NUnit.ManualTest/) for further information.

## Features
* Uses NUnit infrastructure for reports.
* Integrated with NUnit and allows the mixing of automated and manually performed steps.
* Let your product owners, product managers or marketing define manual tests by writing/editing YAML files.
* Configurable and/or customizable ways to get user feedback from manual tester (Console, WinForms MessageBoxes or custom implemented callbacks) via Attributes.


## Installation
```
nuget install NUnit.ManualTest

```

## Specifying presenter
A user presenter can either be specified on test fixture attribute

``` C#
[TestFixture, UserPresenter(typeof(ConsoleUserPresenter))]
public class SomeSpec : ManualTestBase
{
}
```
or assembly-wide using assembly attribute

``` C#
[assembly: UserPresenter(typeof(ConsoleUserPresenter))]

```
## Specifying presentation type
There are three types to guide the tester through the test:

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

## Examples
#### Tests generated from yaml file
``` C#
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
```

##### demo file


``` yaml
---
- scenario:
    name: When pressing 'print' button should open print dialog with pre-selected current page.
    description: printing single page.
    type: SingleStep
    preparations:
      - prepare: first preparation step
      - prepare: second preparation step
    executions:
      - execute: first execution step
      - execute: second execution step
    expectations:
      - expects: first expectation
      - expects: second expectation
```

#### Coded tests
``` C#
[Test]
public void When_doing_something_should_result_in_something()
{
  Test()
    .Prepare("Prepare something")
    .Prepare("Prepare something other")
    .Do("Execute something")
    .Do("Execute something other")
    .Verify("Verify somthing happened")
    .Verify("and also this one")
    .AsGroupedUserInteraction()
    .Go();
}
```

## Planned
* Support of nested test scenarios in YAML files
* Generating test cases and fixtures from markdown files

> This project was generated with [F# project scaffold](https://github.com/fsprojects/ProjectScaffold) :thumbsup:
