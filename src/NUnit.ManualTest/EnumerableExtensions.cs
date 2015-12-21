using System;
using System.Collections.Generic;

namespace NUnit.ManualTest
{
  /// <summary>
  /// Extension methods for <see cref="IEnumerable{T}"/>
  /// </summary>
  public static class EnumerableExtensions
  {
    /// <summary>
    /// Iterates over an enumerable using a for loop.
    /// </summary>
    /// <param name="this">The enumerable.</param>
    /// <param name="action">The action to be called for each item.</param>
    /// <typeparam name="T">Any type.</typeparam>
    public static void For<T>(this IEnumerable<T> @this, Action<int, T> action)
    {
      int index = 0;
      foreach (T item in @this)
      {
        action(index++, item);
      }
    }
  }
}