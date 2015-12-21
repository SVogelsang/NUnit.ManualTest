using System;
using System.Reflection;

namespace NUnit.ManualTest
{
  /// <summary>
  /// The attribute can be used to specify the user presenter to used in tests.
  /// </summary>
  [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class)]
  public class UserPresenterAttribute : Attribute
  {
    private readonly Type _presenterType;

    /// <summary>
    /// Create new instance.
    /// </summary>
    /// <param name="presenterType">The type of presenter to be instantiated.</param>
    /// <exception cref="ArgumentException"></exception>
    public UserPresenterAttribute(Type presenterType)
    {
      if (!typeof(IUserPresenter).IsAssignableFrom(presenterType))
      {
        throw new ArgumentException("Presenter must be IUserPresenter");
      }
      _presenterType = presenterType;
    }

    /// <summary>
    /// The type of presenter to be instantiated.
    /// </summary>
    public Type PresenterType
    {
      get
      {
        return _presenterType;
      }
    }

    /// <summary>
    /// Creates an instance of the presenter.
    /// </summary>
    /// <returns>The presenter instance.</returns>
    public IUserPresenter Create()
    {
      return (IUserPresenter)Activator.CreateInstance(_presenterType);
    }

    /// <summary>
    /// Creates an instance of the presenter by searching the passed type to be attributed with fallback to assmbly attribute or globally default presenter type.
    /// </summary>
    /// <param name="type">The type (test fixture).</param>
    /// <returns>The presenter instance.</returns>
    public static IUserPresenter CreatePresenter(Type type)
    {
      var attribute = FindAttribute(type);
      return attribute != null ? attribute.Create() : new ConsoleUserPresenter();
    }

    private static UserPresenterAttribute FindAttribute(Type type)
    {
      UserPresenterAttribute attribute = type.GetCustomAttribute<UserPresenterAttribute>(true);
      
      if (attribute != null)
      {
        return attribute;
      }

      return type.Assembly.GetCustomAttribute<UserPresenterAttribute>();
    }
  }
}