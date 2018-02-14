using System;
using System.Drawing;
using System.Windows.Forms;

namespace Test
{
  public partial class Form1 : Form
  {
    public Form1()
    {
      InitializeComponent();
    }

    private void Form1_Load(object sender, EventArgs e)
    {
      Check();
    }

    private void AddColor_Click(object sender, EventArgs e)
    {
      var rnd = new Random(DateTime.Now.Millisecond);
      var randomColor = Color.FromArgb(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255));
      var randomPosition = (float)rnd.NextDouble();

      GradientColorPicker1.AddColor(randomColor, randomPosition).SelectNextColor();
      Check();
    }

    private void GradientCanvas_Paint(object sender, PaintEventArgs e)
    {
      GradientColorPicker1.DrawLinearGradient(e.Graphics, GradientCanvas.ClientRectangle, AnglePicker.Value);
    }

    private void RemoveColor_Click(object sender, EventArgs e)
    {
      GradientColorPicker1.RemoveSelectedColor().SelectNextColor();
      Check();
    }

    private void SelectColor_Click(object sender, EventArgs e)
    {
      if (GradientColorPicker1.SelectedColor != null && ColorDialog.ShowDialog() == DialogResult.OK)
      {
        GradientColorPicker1.SelectedColor.Color = ColorDialog.Color;
      }
    }

    private void RandomizeColors_Click(object sender, EventArgs e)
    {
      GradientColorPicker1.Randomize(true, true);
    }

    private void InvertColors_Click(object sender, EventArgs e)
    {
      GradientColorPicker1.InvertColors();
    }

    private void EvenlyAlignColors_Click(object sender, EventArgs e)
    {
      GradientColorPicker1.EvenlyAlignColors();
    }

    private void AnglePicker_ValueChanged(object sender, EventArgs e)
    {
      GradientCanvas.Refresh();
    }

    private void GradientColorPicker1_EventsHandler(object sender, EventArgs e)
    {
      GradientCanvas.Refresh();
      Check();
    }

    private void Check()
    {
      RemoveColor.Enabled = SelectColor.Enabled =
      RandomizeColors.Enabled = InvertColors.Enabled =
      EvenlyAlignColors.Enabled = GradientColorPicker1.Colors.Count > 0;
    }
  }
}
