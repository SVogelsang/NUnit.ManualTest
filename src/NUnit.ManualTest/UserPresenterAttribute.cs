using System;
using System.Diagnostics;
using System.Reflection;
using NUnit.Framework;

namespace NUnit.ManualTest
{
  [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class)]
  public class UserPresenterAttribute : Attribute
  {
    private static class StackWalker
    {
      public static T WalkToAndGet<T>(Func<Type, bool> predicate, Func<Type, T> getter)
      {
        int frameOffset = 4;

        while (true)
        {
          StackFrame frame = new StackFrame(frameOffset);
          if (frame.GetMethod() == null)
          {
            return default(T);
          }

          Type current = frame.GetMethod().DeclaringType;
          if (predicate(current))
          {
            return getter(current);
          }

          frameOffset++;
        }
      }
    }

    static private class AssemblyDefinedUserPresenter
    {
      private static UserPresenterAttribute _attribute;
      private static bool _alreadySearched;

      static public UserPresenterAttribute Get()
      {
        if (!_alreadySearched && _attribute == null)
        {
          _attribute = StackWalker.WalkToAndGet(IsTestFixture, type => type.Assembly.GetCustomAttribute<UserPresenterAttribute>());
          _alreadySearched = true;
        }
        return _attribute;
      }
    }

    private readonly Type _presenterType;

    public UserPresenterAttribute(Type presenterType)
    {
      if (!typeof(IUserPresenter).IsAssignableFrom(presenterType))
      {
        throw new ArgumentException("Presenter must be IUserPresenter");
      }
      _presenterType = presenterType;
    }

    public Type PresenterType
    {
      get
      {
        return _presenterType;
      }
    }

    public IUserPresenter Create()
    {
      return (IUserPresenter)Activator.CreateInstance(_presenterType);
    }

    static public IUserPresenter CreatePresenter()
    {
      var attribute = FindAttribute();
      return attribute != null ? attribute.Create() : new MessageBoxUserPresenter();
    }

    private static UserPresenterAttribute FindAttribute()
    {
      UserPresenterAttribute attribute;
      if (TryFindOnTestClass(out attribute))
      {
        return attribute;
      }

      return AssemblyDefinedUserPresenter.Get();
    }

    private static bool TryFindOnTestClass(out UserPresenterAttribute attribute)
    {
      attribute = StackWalker.WalkToAndGet(IsTestFixture, type => type.GetCustomAttribute<UserPresenterAttribute>());
      return attribute != null;
    }

    private static bool IsTestFixture(Type type)
    {
      return type.GetCustomAttribute<TestFixtureAttribute>() != null;
    }
  }
}