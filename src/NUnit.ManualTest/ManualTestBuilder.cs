using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace NUnit.ManualTest
{
  /// <summary>
  /// The builder for building the test scenarios.
  /// </summary>
  public class ManualTestBuilder
  {
    private readonly IUserPresenter _presenter;
    private readonly List<string> _preparations = new List<string>();
    private readonly List<string> _executions = new List<string>();
    private readonly List<string> _expectations = new List<string>();

    /// <summary>
    /// Creates a new instance.
    /// </summary>
    /// <param name="presenter">The user presenter to be used for feedback.</param>
    /// <param name="presentationType">The presentation type to be used.</param>
    public ManualTestBuilder(IUserPresenter presenter, PresentationType presentationType = PresentationType.Once)
    {
      _presenter = presenter;
      PresentationType = presentationType;
    }

    /// <summary>
    /// Gets or sets the presentation type.
    /// </summary>
    public PresentationType PresentationType { get; set; }

    /// <summary>
    /// Use SingleStep <see cref="PresentationType"/>.
    /// </summary>
    /// <returns>this</returns>
    public ManualTestBuilder AsSingleStepUserInteraction()
    {
      PresentationType = PresentationType.SingleStep;
      return this;
    }

    /// <summary>
    /// Use Grouped <see cref="PresentationType"/>.
    /// </summary>
    /// <returns>this</returns>
    public ManualTestBuilder AsGroupedUserInteraction()
    {
      PresentationType = PresentationType.Grouped;
      return this;
    }

    /// <summary>
    /// Use Once <see cref="PresentationType"/>.
    /// </summary>
    /// <returns>this</returns>
    public ManualTestBuilder AsOneStepUserInteraction()
    {
      PresentationType = PresentationType.Once;
      return this;
    }

    /// <summary>
    /// Adds a preparation step.
    /// </summary>
    /// <param name="format">The message or format (<see cref="string.Format(string,object)"/>).</param>
    /// <param name="args">The format arguments.</param>
    /// <returns>this</returns>
    public ManualTestBuilder Prepare(string format, params object[] args)
    {
      _preparations.Add(String.Format(format, args));
      return this;
    }

    /// <summary>
    /// Adds an execution step.
    /// </summary>
    /// <param name="format">The message or format (<see cref="string.Format(string,object)"/>).</param>
    /// <param name="args">The format arguments.</param>
    /// <returns>this</returns>
    public ManualTestBuilder Do(string format, params object[] args)
    {
      _executions.Add(String.Format(format, args));
      return this;
    }

    /// <summary>
    /// Adds a verification step.
    /// </summary>
    /// <param name="format">The message or format (<see cref="string.Format(string,object)"/>).</param>
    /// <param name="args">The format arguments.</param>
    /// <returns>this</returns>
    public ManualTestBuilder Verify(string format, params object[] args)
    {
      _expectations.Add(String.Format(format, args));
      return this;
    }

    /// <summary>
    /// Executes the test by presenting the preparations/executions and verification steps to the tester. The first negative feedback will cause an <see cref="AssertionException"/>
    /// </summary>
    /// <exception cref="AssertionException">if the tester does not confirm one of the steps.</exception>
    public void Go()
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
            Any(_preparations, () => Assert.True(_presenter.Query(preparation), preparation));
            string execution = AppendExecution(new StringBuilder()).ToString();
            Any(_executions, () => Assert.True(_presenter.Query(execution), execution));
            string expectation = AppendExpectation(new StringBuilder()).ToString();
            Any(_expectations, () => Assert.True(_presenter.Query(expectation), expectation));
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

    private static void Any<T>(IEnumerable<T> @enum, Action doThis)
    {
      if (@enum.Any())
      {
        doThis();
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