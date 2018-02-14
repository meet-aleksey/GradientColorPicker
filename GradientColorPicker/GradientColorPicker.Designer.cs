namespace System.Windows.Forms
{
  partial class GradientColorPicker
  {
    /// <summary>
    /// Обязательная переменная конструктора.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Освободить все используемые ресурсы.
    /// </summary>
    /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Код, автоматически созданный конструктором компонентов

    /// <summary>
    /// Требуемый метод для поддержки конструктора — не изменяйте 
    /// содержимое этого метода с помощью редактора кода.
    /// </summary>
    private void InitializeComponent()
    {
      this.ColorDialog = new System.Windows.Forms.ColorDialog();
      this.SuspendLayout();
      // 
      // GradientColorPicker
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Cursor = System.Windows.Forms.Cursors.Hand;
      this.DoubleBuffered = true;
      this.Name = "GradientColorPicker";
      this.Size = new System.Drawing.Size(175, 28);
      this.ResumeLayout(false);

    }

    #endregion

    private ColorDialog ColorDialog;
  }
}
