using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace NUnit.ManualTest
{
  public class ManualTestBuilder
  {
    private readonly IUserPresenter _presenter;
    private readonly List<string> _preparations = new List<string>();
    private readonly List<string> _executions = new List<string>();
    private readonly List<string> _expectations = new List<string>();

    public ManualTestBuilder(IUserPresenter presenter)
    {
      _presenter = presenter;
      PresentationType = PresentationType.Once;
    }

    public PresentationType PresentationType { get; set; }

    public ManualTestBuilder AsSingleStepUserInteraction()
    {
      PresentationType = PresentationType.SingleStep;
      return this;
    }

    public ManualTestBuilder AsGroupedUserInteraction()
    {
      PresentationType = PresentationType.Grouped;
      return this;
    }

    public ManualTestBuilder AsOneStepUserInteraction()
    {
      PresentationType = PresentationType.Once;
      return this;
    }

    public ManualTestBuilder Prepare(string format, params object[] args)
    {
      _preparations.Add(String.Format(format, args));
      return this;
    }

    public ManualTestBuilder Do(string format, params object[] args)
    {
      _executions.Add(String.Format(format, args));
      return this;
    }

    public ManualTestBuilder Verify(string format, params object[] args)
    {
      _expectations.Add(String.Format(format, args));
      return this;
    }

    public void Ok()
    {
      switch (PresentationType)
      {
        case PresentationType.Once:
          {
            string presentation = AppendExpectation(AppendExecution(AppendPreparation(new StringBuilder()))).ToString();
            Assert.True(_presenter.Query(presentation), presentation);
            break;
          }
        case PresentationType.Grouped:
          {
            string preparation = AppendPreparation(new StringBuilder()).ToString();
            If.Any(_preparations, () => Assert.True(_presenter.Query(preparation), preparation));
            string execution = AppendExecution(new StringBuilder()).ToString();
            If.Any(_executions, () => Assert.True(_presenter.Query(execution), execution));
            string expectation = AppendExpectation(new StringBuilder()).ToString();
            If.Any(_expectations, () => Assert.True(_presenter.Query(expectation), expectation));
            break;
          }
        case PresentationType.SingleStep:
          {
            _preparations.ForEach(prepare => Assert.True(_presenter.Query(prepare), prepare));
            _executions.ForEach(execution => Assert.True(_presenter.Query(execution), execution));
            _expectations.ForEach(expectation => Assert.True(_presenter.Query(expectation), expectation));
            break;
          }
        default:
          throw new ArgumentOutOfRangeException();
      }
    }

    private StringBuilder AppendPreparation(StringBuilder builder)
    {
      if (_preparations.Any())
      {
        builder.AppendLine("Prepare:");
        builder.AppendLine("========");
        _preparations.For((index, prepare) => builder.AppendLine(String.Format("{0}. {1}", index, prepare)));
        builder.AppendLine();
      }

      return builder;
    }

    private StringBuilder AppendExecution(StringBuilder builder)
    {
      if (_executions.Any())
      {
        builder.AppendLine("Execute:");
        builder.AppendLine("========");
        _executions.For((index, exec) => builder.AppendLine(String.Format("{0}. {1}", index, exec)));
        builder.AppendLine();
      }

      return builder;
    }

    private StringBuilder AppendExpectation(StringBuilder builder)
    {
      if (_expectations.Any())
      {
        builder.AppendLine("Verify:");
        builder.AppendLine("=======");
        _expectations.For((index, expects) => builder.AppendLine(String.Format("{0}. {1}", index, expects)));
        builder.AppendLine();
      }

      return builder;
    }
  }
}