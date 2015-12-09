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
    private bool _singleUserInteraction;

    public ManualTestBuilder(IUserPresenter presenter)
    {
      _presenter = presenter;
    }

    public ManualTestBuilder AsingleUserInteraction()
    {
      _singleUserInteraction = true;
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
      if (_singleUserInteraction)
      {
        _preparations.ForEach(prepare => _presenter.Show(prepare));
        _executions.ForEach(exec => _presenter.Show(exec));
        Assert.True(_expectations.TrueForAll(expects => _presenter.Query(expects)));
      }
      else
      {
        Assert.True(_presenter.Query(MakeUserPresentation()));
      }
    }

    private string MakeUserPresentation()
    {
      StringBuilder builder = new StringBuilder();

      if (_preparations.Any())
      {
        builder.AppendLine("Prepare:");
        builder.AppendLine("========");
        _preparations.For((index, prepare) => builder.AppendLine(String.Format("{0}. {1}", index, prepare)));
        builder.AppendLine();
      }

      if (_executions.Any())
      {
        builder.AppendLine("Execute:");
        builder.AppendLine("========");
        _executions.For((index, exec) => builder.AppendLine(String.Format("{0}. {1}", index, exec)));
        builder.AppendLine();
      }

      if (_expectations.Any())
      {
        builder.AppendLine("Verify:");
        builder.AppendLine("=======");
        _expectations.For((index, expects) => builder.AppendLine(String.Format("{0}. {1}", index, expects)));
        builder.AppendLine();
      }

      return builder.ToString();
    }
  }
}