[![Build status](https://ci.appveyor.com/api/projects/status/9cwd0dgla00jpy3q?svg=true)](https://ci.appveyor.com/project/SVogelsang/nunit-manualtest)

[![Issue Stats](http://issuestats.com/github/SVogelsang/NUnit.ManualTest/badge/issue)](http://issuestats.com/github/SVogelsang/NUnit.ManualTest)

# NUnit.ManualTest
NUnit.ManualTest is designed to include manual tests in the NUnit Framework infrastructure. The developer simply can add manual test scenarios to an NUnit test that guides a manual tester step-by-step through the testing scenario. The benefit is a NUnit compliant report.
Furthermore it offers the possibility that non-programmers e.g. product ownders, product managers or marketing can define manual test cases through YAML files.

## Features
* Uses NUnit infrastructure for reports.
* Integrated with NUnit and allows the mixing of automated and manually performed steps.
* Let your product owners, product managers or marketing define manual tests by writing/editing YAML files.
* Configurable and/or customizable ways to get user feedback from manual tester (Console, WinForms MessageBoxes or custom implemented callbacks) via Attributes.

## Installation
```
nuget install NUnit.ManualTest

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
``` yaml
# demo file
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
//todo
```
## Planned
* NuGet package
* Documenation and examples
* Generating test cases and fixtures from markdown files
