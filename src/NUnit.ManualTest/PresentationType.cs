namespace NUnit.ManualTest
{
  /// <summary>
  /// The presentation type the tester is triggered for feedback.
  /// </summary>
  public enum PresentationType
  {
    /// <summary>Only one feedback for all steps.</summary>
    Once,
    /// <summary>Preparations, executions and verifications are presented seperately.</summary>
    Grouped,
    /// <summary>Every single step is presented seperately.</summary>
    SingleStep
  }
}