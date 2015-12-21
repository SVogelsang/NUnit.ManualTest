using System;

namespace NUnit.ManualTest
{
  /// <summary>
  /// A preasenter for getting feedback through console.
  /// </summary>
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
          if (line.ToLower() == "yes" | line.ToLower() == "y")
          {
            return true;
          }
          if (line.ToLower() == "no" || line.ToLower() == "n")
          {
            return false;
          }
        }
      } while (true);
    }
  }
}