using System;
using System.Reflection;

namespace NUnit.ManualTest
{
  [AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class)]
  public class UserPresenterAttribute : Attribute
  {
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

    static public IUserPresenter CreatePresenter(Type type)
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