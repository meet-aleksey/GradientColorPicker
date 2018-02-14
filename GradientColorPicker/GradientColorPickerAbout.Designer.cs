namespace System.Windows.Forms
{
  using System.Diagnostics.CodeAnalysis;

  partial class GradientColorPickerAbout
  {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    [SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", Justification = "Reviewed.")]
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      System.Windows.Forms.GradientColorPickerItem gradientColorPickerItem25 = new System.Windows.Forms.GradientColorPickerItem();
      System.Windows.Forms.GradientColorPickerItem gradientColorPickerItem26 = new System.Windows.Forms.GradientColorPickerItem();
      System.Windows.Forms.GradientColorPickerItem gradientColorPickerItem27 = new System.Windows.Forms.GradientColorPickerItem();
      this.label1 = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.linkLabel1 = new System.Windows.Forms.LinkLabel();
      this.linkLabel2 = new System.Windows.Forms.LinkLabel();
      this.linkLabel3 = new System.Windows.Forms.LinkLabel();
      this.label3 = new System.Windows.Forms.Label();
      this.label4 = new System.Windows.Forms.Label();
      this.Ok = new System.Windows.Forms.Button();
      this.Eye = new System.Windows.Forms.PictureBox();
      this.Logo = new System.Windows.Forms.GradientColorPicker();
      this.EyeGradientColorPicker = new System.Windows.Forms.GradientColorPicker();
      this.timer1 = new System.Windows.Forms.Timer(this.components);
      this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
      this.Version = new System.Windows.Forms.Label();
      this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
      this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
      ((System.ComponentModel.ISupportInitialize)(this.Eye)).BeginInit();
      this.tableLayoutPanel1.SuspendLayout();
      this.tableLayoutPanel2.SuspendLayout();
      this.flowLayoutPanel1.SuspendLayout();
      this.SuspendLayout();
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(3, 0);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(101, 13);
      this.label1.TabIndex = 0;
      this.label1.Text = "GradientColorPicker";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(3, 38);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(71, 13);
      this.label2.TabIndex = 0;
      this.label2.Text = "Meet Aleksey";
      // 
      // linkLabel1
      // 
      this.linkLabel1.AutoSize = true;
      this.linkLabel1.Location = new System.Drawing.Point(3, 63);
      this.linkLabel1.Name = "linkLabel1";
      this.linkLabel1.Size = new System.Drawing.Size(162, 13);
      this.linkLabel1.TabIndex = 0;
      this.linkLabel1.TabStop = true;
      this.linkLabel1.Text = "https://github.com/meet-aleksey";
      this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_LinkClicked);
      // 
      // linkLabel2
      // 
      this.linkLabel2.AutoSize = true;
      this.linkLabel2.Location = new System.Drawing.Point(3, 89);
      this.linkLabel2.Name = "linkLabel2";
      this.linkLabel2.Size = new System.Drawing.Size(180, 13);
      this.linkLabel2.TabIndex = 2;
      this.linkLabel2.TabStop = true;
      this.linkLabel2.Text = "https://medium.com/@meet.aleksey";
      this.linkLabel2.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_LinkClicked);
      // 
      // linkLabel3
      // 
      this.linkLabel3.AutoSize = true;
      this.linkLabel3.Location = new System.Drawing.Point(3, 76);
      this.linkLabel3.Name = "linkLabel3";
      this.linkLabel3.Size = new System.Drawing.Size(158, 13);
      this.linkLabel3.TabIndex = 1;
      this.linkLabel3.TabStop = true;
      this.linkLabel3.Text = "https://twitter.com/meetaleksey";
      this.linkLabel3.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_LinkClicked);
      // 
      // label3
      // 
      this.label3.AutoSize = true;
      this.label3.Location = new System.Drawing.Point(3, 114);
      this.label3.Name = "label3";
      this.label3.Size = new System.Drawing.Size(116, 13);
      this.label3.TabIndex = 0;
      this.label3.Text = "The MIT License (MIT)";
      // 
      // label4
      // 
      this.label4.AutoSize = true;
      this.label4.Location = new System.Drawing.Point(3, 127);
      this.label4.Name = "label4";
      this.label4.Size = new System.Drawing.Size(169, 13);
      this.label4.TabIndex = 0;
      this.label4.Text = "Copyright © 2018, @meet-aleksey";
      // 
      // Ok
      // 
      this.Ok.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.Ok.Location = new System.Drawing.Point(205, 3);
      this.Ok.Name = "Ok";
      this.Ok.Size = new System.Drawing.Size(75, 23);
      this.Ok.TabIndex = 3;
      this.Ok.Text = "Ok";
      this.Ok.UseVisualStyleBackColor = true;
      this.Ok.Click += new System.EventHandler(this.Ok_Click);
      // 
      // Eye
      // 
      this.Eye.Location = new System.Drawing.Point(0, 76);
      this.Eye.Margin = new System.Windows.Forms.Padding(0);
      this.Eye.Name = "Eye";
      this.Eye.Size = new System.Drawing.Size(64, 64);
      this.Eye.TabIndex = 3;
      this.Eye.TabStop = false;
      this.Eye.Paint += new System.Windows.Forms.PaintEventHandler(this.Eye_Paint);
      // 
      // Logo
      // 
      this.Logo.AlwaysShowAddBox = false;
      this.Logo.AutoScrollMargin = new System.Drawing.Size(0, 0);
      this.Logo.AutoScrollMinSize = new System.Drawing.Size(0, 0);
      this.Logo.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
      this.Logo.BackColor = System.Drawing.Color.White;
      this.Logo.ColorItemBorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      gradientColorPickerItem25.Color = System.Drawing.Color.Red;
      gradientColorPickerItem25.Position = 0F;
      gradientColorPickerItem26.Color = System.Drawing.Color.Green;
      gradientColorPickerItem26.Position = 0.5F;
      gradientColorPickerItem27.Color = System.Drawing.Color.Blue;
      gradientColorPickerItem27.Position = 1F;
      this.Logo.Colors.Add(gradientColorPickerItem25);
      this.Logo.Colors.Add(gradientColorPickerItem26);
      this.Logo.Colors.Add(gradientColorPickerItem27);
      this.Logo.Cursor = System.Windows.Forms.Cursors.Hand;
      this.Logo.Enabled = false;
      this.Logo.GradientLayoutSize = 36;
      this.Logo.GrayscaleWhenDisabled = false;
      this.Logo.Location = new System.Drawing.Point(0, 0);
      this.Logo.Margin = new System.Windows.Forms.Padding(0);
      this.Logo.Name = "Logo";
      this.Logo.Padding = new System.Windows.Forms.Padding(4);
      this.Logo.Size = new System.Drawing.Size(64, 64);
      this.Logo.TabIndex = 8;
      // 
      // EyeGradientColorPicker
      // 
      this.EyeGradientColorPicker.AlwaysShowAddBox = false;
      this.EyeGradientColorPicker.AutoScrollMargin = new System.Drawing.Size(0, 0);
      this.EyeGradientColorPicker.AutoScrollMinSize = new System.Drawing.Size(0, 0);
      this.EyeGradientColorPicker.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
      this.EyeGradientColorPicker.Cursor = System.Windows.Forms.Cursors.Hand;
      this.EyeGradientColorPicker.Location = new System.Drawing.Point(11, 224);
      this.EyeGradientColorPicker.Name = "EyeGradientColorPicker";
      this.EyeGradientColorPicker.Size = new System.Drawing.Size(64, 31);
      this.EyeGradientColorPicker.TabIndex = 9;
      this.EyeGradientColorPicker.Visible = false;
      // 
      // timer1
      // 
      this.timer1.Enabled = true;
      this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
      // 
      // tableLayoutPanel1
      // 
      this.tableLayoutPanel1.ColumnCount = 1;
      this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel1.Controls.Add(this.Version, 0, 1);
      this.tableLayoutPanel1.Controls.Add(this.label2, 0, 3);
      this.tableLayoutPanel1.Controls.Add(this.linkLabel1, 0, 5);
      this.tableLayoutPanel1.Controls.Add(this.linkLabel3, 0, 6);
      this.tableLayoutPanel1.Controls.Add(this.label4, 0, 10);
      this.tableLayoutPanel1.Controls.Add(this.linkLabel2, 0, 7);
      this.tableLayoutPanel1.Controls.Add(this.label3, 0, 9);
      this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
      this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.tableLayoutPanel1.Location = new System.Drawing.Point(68, 4);
      this.tableLayoutPanel1.Name = "tableLayoutPanel1";
      this.tableLayoutPanel1.RowCount = 12;
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 12F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 12F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 12F));
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
      this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel1.Size = new System.Drawing.Size(283, 205);
      this.tableLayoutPanel1.TabIndex = 10;
      // 
      // Version
      // 
      this.Version.AutoSize = true;
      this.Version.Location = new System.Drawing.Point(3, 13);
      this.Version.Name = "Version";
      this.Version.Size = new System.Drawing.Size(97, 13);
      this.Version.TabIndex = 11;
      this.Version.Text = "Version: ${Version}";
      // 
      // tableLayoutPanel2
      // 
      this.tableLayoutPanel2.ColumnCount = 1;
      this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel2.Controls.Add(this.Eye, 0, 2);
      this.tableLayoutPanel2.Controls.Add(this.Logo, 0, 0);
      this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Left;
      this.tableLayoutPanel2.Location = new System.Drawing.Point(4, 4);
      this.tableLayoutPanel2.Name = "tableLayoutPanel2";
      this.tableLayoutPanel2.RowCount = 4;
      this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 64F));
      this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 12F));
      this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 64F));
      this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
      this.tableLayoutPanel2.Size = new System.Drawing.Size(64, 234);
      this.tableLayoutPanel2.TabIndex = 11;
      // 
      // flowLayoutPanel1
      // 
      this.flowLayoutPanel1.AutoSize = true;
      this.flowLayoutPanel1.Controls.Add(this.Ok);
      this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
      this.flowLayoutPanel1.Location = new System.Drawing.Point(68, 209);
      this.flowLayoutPanel1.Name = "flowLayoutPanel1";
      this.flowLayoutPanel1.Size = new System.Drawing.Size(283, 29);
      this.flowLayoutPanel1.TabIndex = 12;
      // 
      // GradientColorPickerAbout
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.CancelButton = this.Ok;
      this.ClientSize = new System.Drawing.Size(355, 242);
      this.Controls.Add(this.tableLayoutPanel1);
      this.Controls.Add(this.flowLayoutPanel1);
      this.Controls.Add(this.tableLayoutPanel2);
      this.Controls.Add(this.EyeGradientColorPicker);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "GradientColorPickerAbout";
      this.Padding = new System.Windows.Forms.Padding(4);
      this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
      this.Text = "About GradientColorPicker Control";
      this.Load += new System.EventHandler(this.GradientColorPickerAbout_Load);
      ((System.ComponentModel.ISupportInitialize)(this.Eye)).EndInit();
      this.tableLayoutPanel1.ResumeLayout(false);
      this.tableLayoutPanel1.PerformLayout();
      this.tableLayoutPanel2.ResumeLayout(false);
      this.flowLayoutPanel1.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private Label label1;
    private Label label2;
    private LinkLabel linkLabel1;
    private LinkLabel linkLabel2;
    private LinkLabel linkLabel3;
    private Label label3;
    private Label label4;
    private Button Ok;
    private PictureBox Eye;
    private GradientColorPicker Logo;
    private GradientColorPicker EyeGradientColorPicker;
    private Timer timer1;
    private TableLayoutPanel tableLayoutPanel1;
    private Label Version;
    private TableLayoutPanel tableLayoutPanel2;
    private FlowLayoutPanel flowLayoutPanel1;
  }
}