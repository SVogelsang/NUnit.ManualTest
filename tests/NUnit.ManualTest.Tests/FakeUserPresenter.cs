using System.Diagnostics;

namespace NUnit.ManualTest.Tests
{
  public class FakeUserPresenter : IUserPresenter
  {
    public void Show(string message)
    {
      Trace.WriteLine(message);
    }

    public bool Query(string question)
    {
      Trace.WriteLine(question);
      return true;
    }
  }
}