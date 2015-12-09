namespace NUnit.ManualTest
{
  public abstract class ManualTestBase
  {
    private readonly IUserPresenter _presenter = UserPresenterAttribute.CreatePresenter();

    protected ManualTestBuilder Test()
    {
      return new ManualTestBuilder(_presenter);
    }

    protected void RunScenario(TestScenario scenario)
    {
      var test = Test();
      test.PresentationType = scenario.PresentationType;

      scenario.Preparations.ForEach(preparation => test.Prepare(preparation.Prepare));
      scenario.Executions.ForEach(execution => test.Do(execution.Execute));
      scenario.Expectations.ForEach(expectation => test.Verify(expectation.Expects));
      test.Ok();
    }
  }
}