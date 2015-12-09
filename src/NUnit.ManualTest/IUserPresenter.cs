namespace NUnit.ManualTest
{
  public interface IUserPresenter
  {
    void Show(string message);
    bool Query(string question);
  }
}