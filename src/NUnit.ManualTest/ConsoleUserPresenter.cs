using System;

namespace NUnit.ManualTest
{
  public class ConsoleUserPresenter : IUserPresenter
  {
    public bool Query(string message)
    {
      Console.WriteLine(message);
      Console.WriteLine("yes|no?");

      do
      {
        string line = Console.ReadLine();
        if (!string.IsNullOrEmpty(line))
        {
          if (line.ToLower() == "yes")
          {
            return true;
          }
          if (line.ToLower() == "no")
          {
            return false;
          }
        }
      } while (true);
    }
  }
}