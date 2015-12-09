using System.Reflection;
using System.Windows.Forms;

namespace NUnit.ManualTest
{
  public class MessageBoxUserPresenter : IUserPresenter
  {
    public bool Query(string question)
    {
      return MessageBox.Show(question, Assembly.GetExecutingAssembly().GetName().Name, MessageBoxButtons.YesNo) == DialogResult.Yes;
    }
  }
}