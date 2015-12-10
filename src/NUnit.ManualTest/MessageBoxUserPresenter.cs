using System.Reflection;
using System.Windows.Forms;

namespace NUnit.ManualTest
{
  public class MessageBoxUserPresenter : IUserPresenter
  {
    public bool Query(string message)
    {
      return MessageBox.Show(message, Assembly.GetExecutingAssembly().GetName().Name, MessageBoxButtons.YesNo) == DialogResult.Yes;
    }
  }
}