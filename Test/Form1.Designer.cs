namespace Test
{
  partial class Form1
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

    #region Код, автоматически созданный конструктором форм Windows

    /// <summary>
    /// Требуемый метод для поддержки конструктора — не изменяйте 
    /// содержимое этого метода с помощью редактора кода.
    /// </summary>
    private void InitializeComponent()
    {
      this.AddColor = new System.Windows.Forms.Button();
      this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
      this.GradientColorPicker1 = new System.Windows.Forms.GradientColorPicker();
      this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
      this.RemoveColor = new System.Windows.Forms.Button();
      this.SelectColor = new System.Windows.Forms.Button();
      this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
      this.EvenlyAlignColors = new System.Windows.Forms.Button();
      this.RandomizeColors = new System.Windows.Forms.Button();
      this.InvertColors = new System.Windows.Forms.Button();
      this.AnglePicker = new System.Windows.Forms.CircleAnglePicker();
      this.GradientCanvas = new System.Windows.Forms.PictureBox();
      this.ColorDialog = new System.Windows.Forms.ColorDialog();
      this.tableLayoutPanel1.SuspendLayout();
      this.flowLayoutPanel1.SuspendLayout();
      this.flowLayoutPanel2.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.GradientCanvas)).BeginInit();
      this.SuspendLayout();
      // 
      // AddColor
      // 
      this.AddColor.Location = new System.Drawing.Point(3, 3);
      this.AddColor.Name = "AddColor";
      this.AddColor.Size = new System.Drawing.Size(75, 23);
      this.AddColor.TabIndex = 1;
      this.AddColor.Text = "Add";
      this.AddColor.UseVisualStyleBackColor = true;
      this.AddColor.Click += new System.EventHandler(this.AddColor_Click);
      // 
      // tableLayoutPanel1
      // 
      this.tableLayoutPanel1.ColumnCount = 1;
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel1.Controls.Add(this.GradientColorPicker1, 0, 1);
      this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 2);
      this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel2, 0, 3);
      this.tableLayoutPanel1.Controls.Add(this.GradientCanvas, 0, 0);
      this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 4;
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 125F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel1.Size = new System.Drawing.Size(380, 238);
      this.tableLayoutPanel1.TabIndex = 2;
      // 
      // GradientColorPicker1
      // 
      this.GradientColorPicker1.AutoScrollMargin = new System.Drawing.Size(0, 0);
      this.GradientColorPicker1.AutoScrollMinSize = new System.Drawing.Size(0, 0);
      this.GradientColorPicker1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
      this.GradientColorPicker1.Cursor = System.Windows.Forms.Cursors.Hand;
      this.GradientColorPicker1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.GradientColorPicker1.Location = new System.Drawing.Point(3, 128);
      this.GradientColorPicker1.Name = "GradientColorPicker1";
      this.GradientColorPicker1.SelectedColor = null;
      this.GradientColorPicker1.Size = new System.Drawing.Size(374, 29);
      this.GradientColorPicker1.TabIndex = 0;
      this.GradientColorPicker1.ColorAdded += new System.EventHandler(this.GradientColorPicker1_EventsHandler);
      this.GradientColorPicker1.ColorChanged += new System.EventHandler(this.GradientColorPicker1_EventsHandler);
      this.GradientColorPicker1.ColorMoved += new System.EventHandler(this.GradientColorPicker1_EventsHandler);
      this.GradientColorPicker1.ColorRemoved += new System.EventHandler(this.GradientColorPicker1_EventsHandler);
      // 
      // flowLayoutPanel1
      // 
      this.flowLayoutPanel1.AutoSize = true;
      this.flowLayoutPanel1.Controls.Add(this.AddColor);
      this.flowLayoutPanel1.Controls.Add(this.RemoveColor);
      this.flowLayoutPanel1.Controls.Add(this.SelectColor);
      this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 163);
      this.flowLayoutPanel1.Name = "flowLayoutPanel1";
      this.flowLayoutPanel1.Size = new System.Drawing.Size(374, 29);
      this.flowLayoutPanel1.TabIndex = 1;
      this.flowLayoutPanel1.WrapContents = false;
      // 
      // RemoveColor
      // 
      this.RemoveColor.Location = new System.Drawing.Point(84, 3);
      this.RemoveColor.Name = "RemoveColor";
      this.RemoveColor.Size = new System.Drawing.Size(75, 23);
      this.RemoveColor.TabIndex = 1;
      this.RemoveColor.Text = "Remove";
      this.RemoveColor.UseVisualStyleBackColor = true;
      this.RemoveColor.Click += new System.EventHandler(this.RemoveColor_Click);
      // 
      // SelectColor
      // 
      this.SelectColor.Location = new System.Drawing.Point(165, 3);
      this.SelectColor.Name = "SelectColor";
      this.SelectColor.Size = new System.Drawing.Size(75, 23);
      this.SelectColor.TabIndex = 2;
      this.SelectColor.Text = "Select color";
      this.SelectColor.UseVisualStyleBackColor = true;
      this.SelectColor.Click += new System.EventHandler(this.SelectColor_Click);
      // 
      // flowLayoutPanel2
      // 
      this.flowLayoutPanel2.AutoSize = true;
      this.flowLayoutPanel2.Controls.Add(this.EvenlyAlignColors);
      this.flowLayoutPanel2.Controls.Add(this.RandomizeColors);
      this.flowLayoutPanel2.Controls.Add(this.InvertColors);
      this.flowLayoutPanel2.Controls.Add(this.AnglePicker);
      this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
      this.flowLayoutPanel2.Location = new System.Drawing.Point(3, 198);
      this.flowLayoutPanel2.Name = "flowLayoutPanel2";
      this.flowLayoutPanel2.Size = new System.Drawing.Size(374, 37);
      this.flowLayoutPanel2.TabIndex = 3;
      this.flowLayoutPanel2.WrapContents = false;
      // 
      // EvenlyAlignColors
      // 
      this.EvenlyAlignColors.Location = new System.Drawing.Point(3, 3);
      this.EvenlyAlignColors.Name = "EvenlyAlignColors";
      this.EvenlyAlignColors.Size = new System.Drawing.Size(75, 23);
      this.EvenlyAlignColors.TabIndex = 4;
      this.EvenlyAlignColors.Text = "Evenly";
      this.EvenlyAlignColors.UseVisualStyleBackColor = true;
      this.EvenlyAlignColors.Click += new System.EventHandler(this.EvenlyAlignColors_Click);
      // 
      // RandomizeColors
      // 
      this.RandomizeColors.Location = new System.Drawing.Point(84, 3);
      this.RandomizeColors.Name = "RandomizeColors";
      this.RandomizeColors.Size = new System.Drawing.Size(75, 23);
      this.RandomizeColors.TabIndex = 3;
      this.RandomizeColors.Text = "Randomize";
      this.RandomizeColors.UseVisualStyleBackColor = true;
      this.RandomizeColors.Click += new System.EventHandler(this.RandomizeColors_Click);
      // 
      // InvertColors
      // 
      this.InvertColors.Location = new System.Drawing.Point(165, 3);
      this.InvertColors.Name = "InvertColors";
      this.InvertColors.Size = new System.Drawing.Size(75, 23);
      this.InvertColors.TabIndex = 4;
      this.InvertColors.Text = "Invert";
      this.InvertColors.UseVisualStyleBackColor = true;
      this.InvertColors.Click += new System.EventHandler(this.InvertColors_Click);
      // 
      // AnglePicker
      // 
      this.AnglePicker.AutoScrollMargin = new System.Drawing.Size(0, 0);
      this.AnglePicker.AutoScrollMinSize = new System.Drawing.Size(0, 0);
      this.AnglePicker.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
      this.AnglePicker.Location = new System.Drawing.Point(246, 3);
      this.AnglePicker.Name = "AnglePicker";
      this.AnglePicker.Size = new System.Drawing.Size(23, 23);
      this.AnglePicker.TabIndex = 5;
      this.AnglePicker.ValueChanged += new System.EventHandler(this.AnglePicker_ValueChanged);
      // 
      // GradientCanvas
      // 
      this.GradientCanvas.Dock = System.Windows.Forms.DockStyle.Fill;
      this.GradientCanvas.Location = new System.Drawing.Point(3, 3);
      this.GradientCanvas.Name = "GradientCanvas";
      this.GradientCanvas.Size = new System.Drawing.Size(374, 119);
      this.GradientCanvas.TabIndex = 2;
      this.GradientCanvas.TabStop = false;
      this.GradientCanvas.Paint += new System.Windows.Forms.PaintEventHandler(this.GradientCanvas_Paint);
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(380, 238);
      this.Controls.Add(this.tableLayoutPanel1);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.MaximizeBox = false;
      this.Name = "Form1";
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "GradientColorPicker Test";
      this.Load += new System.EventHandler(this.Form1_Load);
      this.tableLayoutPanel1.ResumeLayout(false);
      this.tableLayoutPanel1.PerformLayout();
      this.flowLayoutPanel1.ResumeLayout(false);
      this.flowLayoutPanel2.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.GradientCanvas)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.GradientColorPicker GradientColorPicker1;
    private System.Windows.Forms.Button AddColor;
    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    private System.Windows.Forms.Button RemoveColor;
    private System.Windows.Forms.Button SelectColor;
    private System.Windows.Forms.PictureBox GradientCanvas;
    private System.Windows.Forms.ColorDialog ColorDialog;
    private System.Windows.Forms.Button RandomizeColors;
    private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
    private System.Windows.Forms.Button InvertColors;
    private System.Windows.Forms.Button EvenlyAlignColors;
    private System.Windows.Forms.CircleAnglePicker AnglePicker;
  }
}

