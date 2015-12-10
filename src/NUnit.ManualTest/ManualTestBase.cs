namespace NUnit.ManualTest
{
  public abstract class ManualTestBase
  {
    private readonly PresentationType _presentationType;
    private readonly IUserPresenter _presenter = UserPresenterAttribute.CreatePresenter();

    protected ManualTestBase(PresentationType presentationType = PresentationType.Once)
    {
      _presentationType = presentationType;
    }

    protected ManualTestBuilder Test()
    {
      return new ManualTestBuilder(_presenter, _presentationType);
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