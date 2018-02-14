//-----------------------------------------------------------------------
// <copyright file="GradientColorPickerAbout.cs">
//   Copyright (c) Aleksey. All rights reserved.
// </copyright>
// <author>Aleksey</author>
//-----------------------------------------------------------------------
namespace System.Windows.Forms
{
  using System.Diagnostics;
  using System.Diagnostics.CodeAnalysis;
  using System.Drawing;
  using System.Threading;

  [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1601:PartialElementsMustBeDocumented", Justification = "Reviewed.")]
  public partial class GradientColorPickerAbout : Form
  {
    /// <summary>
    /// Indicates that the task is being processed by the timer.
    /// </summary>
    private int inTimerCallback = 0;

    /// <summary>
    /// Random number generator.
    /// </summary>
    private Random random;

    /// <summary>
    /// Initializes a new instance of the <see cref="GradientColorPickerAbout"/> class.
    /// </summary>
    public GradientColorPickerAbout()
    {
      InitializeComponent();
    }

    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed.")]
    private void Eye_Paint(object sender, PaintEventArgs e)
    {
      var centerPoint = new PointF(random.Next(10, 42), random.Next(10, 42));
      EyeGradientColorPicker.DrawRadialGradient(e.Graphics, Eye.ClientRectangle, centerPoint);
    }

    [SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", Justification = "Reviewed.")]
    [SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", Justification = "Reviewed.")]
    [SuppressMessage("Microsoft.Mobility", "CA1601:DoNotUseTimersThatPreventPowerStateChanges", Justification = "Reviewed.")]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed.")]
    private void GradientColorPickerAbout_Load(object sender, EventArgs e)
    {
      Version.Text = $"Version: v{typeof(GradientColorPicker).Assembly.GetName().Version}";

      random = new Random(DateTime.Now.Millisecond);

      Eye.Image = new Bitmap(Eye.ClientSize.Width, Eye.ClientSize.Height);

      EyeGradientColorPicker.AddColor(Color.White, 0.2631579f);
      EyeGradientColorPicker.AddColor(Color.FromArgb(255, 0, 95, 140), 0.4385965f);
      EyeGradientColorPicker.AddColor(Color.FromArgb(255, 0, 114, 168), 0.649122834f);
      EyeGradientColorPicker.AddColor(Color.Black, 0.78245616f);

#if DEBUG
      EyeGradientColorPicker.DrawColorPosition = false;
#endif

      timer1.Interval = 500;
      timer1_Tick(this, e);
    }

    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Reviewed.")]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed.")]
    private void linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      var link = sender as LinkLabel;
      Process.Start(link.Text);
    }

    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed.")]
    private void Ok_Click(object sender, EventArgs e)
    {
      Close();
    }

    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1300:ElementMustBeginWithUpperCaseLetter", Justification = "Reviewed.")]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed.")]
    private void timer1_Tick(object sender, EventArgs e)
    {
      if (Interlocked.Exchange(ref inTimerCallback, 1) != 0)
      {
        return;
      }

      Eye.Refresh();

      timer1.Interval = random.Next(500, 1000);

      Interlocked.Exchange(ref this.inTimerCallback, 0);
    }
  }
}
