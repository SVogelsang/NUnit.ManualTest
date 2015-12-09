using System;
using System.Collections.Generic;

namespace NUnit.ManualTest
{
  public static class EnumerableExtensions
  {
    static public void For<T>(this IEnumerable<T> @this, Action<int, T> action)
    {
      int index = 0;
      foreach (T item in @this)
      {
        action(index++, item);
      }
    }
  }
}