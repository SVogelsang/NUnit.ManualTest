using System.Diagnostics;

namespace NUnit.ManualTest.Tests
{
  public class FakeUserPresenter : IUserPresenter
  {
    public bool Query(string question)
    {
      Trace.WriteLine(question);
      return true;
    }
  }
}