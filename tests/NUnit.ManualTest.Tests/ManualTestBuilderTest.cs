using Moq;
using NUnit.Framework;

namespace NUnit.ManualTest.Tests
{
  [TestFixture]
  public class ManualTestBuilderTest
  {
    [Test]
    public void When_preparation_execution_and_verification_steps_executed_without_single_user_steps_should_only_call_query()
    {
      var presenter = Mock.Of<IUserPresenter>(me => me.Query(It.IsAny<string>()));
      var sut = new Builder().WithPresenter(presenter).Build();

      sut.Prepare("prep").Do("do").Verify("ok").Ok();

      Mock.Get(presenter).Verify(me => me.Query(It.IsAny<string>()), Times.Once);
    }

    [Test]
    public void When_multiple_preparation_executed_with_single_user_steps_should_call_query_for_each_preparation()
    {
      var presenter = Mock.Of<IUserPresenter>(me => me.Query(It.IsAny<string>()));
      var sut = new Builder().WithPresenter(presenter).Build();

      sut.Prepare("1").Prepare("2").AsSingleStepUserInteraction().Ok();

      Mock.Get(presenter).Verify(me => me.Query("1"), Times.Once);
      Mock.Get(presenter).Verify(me => me.Query("2"), Times.Once);
    }

    [Test]
    public void When_multiple_executions_executed_with_single_user_steps_should_call_query_for_each_execution()
    {
      var presenter = Mock.Of<IUserPresenter>(me => me.Query(It.IsAny<string>()));
      var sut = new Builder().WithPresenter(presenter).Build();

      sut.Do("1").Do("2").AsSingleStepUserInteraction().Ok();

      Mock.Get(presenter).Verify(me => me.Query("1"), Times.Once);
      Mock.Get(presenter).Verify(me => me.Query("2"), Times.Once);
    }

    [Test]
    public void When_multiple_expectations_executed_with_single_user_steps_should_call_query_for_each_expectation()
    {
      var presenter = Mock.Of<IUserPresenter>(me => me.Query(It.IsAny<string>()));
      var sut = new Builder().WithPresenter(presenter).Build();

      sut.Verify("1").Verify("2").AsSingleStepUserInteraction().Ok();

      Mock.Get(presenter).Verify(me => me.Query("1"), Times.Once);
      Mock.Get(presenter).Verify(me => me.Query("2"), Times.Once);
    }

    [Test]
    public void When_multiple_preparations_executions_and_expectations_executed_with_grouped_callbacks_should_call_query_for_each_group()
    {
      var presenter = Mock.Of<IUserPresenter>(me => me.Query(It.IsAny<string>()));
      var sut = new Builder().WithPresenter(presenter).Build();

      sut.Prepare("1").Prepare("2").Do("3").Do("4").Verify("5").Verify("6").AsGroupedUserInteraction().Ok();

      Mock.Get(presenter).Verify(me => me.Query(It.IsAny<string>()), Times.Exactly(3));
    }

    [Test]
    public void When_one_expectatation_does_not_match_should_raise_assertion_exception()
    {
      var presenter = Mock.Of<IUserPresenter>(me => me.Query("1") && !me.Query("2"));
      var sut = new Builder().WithPresenter(presenter).Build();

      var prepare = sut.Verify("1").Verify("2").AsSingleStepUserInteraction();
      Assert.Throws<AssertionException>(() => prepare.Ok());
    }
    
    private class Builder
    {
      private IUserPresenter _presenter;

      public Builder WithPresenter(IUserPresenter presenter)
      {
        _presenter = presenter;
        return this;
      }

      public ManualTestBuilder Build()
      {
        return new ManualTestBuilder(_presenter ?? Mock.Of<IUserPresenter>());
      }
    }
  }
}