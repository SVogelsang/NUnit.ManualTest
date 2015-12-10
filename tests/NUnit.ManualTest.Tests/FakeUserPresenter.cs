using System.Diagnostics;

namespace NUnit.ManualTest.Tests
{
  public class FakeUserPresenter : IUserPresenter
  {
    public bool Query(string message)
    {
      Trace.WriteLine(message);
      return true;
    }
  }
}