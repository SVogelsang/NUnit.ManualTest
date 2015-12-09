[![Build status](https://ci.appveyor.com/api/projects/status/9cwd0dgla00jpy3q?svg=true)](https://ci.appveyor.com/project/SVogelsang/nunit-manualtest)
[![Issue Stats](http://issuestats.com/github/SVogelsang/NUnit.ManualTest/badge/issue)](http://issuestats.com/github/SVogelsang/NUnit.ManualTest)

# NUnit.ManualTest
NUnit.ManualTest is designed to include manual tests in the NUnit Framework infrastructure. The developer simply can add manual test scenarios to an NUnit test that guides a manual tester step-by-step through the testing scenario. The benefit is a NUnit compliant report.
Furthermore it offers the possibility that non-programmers e.g. product ownders can define manual test cases through YAML files.

##Features
* Uses NUnit infrastructure for reports.
* Integrated with NUnit and allows the mixing of automated and manually performed steps.
* Let your product owners, product managers or marketing define manual tests by writing/editing YAML files.
* Configurable and/or customizable ways to get user feedback from manual tester (Console, WinForms MessageBoxes or custom implemented callbacks) via Attributes.

##Examples
####Test with automatic data validation
``` C#

```

##Planned
* Build environment
* NuGet package
* Documenation and examples
