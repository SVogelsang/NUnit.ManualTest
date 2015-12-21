namespace NUnit.ManualTest
{
  /// <summary>
  /// The base for all manual test fixtures.
  /// </summary>
  public abstract class ManualTestBase
  {
    private readonly PresentationType _presentationType;
    private readonly IUserPresenter _presenter;

    /// <summary>
    /// Call this ctor if you want to use a <see cref="PresentationType"/> different from default <see cref="PresentationType.Once"/>.
    /// </summary>
    /// <param name="presentationType">Presentation type used for all manual tests (if not overruled by test itself).</param>
    protected ManualTestBase(PresentationType presentationType = PresentationType.Once)
    {
      _presentationType = presentationType;
      _presenter = UserPresenterAttribute.CreatePresenter(GetType());
    }

    /// <summary>
    /// Create a builder for a single manual test.
    /// </summary>
    /// <returns>A test builder.</returns>
    protected ManualTestBuilder Test()
    {
      return new ManualTestBuilder(_presenter, _presentationType);
    }

    /// <summary>
    /// Run a test scenario. This is commonly used for tests generated from YAML files. 
    /// </summary>
    /// <param name="scenario">The scenario to be executed.</param>
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