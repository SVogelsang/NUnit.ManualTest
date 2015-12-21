using System.Reflection;
using System.Windows.Forms;

namespace NUnit.ManualTest
{
  /// <summary>
  /// A preasenter for getting feedback through WinForm message box.
  /// </summary>
  public class MessageBoxUserPresenter : IUserPresenter
  {
    /// <inheritdoc/>
    public bool Query(string message)
    {
      return MessageBox.Show(message, Assembly.GetExecutingAssembly().GetName().Name, MessageBoxButtons.YesNo) == DialogResult.Yes;
    }
  }
}