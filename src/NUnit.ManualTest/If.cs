using System;
using System.Collections.Generic;
using System.Linq;

namespace NUnit.ManualTest
{
  public static class If
  {
    public static void Any<T>(IEnumerable<T> @enum, Action doThis)
    {
      if(@enum.Any())
      {
        doThis();
      }
    }
  }
}