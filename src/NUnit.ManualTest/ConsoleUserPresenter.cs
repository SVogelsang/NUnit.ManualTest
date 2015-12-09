using System;

namespace NUnit.ManualTest
{
  public class ConsoleUserPresenter : IUserPresenter
  {
    public void Show(string message)
    {
      Console.WriteLine(message);
      Console.WriteLine("Press enter to continue...");
      Console.ReadLine();
    }

    public bool Query(string question)
    {
      Console.WriteLine(question);
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