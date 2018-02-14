//-----------------------------------------------------------------------
// <copyright file="GradientColorPicker.cs">
//   Copyright (c) Aleksey. All rights reserved.
// </copyright>
// <author>Aleksey</author>
//-----------------------------------------------------------------------
namespace System.Windows.Forms
{
  using System.ComponentModel;
  using System.Diagnostics.CodeAnalysis;
  using System.Drawing;
  using System.Drawing.Drawing2D;
  using System.Drawing.Imaging;
  using System.Linq;

  /// <summary>
  /// Represents the <see cref="GradientColorPicker" /> Control.
  /// </summary>
  [DefaultProperty("Colors")]
  [DefaultEvent("ColorSelected")]
  [ToolboxBitmap(typeof(GradientColorPicker), "GradientColorPickerIcon.bmp")]
  public partial class GradientColorPicker : UserControl
  {
    /// <summary>
    /// Defines the alwaysShowAddBox.
    /// </summary>
    private bool alwaysShowAddBox = false;

    /// <summary>
    /// Defines the colorItemArrowSize.
    /// </summary>
    private float colorItemArrowSize = 0.35f;

    /// <summary>
    /// Defines the colorItemBlockSize.
    /// </summary>
    private float colorItemBlockSize = 0.65f;

    /// <summary>
    /// Defines the colorItemBorderStyle.
    /// </summary>
    private BorderStyle colorItemBorderStyle = BorderStyle.Fixed3D;

    /// <summary>
    /// Defines the width of color item.
    /// </summary>
    private int colorItemWidth = 12;

    /// <summary>
    /// Defines the list of colors.
    /// </summary>
    private GradientColorPickerItemCollection colors;

    /// <summary>
    /// Defines the EE.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1306:FieldNamesMustBeginWithLowerCaseLetter", Justification = "Reviewed.")]
    private bool EE = false;

    /// <summary>
    /// Defines the EE2.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1306:FieldNamesMustBeginWithLowerCaseLetter", Justification = "Reviewed.")]
    private int EE2 = 0;

    /// <summary>
    /// Defines the EE3.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1306:FieldNamesMustBeginWithLowerCaseLetter", Justification = "Reviewed.")]
    private int EE3 = 0;

    /// <summary>
    /// Defines the EES.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1306:FieldNamesMustBeginWithLowerCaseLetter", Justification = "Reviewed.")]
    private string EES = string.Empty;

    /// <summary>
    /// Defines the gradientLayout.
    /// </summary>
    private GradientColorPickerLayout gradientLayout = GradientColorPickerLayout.FixedSize;

    /// <summary>
    /// Defines the gradientLayoutSize.
    /// </summary>
    private int gradientLayoutSize = 10;

    /// <summary>
    /// Defines the grayscaleWhenDisabled.
    /// </summary>
    private bool grayscaleWhenDisabled = true;

    /// <summary>
    /// Defines the isItemMoved.
    /// </summary>
    private bool isItemMoved = false;

    /// <summary>
    /// Defines the lastColor.
    /// </summary>
    private Color lastColor = Color.Black;

    /// <summary>
    /// Defines the lastMouseLocation.
    /// </summary>
    private Point lastMouseLocation;

    /// <summary>
    /// Defines the lastY.
    /// </summary>
    private int lastY = -1;

    /// <summary>
    /// Defines the maximumColorCount.
    /// </summary>
    private int maximumColorCount = 32;

    /// <summary>
    /// Defines the minimumColorCount.
    /// </summary>
    private int minimumColorCount = 0;

    /// <summary>
    /// Defines the selectedColor.
    /// </summary>
    private GradientColorPickerItem selectedColor = null;

    /// <summary>
    /// Defines the showTransparentBackground.
    /// </summary>
    private bool showTransparentBackground = true;

    /// <summary>
    /// Defines the transparentBackgroundColor1.
    /// </summary>
    private Color transparentBackgroundColor1 = Color.White;

    /// <summary>
    /// Defines the transparentBackgroundColor2.
    /// </summary>
    private Color transparentBackgroundColor2 = Color.Silver;

    /// <summary>
    /// Defines the transparentBackgroundSize.
    /// </summary>
    private int transparentBackgroundSize = 4;

    /// <summary>
    /// Initializes a new instance of the <see cref="GradientColorPicker"/> class.
    /// </summary>
    public GradientColorPicker()
    {
      colors = new GradientColorPickerItemCollection(this);
      colors.ColorChanged += OnColorChanged;

      AllowAddColorByClick = true;
      AllowColorDialog = true;
      AllowDragOutColor = true;
      AllowPickGradientColor = true;
      AllowToHandleKeys = true;
      AllowToHandleMouseWheel = true;
      ColorItemHeight = 18;
      NotifyOfExceedingLimit = true;

#if DEBUG
      DrawColorPosition = true;
#endif

      InitializeComponent();
    }

    /// <summary>
    /// Occurs when a color is added to the list.
    /// </summary>
    [Category("Color")]
    public event EventHandler ColorAdded;

    /// <summary>
    /// Occurs when the color of the item is changed.
    /// </summary>
    [Category("Color")]
    public event EventHandler ColorChanged;

    /// <summary>
    /// Occurs when a color is moved.
    /// </summary>
    [Category("Color")]
    public event EventHandler ColorMoved;

    /// <summary>
    /// Occurs when a color is removed from the list.
    /// </summary>
    [Category("Color")]
    public event EventHandler ColorRemoved;

    /// <summary>
    /// Occurs when a color is selected.
    /// </summary>
    [Category("Color")]
    public event EventHandler ColorSelected;

    /// <summary>
    /// Occurs when the allowed limit of the number of colors is exceeded.
    /// </summary>
    [Category("Color")]
    public event EventHandler LimitExceeded;

    /// <summary>
    /// Gets or sets a value indicating whether to allow a new color to be added with the mouse click.
    /// </summary>
    [Category("Behavior")]
    [Description("Indicates whether the user can add a new color with the mouse click.")]
    [DefaultValue(true)]
    public bool AllowAddColorByClick { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether allow to show the color dialog.
    /// </summary>
    [Category("Behavior")]
    [Description("Allows to enable or disable the show of the color dialog.")]
    [DefaultValue(true)]
    public bool AllowColorDialog { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether allow to drag out of the colors from the list.
    /// </summary>
    [Category("Behavior")]
    [Description("Indicates whether the user can to drag out colors from the list.")]
    [DefaultValue(true)]
    public bool AllowDragOutColor { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to allow the pick color from the gradient.
    /// </summary>
    [Category("Behavior")]
    [Description("Indicates whether the user can to pick color from the gradient.")]
    [DefaultValue(true)]
    public bool AllowPickGradientColor { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to allow processing of the default keyboard events.
    /// </summary>
    [Category("Behavior")]
    [Description("Enables or disables the processing of the default keyboard events.")]
    [DefaultValue(true)]
    public bool AllowToHandleKeys { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to allow processing the mouse wheel event.
    /// </summary>
    [Category("Behavior")]
    [Description("Enables or disables the processing of the mouse wheel event.")]
    [DefaultValue(true)]
    public bool AllowToHandleMouseWheel { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether to always shows the add box.
    /// </summary>
    [Category("Appearance")]
    [Description("Indicates whether the color add box should be displayed or not.")]
    [DefaultValue(false)]
    [SuppressMessage("StyleCop.CSharp.LayoutRules", "SA1516:ElementsMustBeSeparatedByBlankLine", Justification = "Reviewed.")]
    [SuppressMessage("StyleCop.CSharp.LayoutRules", "SA1513:ClosingCurlyBracketMustBeFollowedByBlankLine", Justification = "Reviewed.")]
    public bool AlwaysShowAddBox
    {
      get
      {
        return alwaysShowAddBox;
      }
      set
      {
        alwaysShowAddBox = value;

        Refresh();
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether AutoScroll.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public new bool AutoScroll { get; set; }

    /// <summary>
    /// Gets or sets the AutoScrollMargin.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public new Size AutoScrollMargin { get; set; }

    /// <summary>
    /// Gets or sets the AutoScrollMinSize.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public new Size AutoScrollMinSize { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether AutoSize.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public new bool AutoSize { get; set; }

    /// <summary>
    /// Gets or sets the AutoSizeMode.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [SuppressMessage("Microsoft.Naming", "CA1721:PropertyNamesShouldNotMatchGetMethods", Justification = "Reviewed.")]
    public new AutoSizeMode AutoSizeMode { get; set; }

    /// <summary>
    /// Gets a value indicating whether it is possible to add a new color to the list.
    /// </summary>
    [Browsable(false)]
    public bool CanAddColor
    {
      get
      {
        return MaximumColorCount == 0 || Colors.Count < MaximumColorCount;
      }
    }

    /// <summary>
    /// Gets or sets the arrow size of color item.
    /// </summary>
    [Category("Color item")]
    [Description("The arrow size of color item.")]
    [DefaultValue(0.35f)]
    [TypeConverter(typeof(GradientColorPercentValueFloat))]
    [SuppressMessage("StyleCop.CSharp.LayoutRules", "SA1516:ElementsMustBeSeparatedByBlankLine", Justification = "Reviewed.")]
    [SuppressMessage("StyleCop.CSharp.LayoutRules", "SA1513:ClosingCurlyBracketMustBeFollowedByBlankLine", Justification = "Reviewed.")]
    public float ColorItemArrowSize
    {
      get
      {
        return colorItemArrowSize;
      }
      set
      {
        colorItemArrowSize = value;

        if (value + ColorItemBlockSize > 1.0f)
        {
          ColorItemBlockSize = 1.0f - value;
        }

        Refresh();
      }
    }

    /// <summary>
    /// Gets or sets the size of the color item block.
    /// </summary>
    [Category("Color item")]
    [Description("The size of the color item block.")]
    [DefaultValue(0.65f)]
    [TypeConverter(typeof(GradientColorPercentValueFloat))]
    [SuppressMessage("StyleCop.CSharp.LayoutRules", "SA1516:ElementsMustBeSeparatedByBlankLine", Justification = "Reviewed.")]
    [SuppressMessage("StyleCop.CSharp.LayoutRules", "SA1513:ClosingCurlyBracketMustBeFollowedByBlankLine", Justification = "Reviewed.")]
    public float ColorItemBlockSize
    {
      get
      {
        return colorItemBlockSize;
      }
      set
      {
        colorItemBlockSize = value;

        if (value + ColorItemArrowSize > 1.0f)
        {
          ColorItemArrowSize = 1.0f - value;
        }

        Refresh();
      }
    }

    /// <summary>
    /// Gets or sets the border style of the color item.
    /// </summary>
    [Category("Color item")]
    [Description("The border style of the color item.")]
    [DefaultValue(typeof(BorderStyle), "Fixed3D")]
    [SuppressMessage("StyleCop.CSharp.LayoutRules", "SA1516:ElementsMustBeSeparatedByBlankLine", Justification = "Reviewed.")]
    [SuppressMessage("StyleCop.CSharp.LayoutRules", "SA1513:ClosingCurlyBracketMustBeFollowedByBlankLine", Justification = "Reviewed.")]
    public BorderStyle ColorItemBorderStyle
    {
      get
      {
        return colorItemBorderStyle;
      }
      set
      {
        colorItemBorderStyle = value;

        Refresh();
      }
    }

    /// <summary>
    /// Gets or sets the width of the color item.
    /// </summary>
    [Category("Color item")]
    [Description("The width of the color item.")]
    [DefaultValue(12)]
    [SuppressMessage("StyleCop.CSharp.LayoutRules", "SA1516:ElementsMustBeSeparatedByBlankLine", Justification = "Reviewed.")]
    [SuppressMessage("StyleCop.CSharp.LayoutRules", "SA1513:ClosingCurlyBracketMustBeFollowedByBlankLine", Justification = "Reviewed.")]
    public int ColorItemWidth
    {
      get
      {
        return colorItemWidth;
      }
      set
      {
        if (value <= 0)
        {
          value = 1;
        }

        colorItemWidth = value;

        OnResize(EventArgs.Empty);
      }
    }

    /// <summary>
    /// Gets the list of colors.
    /// </summary>
    [Category("Behavior")]
    [Description("The list of colors.")]
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
    [MergableProperty(false)]
    [SuppressMessage("StyleCop.CSharp.LayoutRules", "SA1516:ElementsMustBeSeparatedByBlankLine", Justification = "Reviewed.")]
    public GradientColorPickerItemCollection Colors
    {
      get
      {
        return colors;
      }
    }

    /// <summary>
    /// Gets or sets the cursor that will be displayed when the mouse is over this control.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [SuppressMessage("StyleCop.CSharp.LayoutRules", "SA1516:ElementsMustBeSeparatedByBlankLine", Justification = "Reviewed.")]
    [SuppressMessage("StyleCop.CSharp.LayoutRules", "SA1513:ClosingCurlyBracketMustBeFollowedByBlankLine", Justification = "Reviewed.")]
    public new Cursor Cursor
    {
      get
      {
        return base.Cursor;
      }
      set
      {
        base.Cursor = value;
      }
    }

#if DEBUG

    /// <summary>
    /// Gets or sets a value indicating whether the need to draw debugging information. Only for DEBUG build!
    /// </summary>
    [Category("Design")]
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [DefaultValue(true)]
    public bool DrawColorPosition { get; set; }

#endif

    /// <summary>
    /// Gets or sets the font.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public new Font Font { get; set; }

    /// <summary>
    /// Gets or sets the fore color.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public new Color ForeColor { get; set; }

    /// <summary>
    /// Gets or sets the gradient layout.
    /// </summary>
    [Category("Gradient")]
    [Description("Specifies the mode for drawing a gradient.")]
    [DefaultValue(typeof(GradientColorPickerLayout), "FixedSize")]
    [SuppressMessage("StyleCop.CSharp.LayoutRules", "SA1516:ElementsMustBeSeparatedByBlankLine", Justification = "Reviewed.")]
    [SuppressMessage("StyleCop.CSharp.LayoutRules", "SA1513:ClosingCurlyBracketMustBeFollowedByBlankLine", Justification = "Reviewed.")]
    public GradientColorPickerLayout GradientLayout
    {
      get
      {
        return gradientLayout;
      }
      set
      {
        gradientLayout = value;

        OnResize(EventArgs.Empty);
      }
    }

    /// <summary>
    /// Gets or sets the size of the gradient.
    /// </summary>
    [Category("Gradient")]
    [Description("The gradient size.")]
    [DefaultValue(10)]
    [SuppressMessage("StyleCop.CSharp.LayoutRules", "SA1516:ElementsMustBeSeparatedByBlankLine", Justification = "Reviewed.")]
    [SuppressMessage("StyleCop.CSharp.LayoutRules", "SA1513:ClosingCurlyBracketMustBeFollowedByBlankLine", Justification = "Reviewed.")]
    public int GradientLayoutSize
    {
      get
      {
        return gradientLayoutSize;
      }
      set
      {
        gradientLayoutSize = value;

        OnResize(EventArgs.Empty);
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to use grayscale when the element is disabled.
    /// </summary>
    [Category("Behavior")]
    [Description("Specifies whether to use grayscale when the element is disabled.")]
    [DefaultValue(true)]
    [SuppressMessage("StyleCop.CSharp.LayoutRules", "SA1516:ElementsMustBeSeparatedByBlankLine", Justification = "Reviewed.")]
    [SuppressMessage("StyleCop.CSharp.LayoutRules", "SA1513:ClosingCurlyBracketMustBeFollowedByBlankLine", Justification = "Reviewed.")]
    public bool GrayscaleWhenDisabled
    {
      get
      {
        return grayscaleWhenDisabled;
      }
      set
      {
        grayscaleWhenDisabled = value;

        Refresh();
      }
    }

    /// <summary>
    /// Gets or sets the ImeMode.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed.")]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed.")]
    public new ImeMode ImeMode { get; set; }

    /// <summary>
    /// Gets or sets the maximum number of colors in the list.
    /// </summary>
    [Category("Behavior")]
    [Description("Specifies the maximum number of colors that can be added to the list.")]
    [DefaultValue(32)]
    [SuppressMessage("StyleCop.CSharp.LayoutRules", "SA1516:ElementsMustBeSeparatedByBlankLine", Justification = "Reviewed.")]
    [SuppressMessage("StyleCop.CSharp.LayoutRules", "SA1513:ClosingCurlyBracketMustBeFollowedByBlankLine", Justification = "Reviewed.")]
    public int MaximumColorCount
    {
      get
      {
        return maximumColorCount;
      }
      set
      {
        if (value < MinimumColorCount)
        {
          MinimumColorCount = value;
        }

        if (value < Colors.Count)
        {
          Colors.RemoveRange(value - 1, Colors.Count - value);
        }

        maximumColorCount = value;

        Refresh();
      }
    }

    /// <summary>
    /// Gets or sets the minimum number of colors in the list.
    /// </summary>
    [Category("Behavior")]
    [Description("Specifies the minimum allowed number of colors that must be in the list.")]
    [DefaultValue(0)]
    [SuppressMessage("StyleCop.CSharp.LayoutRules", "SA1516:ElementsMustBeSeparatedByBlankLine", Justification = "Reviewed.")]
    [SuppressMessage("StyleCop.CSharp.LayoutRules", "SA1513:ClosingCurlyBracketMustBeFollowedByBlankLine", Justification = "Reviewed.")]
    public int MinimumColorCount
    {
      get
      {
        return minimumColorCount;
      }
      set
      {
        if (value > MaximumColorCount)
        {
          MaximumColorCount = value;
        }

        if (value > Colors.Count)
        {
          AddRandomColors(value - Colors.Count);
        }

        minimumColorCount = value;

        Refresh();
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether the notification should be displayed when exceeding the allowed number of colors.
    /// </summary>
    [Category("Behavior")]
    [Description("Indicating whether the notification should be displayed when exceeding the allowed number of colors.")]
    [DefaultValue(true)]
    public bool NotifyOfExceedingLimit { get; set; }

    /// <summary>
    /// Gets or sets RightToLeft. This is used for international applications where the language is written from RightToLeft. When this property is true, control placement and text will be from right to left.
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public new RightToLeft RightToLeft { get; set; }

    /// <summary>
    /// Gets or sets the selected color.
    /// </summary>
    [Browsable(false)]
    [SuppressMessage("StyleCop.CSharp.LayoutRules", "SA1516:ElementsMustBeSeparatedByBlankLine", Justification = "Reviewed.")]
    [SuppressMessage("StyleCop.CSharp.LayoutRules", "SA1513:ClosingCurlyBracketMustBeFollowedByBlankLine", Justification = "Reviewed.")]
    public GradientColorPickerItem SelectedColor
    {
      get
      {
        return selectedColor;
      }
      set
      {
        Colors.ActiveColor(selectedColor = value);
      }
    }

    /// <summary>
    /// Gets or sets a value indicating whether to show the image that represents the transparent color.
    /// </summary>
    [Category("Transparent background")]
    [Description("Specifies whether to display the background image instead of the transparent color or not.")]
    [DefaultValue(true)]
    [SuppressMessage("StyleCop.CSharp.LayoutRules", "SA1516:ElementsMustBeSeparatedByBlankLine", Justification = "Reviewed.")]
    [SuppressMessage("StyleCop.CSharp.LayoutRules", "SA1513:ClosingCurlyBracketMustBeFollowedByBlankLine", Justification = "Reviewed.")]
    public bool ShowTransparentBackground
    {
      get
      {
        return showTransparentBackground;
      }
      set
      {
        showTransparentBackground = value;

        Refresh();
      }
    }

    /// <summary>
    /// Gets or sets the size of this control is in pixels.
    /// </summary>
    [Category("Layout")]
    [Description("The size of this control is in pixels.")]
    [DefaultValue(typeof(Size), "175,28")]
    [SuppressMessage("StyleCop.CSharp.LayoutRules", "SA1513:ClosingCurlyBracketMustBeFollowedByBlankLine", Justification = "Reviewed.")]
    [SuppressMessage("StyleCop.CSharp.LayoutRules", "SA1516:ElementsMustBeSeparatedByBlankLine", Justification = "Reviewed.")]
    public new Size Size
    {
      get
      {
        return base.Size;
      }
      set
      {
        base.Size = value;
      }
    }

    /// <summary>
    /// Gets or sets the first color of the image that represents the transparent color.
    /// </summary>
    [Category("Transparent background")]
    [Description("First color of the image that represents the transparent color.")]
    [DefaultValue(typeof(Color), "White")]
    [SuppressMessage("StyleCop.CSharp.LayoutRules", "SA1516:ElementsMustBeSeparatedByBlankLine", Justification = "Reviewed.")]
    [SuppressMessage("StyleCop.CSharp.LayoutRules", "SA1513:ClosingCurlyBracketMustBeFollowedByBlankLine", Justification = "Reviewed.")]
    public Color TransparentBackgroundColor1
    {
      get
      {
        return transparentBackgroundColor1;
      }
      set
      {
        transparentBackgroundColor1 = value;

        Refresh();
      }
    }

    /// <summary>
    /// Gets or sets the second color of the image that represents the transparent color.
    /// </summary>
    [Category("Transparent background")]
    [Description("Second color of the image that represents the transparent color.")]
    [DefaultValue(typeof(Color), "Silver")]
    [SuppressMessage("StyleCop.CSharp.LayoutRules", "SA1516:ElementsMustBeSeparatedByBlankLine", Justification = "Reviewed.")]
    [SuppressMessage("StyleCop.CSharp.LayoutRules", "SA1513:ClosingCurlyBracketMustBeFollowedByBlankLine", Justification = "Reviewed.")]
    public Color TransparentBackgroundColor2
    {
      get
      {
        return transparentBackgroundColor2;
      }
      set
      {
        transparentBackgroundColor2 = value;

        Refresh();
      }
    }

    /// <summary>
    /// Gets or sets the cell size of the transparent background image.
    /// </summary>
    [Category("Transparent background")]
    [Description("Cell size of the transparent background image.")]
    [DefaultValue(4)]
    [SuppressMessage("StyleCop.CSharp.LayoutRules", "SA1516:ElementsMustBeSeparatedByBlankLine", Justification = "Reviewed.")]
    [SuppressMessage("StyleCop.CSharp.LayoutRules", "SA1513:ClosingCurlyBracketMustBeFollowedByBlankLine", Justification = "Reviewed.")]
    public int TransparentBackgroundSize
    {
      get
      {
        return transparentBackgroundSize;
      }
      set
      {
        transparentBackgroundSize = value;

        Refresh();
      }
    }

    /// <summary>
    /// Gets or sets height of the color item.
    /// </summary>
    [Browsable(false)]
    internal int ColorItemHeight { get; set; }

    /// <summary>
    /// Gets or sets rectangle of the color list.
    /// </summary>
    internal Rectangle ColorRectangle { get; set; }

    /// <summary>
    /// Gets or sets rectangle of the gradient.
    /// </summary>
    internal Rectangle GradientRectangle { get; set; }

    /// <summary>
    /// Adds new color to the list.
    /// </summary>
    /// <param name="color">The <see cref="Color"/> instance to add.</param>
    /// <param name="position">The color position. The value is from <c>0</c> to <c>1</c> (0-100%).</param>
    /// <returns>Returns the current instance of the <see cref="GradientColorPicker"/> class.</returns>
    public GradientColorPicker AddColor(Color color, float position)
    {
      if (position < 0 || position > 1.0f)
      {
        throw new ArgumentOutOfRangeException("position", "The value must be between zero and one.");
      }

      // create color
      var item = new GradientColorPickerItem(this, color);
      item.Position = position;

      // add color
      Colors.Add(item);

      // invoke ColorAdded handler
      ColorAdded?.Invoke(this, EventArgs.Empty);

      Refresh();

      return this;
    }

    /// <summary>
    /// Removes all colors from the list.
    /// </summary>
    /// <returns>Returns current instance of the <see cref="GradientColorPicker"/> class.</returns>
    public GradientColorPicker Clear()
    {
      SelectedColor = null;

      // remove all colors
      while (Colors.Any())
      {
        RemoveColor(Colors.First());
      }

      // add minimum count
      if (MinimumColorCount == 2)
      {
        if (AddColor(ColorRectangle.X, Color.Black))
        {
          AddColor(ColorRectangle.Width - ColorRectangle.X - ColorItemWidth, Color.White);
        }
      }
      else if (MinimumColorCount != 2 && MinimumColorCount > 0)
      {
        AddRandomColors(MinimumColorCount);
      }

      Refresh();

      return this;
    }

    /// <summary>
    /// Draws linear gradient through the specified <see cref="Graphics"/> instance.
    /// </summary>
    /// <param name="graphics">The <see cref="Graphics"/> instance.</param>
    /// <param name="targetBounds">The bounds within which the gradient is rendered.</param>
    /// <returns>Returns the current instance of the <see cref="GradientColorPicker"/> class.</returns>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public GradientColorPicker DrawLinearGradient(Graphics graphics, Rectangle targetBounds)
    {
      return DrawLinearGradient(graphics, targetBounds, 0);
    }

    /// <summary>
    /// Draws linear gradient through the specified <see cref="Graphics"/> instance.
    /// </summary>
    /// <param name="graphics">The <see cref="Graphics"/> instance.</param>
    /// <param name="targetBounds">The bounds within which the gradient is rendered.</param>
    /// <param name="angle">The tilt angle.</param>
    /// <returns>Returns the current instance of the <see cref="GradientColorPicker"/> class.</returns>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public GradientColorPicker DrawLinearGradient(Graphics graphics, Rectangle targetBounds, float angle)
    {
      if (graphics == null)
      {
        throw new ArgumentNullException("graphics");
      }

      if (targetBounds.IsEmpty)
      {
        throw new ArgumentNullException("targetBounds");
      }

      if (Colors.Count > 0)
      {
        graphics.SmoothingMode = SmoothingMode.AntiAlias;

        using (var brush = new LinearGradientBrush(targetBounds, Color.White, Color.Black, angle, true))
        {
          brush.InterpolationColors = Colors.GetColorBlend();

          graphics.FillRectangle(brush, targetBounds);
        }
      }

      return this;
    }

    /// <summary>
    /// Draws linear gradient to image.
    /// </summary>
    /// <param name="image">The <see cref="Image"/> instance.</param>
    /// <returns>Returns the current instance of the <see cref="GradientColorPicker"/> class.</returns>
    public GradientColorPicker DrawLinearGradientToImage(Image image)
    {
      return DrawLinearGradientToImage(image, Rectangle.Empty, 0);
    }

    /// <summary>
    /// Draws linear gradient to image.
    /// </summary>
    /// <param name="image">The <see cref="Image"/> instance.</param>
    /// <param name="angle">The tilt angle.</param>
    /// <returns>Returns the current instance of the <see cref="GradientColorPicker"/> class.</returns>
    public GradientColorPicker DrawLinearGradientToImage(Image image, float angle)
    {
      return DrawLinearGradientToImage(image, Rectangle.Empty, angle);
    }

    /// <summary>
    /// Draws linear gradient to image.
    /// </summary>
    /// <param name="image">The <see cref="Image"/> instance.</param>
    /// <param name="targetBounds">The bounds within which the gradient is rendered.</param>
    /// <returns>Returns the current instance of the <see cref="GradientColorPicker"/> class.</returns>
    public GradientColorPicker DrawLinearGradientToImage(Image image, Rectangle targetBounds)
    {
      return DrawLinearGradientToImage(image, targetBounds, 0);
    }

    /// <summary>
    /// Draws linear gradient to image.
    /// </summary>
    /// <param name="image">The <see cref="Image"/> instance.</param>
    /// <param name="targetBounds">The bounds within which the gradient is rendered.</param>
    /// <param name="angle">The tilt angle.</param>
    /// <returns>Returns the current instance of the <see cref="GradientColorPicker"/> class.</returns>
    public GradientColorPicker DrawLinearGradientToImage(Image image, Rectangle targetBounds, float angle)
    {
      if (image == null)
      {
        throw new ArgumentNullException("image");
      }

      if (targetBounds.IsEmpty)
      {
        targetBounds = new Rectangle(0, 0, image.Width, image.Height);
      }

      if (Colors.Count > 0)
      {
        using (var g = Graphics.FromImage(image))
        {
          DrawLinearGradient(g, targetBounds, angle);
        }
      }

      return this;
    }

    /// <summary>
    /// Draws radial gradient through the specified <see cref="Graphics"/> instance.
    /// </summary>
    /// <param name="graphics">The <see cref="Graphics"/> instance.</param>
    /// <param name="targetBounds">The bounds within which the gradient is rendered.</param>
    /// <param name="centerPoint">The center point.</param>
    /// <returns>Returns the current instance of the <see cref="GradientColorPicker"/> class.</returns>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public GradientColorPicker DrawRadialGradient(Graphics graphics, Rectangle targetBounds, float centerPoint)
    {
      return DrawRadialGradient(graphics, targetBounds, new PointF(centerPoint, centerPoint));
    }

    /// <summary>
    /// Draws radial gradient through the specified <see cref="Graphics"/> instance.
    /// </summary>
    /// <param name="graphics">The <see cref="Graphics"/> instance.</param>
    /// <param name="targetBounds">The bounds within which the gradient is rendered.</param>
    /// <param name="centerX">The position of the center point along the X axis.</param>
    /// <param name="centerY">The position of the center point along the Y axis.</param>
    /// <returns>Returns the current instance of the <see cref="GradientColorPicker"/> class.</returns>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public GradientColorPicker DrawRadialGradient(Graphics graphics, Rectangle targetBounds, float centerX, float centerY)
    {
      return DrawRadialGradient(graphics, targetBounds, new PointF(centerX, centerY));
    }

    /// <summary>
    /// Draws radial gradient through the specified <see cref="Graphics"/> instance.
    /// </summary>
    /// <param name="graphics">The <see cref="Graphics"/> instance.</param>
    /// <param name="targetBounds">The bounds within which the gradient is rendered.</param>
    /// <param name="centerPoint">The center point.</param>
    /// <returns>Returns the current instance of the <see cref="GradientColorPicker"/> class.</returns>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    public GradientColorPicker DrawRadialGradient(Graphics graphics, Rectangle targetBounds, PointF centerPoint)
    {
      if (graphics == null)
      {
        throw new ArgumentNullException("graphics");
      }

      if (targetBounds.IsEmpty)
      {
        throw new ArgumentNullException("targetBounds");
      }

      if (Colors.Count > 0)
      {
        graphics.SmoothingMode = SmoothingMode.AntiAlias;

        using (var path = new GraphicsPath())
        {
          path.AddEllipse(targetBounds);

          using (var brush = new PathGradientBrush(path))
          {
            brush.CenterPoint = centerPoint;
            brush.InterpolationColors = Colors.GetColorBlend();

            graphics.FillPath(brush, path);
          }
        }
      }

      return this;
    }

    /// <summary>
    /// Draws radial gradient to image.
    /// </summary>
    /// <param name="image">The <see cref="Image"/> instance.</param>
    /// <param name="centerPoint">The center point.</param>
    /// <returns>Returns the current instance of the <see cref="GradientColorPicker"/> class.</returns>
    public GradientColorPicker DrawRadialGradientToImage(Image image, float centerPoint)
    {
      return DrawRadialGradientToImage(image, Rectangle.Empty, new PointF(centerPoint, centerPoint));
    }

    /// <summary>
    /// Draws radial gradient to image.
    /// </summary>
    /// <param name="image">The <see cref="Image"/> instance.</param>
    /// <param name="centerX">The position of the center point along the X axis.</param>
    /// <param name="centerY">The position of the center point along the Y axis.</param>
    /// <returns>Returns the current instance of the <see cref="GradientColorPicker"/> class.</returns>
    public GradientColorPicker DrawRadialGradientToImage(Image image, float centerX, float centerY)
    {
      return DrawRadialGradientToImage(image, Rectangle.Empty, new PointF(centerX, centerY));
    }

    /// <summary>
    /// Draws radial gradient to image.
    /// </summary>
    /// <param name="image">The <see cref="Image"/> instance.</param>
    /// <param name="targetBounds">The bounds within which the gradient is rendered.</param>
    /// <param name="centerPoint">The center point.</param>
    /// <returns>Returns the current instance of the <see cref="GradientColorPicker"/> class.</returns>
    public GradientColorPicker DrawRadialGradientToImage(Image image, Rectangle targetBounds, float centerPoint)
    {
      return DrawRadialGradientToImage(image, targetBounds, new PointF(centerPoint, centerPoint));
    }

    /// <summary>
    /// Draws radial gradient to image.
    /// </summary>
    /// <param name="image">The <see cref="Image"/> instance.</param>
    /// <param name="targetBounds">The bounds within which the gradient is rendered.</param>
    /// <param name="centerX">The position of the center point along the X axis.</param>
    /// <param name="centerY">The position of the center point along the Y axis.</param>
    /// <returns>Returns the current instance of the <see cref="GradientColorPicker"/> class.</returns>
    public GradientColorPicker DrawRadialGradientToImage(Image image, Rectangle targetBounds, float centerX, float centerY)
    {
      return DrawRadialGradientToImage(image, targetBounds, new PointF(centerX, centerY));
    }

    /// <summary>
    /// Draws radial gradient to image.
    /// </summary>
    /// <param name="image">The <see cref="Image"/> instance.</param>
    /// <param name="targetBounds">The bounds within which the gradient is rendered.</param>
    /// <param name="centerPoint">The center point.</param>
    /// <returns>Returns the current instance of the <see cref="GradientColorPicker"/> class.</returns>
    public GradientColorPicker DrawRadialGradientToImage(Image image, Rectangle targetBounds, PointF centerPoint)
    {
      if (image == null)
      {
        throw new ArgumentNullException("image");
      }

      if (targetBounds.IsEmpty)
      {
        targetBounds = new Rectangle(0, 0, image.Width, image.Height);
      }

      if (Colors.Count > 0)
      {
        using (var g = Graphics.FromImage(image))
        {
          DrawRadialGradient(g, targetBounds, centerPoint);
        }
      }

      return this;
    }

    /// <summary>
    /// Aligns the colors evenly.
    /// </summary>
    /// <returns>Returns the current instance of the <see cref="GradientColorPicker"/> class.</returns>
    public GradientColorPicker EvenlyAlignColors()
    {
      float step = 1.0f / (colors.Count - 1);
      float start = 0;

      foreach (var color in Colors)
      {
        color.Position = start;
        start += step;
      }

      ColorMoved?.Invoke(this, EventArgs.Empty);

      return this;
    }

    /// <summary>
    /// Gets next color.
    /// </summary>
    /// <param name="color">The color item relative to which the search will be performed.</param>
    /// <returns>Returns instance of the <see cref="GradientColorPickerItem"/> class.</returns>
    public GradientColorPickerItem GetNextColor(GradientColorPickerItem color)
    {
      if (color == null)
      {
        return null;
      }

      var next = Colors.OrderBy(item => item.Position).FirstOrDefault(item => item.Position >= color.Position && !item.Equals(color));

      if (next == null)
      {
        next = Colors.OrderByDescending(item => item.Position).FirstOrDefault(item => item.Position <= color.Position && !item.Equals(color));
      }

      return next;
    }

    /// <summary>
    /// Inverts colors.
    /// </summary>
    /// <returns>Returns the current instance of the <see cref="GradientColorPicker"/> class.</returns>
    public GradientColorPicker InvertColors()
    {
      foreach (var color in Colors)
      {
        color.Color = Color.FromArgb(color.Color.ToArgb() ^ 0xffffff);
      }

      ColorChanged?.Invoke(this, EventArgs.Empty);

      return this;
    }

    /// <summary>
    /// Randomizes the colors in the list.
    /// </summary>
    /// <param name="changePosition">Randomize position.</param>
    /// <param name="changeColor">Randomize color.</param>
    /// <returns>Returns the current instance of the <see cref="GradientColorPicker"/> class.</returns>
    public GradientColorPicker Randomize(bool changePosition, bool changeColor)
    {
      var rnd = new Random(DateTime.Now.Millisecond);

      foreach (var color in Colors)
      {
        if (changePosition)
        {
          color.Position = (float)rnd.NextDouble();
        }

        if (changeColor)
        {
          color.Color = Color.FromArgb(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255));
        }
      }

      if (changePosition)
      {
        ColorMoved?.Invoke(this, EventArgs.Empty);
      }

      if (changeColor)
      {
        ColorChanged?.Invoke(this, EventArgs.Empty);
      }

      return this;
    }

    /// <summary>
    /// Removes color from list.
    /// </summary>
    /// <param name="item">The <see cref="GradientColorPickerItem"/> instance to remove.</param>
    /// <returns>Returns the current instance of the <see cref="GradientColorPicker"/> class.</returns>
    public GradientColorPicker RemoveColor(GradientColorPickerItem item)
    {
      if (item == null)
      {
        return this;
      }

      Colors.Remove(item);
      ColorRemoved?.Invoke(this, EventArgs.Empty);
      Refresh();

      return this;
    }

    /// <summary>
    /// Removes selected color (<see cref="SelectedColor"/>).
    /// </summary>
    /// <returns>Returns the current instance of the <see cref="GradientColorPicker"/> class.</returns>
    public GradientColorPicker RemoveSelectedColor()
    {
      RemoveColor(SelectedColor);
      SelectedColor = null;
      Refresh();

      return this;
    }

    /// <summary>
    /// Reverses colors in the list.
    /// </summary>
    /// <returns>Returns the current instance of the <see cref="GradientColorPicker"/> class.</returns>
    public GradientColorPicker Reverse()
    {
      Colors.Sort();

      for (int i = 0; i <= (Colors.Count / 2) - 1; i++)
      {
        int x = Colors[Colors.Count - 1 - i].X;

        Colors[Colors.Count - i - 1].SetPosition(Colors[i].X);
        Colors[i].SetPosition(x);
      }

      Colors.Sort();

      ColorMoved?.Invoke(this, EventArgs.Empty);

      Refresh();

      return this;
    }

    /// <summary>
    /// Selects next color.
    /// </summary>
    public void SelectNextColor()
    {
      if (SelectedColor != null)
      {
        var next = Colors.OrderBy(item => item.Position).FirstOrDefault(item => item.Position >= SelectedColor.Position && !item.Equals(SelectedColor));

        if (next == null)
        {
          next = Colors.OrderByDescending(item => item.Position).FirstOrDefault(item => item.Position <= SelectedColor.Position && !item.Equals(SelectedColor));
        }

        if (next != null)
        {
          SelectedColor = next;
        }
      }
      else
      {
        SelectedColor = Colors.FirstOrDefault();
      }
    }

    /// <summary>
    /// Raises the <see cref="Control.EnabledChanged"/> event.
    /// </summary>
    /// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected override void OnEnabledChanged(EventArgs e)
    {
      base.OnEnabledChanged(e);
      Refresh();
    }

    /// <summary>
    /// Raises the <see cref="Control.KeyDown"/> event.
    /// </summary>
    /// <param name="e">An <see cref="KeyEventArgs"/> that contains the event data.</param>
    [SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Reviewed.")]
    [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Reviewed.")]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected override void OnKeyDown(KeyEventArgs e)
    {
      if (AllowToHandleKeys)
      {
        if (e.KeyCode == Keys.Tab)
        {
          // active next color
          SelectNextColor(e.Control);
        }
        else if (e.KeyCode == Keys.Left || e.KeyCode == Keys.Down)
        {
          // move color
          if (SelectedColor != null)
          {
            SelectedColor.Position -= e.Alt ? 0.001f : 0.01f;
          }
        }
        else if (e.KeyCode == Keys.Right || e.KeyCode == Keys.Up)
        {
          // move color
          if (SelectedColor != null)
          {
            SelectedColor.Position += e.Alt ? 0.001f : 0.01f;
          }
        }
        else if (e.KeyCode == Keys.Home)
        {
          // goto first color
          if (SelectedColor != null)
          {
            SelectedColor.Position = 0.0f;
          }
        }
        else if (e.KeyCode == Keys.End)
        {
          // goto to last color
          if (SelectedColor != null)
          {
            SelectedColor.Position = 1.0f;
          }
        }
        else if (e.KeyCode == Keys.Delete)
        {
          // delete selected color
          var next = GetNextColor(SelectedColor);

          RemoveSelectedColor();

          if ((SelectedColor = next) == null)
          {
            SelectNextColor();
          }
        }
        else if (e.KeyCode == Keys.A && !EE)
        {
          // add new color
          var rnd = new Random(DateTime.Now.Millisecond);

          AddColor(rnd.Next(ColorRectangle.X, ColorRectangle.Width), lastColor);
        }
        else if (e.KeyCode == Keys.E)
        {
          // alignment equally
          EvenlyAlignColors();
        }
        else if (e.KeyCode == Keys.R)
        {
          // random position and/or color of existing elements
          Randomize(!e.Alt, !e.Control);
        }
        else if (e.KeyCode == Keys.I)
        {
          // invert colors
          InvertColors();
        }
        else if (e.KeyCode == Keys.X)
        {
          // cut selected item
          CutSelectedColorToClipboard();
        }
        else if (e.KeyCode == Keys.C)
        {
          // copy selected item
          CopySelectedColorToClipboard();
        }
        else if (e.KeyCode == Keys.V)
        {
          // paste item from clipboad
          PasteColorFromClipboard();
        }
        else if (e.KeyCode == Keys.Space || e.KeyCode == Keys.Enter)
        {
          // color dialog
          if (SelectedColor != null && AllowColorDialog)
          {
            ShowColorDialog();
          }
        }
        else if (e.KeyCode == Keys.Escape)
        {
          // just reset
          EE = false;
          EE2 = 0;
          Refresh();
        }
        else if (e.KeyCode == Keys.P)
        {
          // something strange
          EE = true;
        }

        if (EE)
        {
          if (e.KeyCode == Keys.P || e.KeyCode == Keys.L || e.KeyCode == Keys.A || e.KeyCode == Keys.Y)
          {
            EES += e.KeyCode.ToString();
          }
          else
          {
            EE = false;
            EES = string.Empty;
          }

          TryPlay();
        }
      }

      base.OnKeyDown(e);
    }

    /// <summary>
    /// Raises the <see cref="UserControl.Load"/> event.
    /// </summary>
    /// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected override void OnLoad(EventArgs e)
    {
      OnResize(EventArgs.Empty);
      base.OnLoad(e);
    }

    /// <summary>
    /// Raises the <see cref="Control.MouseDoubleClick"/> event.
    /// </summary>
    /// <param name="e">An <see cref="MouseEventArgs"/> that contains the event data.</param>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Reviewed.")]
    protected override void OnMouseDoubleClick(MouseEventArgs e)
    {
      if (SelectedColor != null && AllowColorDialog)
      {
        bool canShowDialog = e.Button == MouseButtons.Left;

        if (GradientLayout == GradientColorPickerLayout.Percent || GradientLayout == GradientColorPickerLayout.FixedSize)
        {
          canShowDialog = e.Location.Y >= GradientRectangle.Y + GradientRectangle.Height;
        }

        if (canShowDialog)
        {
          ShowColorDialog();
        }
      }

      base.OnMouseDoubleClick(e);
    }

    /// <summary>
    /// Raises the <see cref="Control.MouseDown"/> event.
    /// </summary>
    /// <param name="e">An <see cref="MouseEventArgs"/> that contains the event data.</param>
    [SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Reviewed.")]
    [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Reviewed.")]
    [SuppressMessage("StyleCop.CSharp.MaintainabilityRules", "SA1119:StatementMustNotUseUnnecessaryParenthesis", Justification = "Reviewed.")]
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected override void OnMouseDown(MouseEventArgs e)
    {
      bool canMove = e.Button == MouseButtons.Left && e.Clicks == 1;

      // check for intersection with the gradient 
      if (canMove && (GradientLayout == GradientColorPickerLayout.Percent || GradientLayout == GradientColorPickerLayout.FixedSize))
      {
        if (e.Location.Y >= GradientRectangle.Y && e.Location.Y <= GradientRectangle.Y + GradientRectangle.Height)
        {
          if (AllowPickGradientColor)
          {
            using (var bitmap = new Bitmap(ClientSize.Width, ClientSize.Height))
            {
              DrawToBitmap(bitmap, ClientRectangle);

              lastColor = bitmap.GetPixel(e.Location.X, e.Location.Y);
            }
          }

          canMove = false;
        }
      }

      // check for intersection with the color block 
      if (canMove)
      {
        if (canMove = (e.Location.X >= ColorRectangle.X && e.Location.X <= ColorRectangle.X + ColorRectangle.Width))
        {
          canMove = (e.Location.Y >= ColorRectangle.Y && e.Location.Y <= ColorRectangle.Y + ColorRectangle.Height);
        }
      }

      // search or add new color
      if (canMove)
      {
        // search color
        SelectedColor = Colors.ActiveColorByLocation(e.Location);

        if (SelectedColor == null && AllowAddColorByClick)
        {
          // add new item
          canMove = AddColor(e.X - (ColorItemWidth / 2) - ColorRectangle.X, lastColor);
        }
      }

      // start moving
      if (canMove && SelectedColor != null)
      {
        isItemMoved = true;
        lastY = e.Y;

        // set offset
        SelectedColor.OffsetX = e.X;
        SelectedColor.OffsetY = e.Y;

        // invoke ColorSelected handler
        ColorSelected?.Invoke(this, EventArgs.Empty);

        // store selected color
        lastColor = SelectedColor.Color;

        // redraw
        Refresh();
      }

      if (e.Location.X <= 4 && e.Location.Y <= 4)
      {
        EE2++;

        if (EE2 == 10)
        {
          Refresh();
        }
      }
      else
      {
        if (EE2 < 10)
        {
          EE2 = 0;
        }
      }

      if (e.Location.X >= ClientRectangle.Width - 4 && e.Location.Y <= 4)
      {
        EE3++;

        if (EE3 == 10)
        {
          using (var about = new GradientColorPickerAbout())
          {
            about.ShowDialog();

            EE3 = 0;
          }
        }
      }
      else
      {
        if (EE3 < 10)
        {
          EE3 = 0;
        }
      }

      base.OnMouseDown(e);
    }

    /// <summary>
    /// Raises the <see cref="Control.MouseMove"/> event.
    /// </summary>
    /// <param name="e">An <see cref="MouseEventArgs"/> that contains the event data.</param>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Reviewed.")]
    protected override void OnMouseMove(MouseEventArgs e)
    {
      lastMouseLocation = e.Location;

      if (AllowPickGradientColor && (GradientLayout == GradientColorPickerLayout.Percent || GradientLayout == GradientColorPickerLayout.FixedSize))
      {
        if (e.Location.Y >= GradientRectangle.Y && e.Location.Y <= GradientRectangle.Y + GradientRectangle.Height)
        {
          Cursor = Cursors.Cross;
        }
        else
        {
          Cursor = Cursors.Hand;
        }
      }
      else
      {
        Cursor = Cursors.Hand;
      }

      if (isItemMoved)
      {
        if (lastY != -1 && Math.Abs(e.Y - lastY) > 50)
        {
          if (Colors.Count > MinimumColorCount && SelectedColor != null && AllowDragOutColor)
          {
            lastColor = SelectedColor.Color;

            Colors.Remove(SelectedColor);

            SelectedColor = null;

            ColorRemoved?.Invoke(this, EventArgs.Empty);
          }
          else
          {
            if (SelectedColor != null)
            {
              MoveItem(e.Location);
            }
          }
        }
        else
        {
          MoveItem(e.Location);
        }

        Refresh();
      }
#if DEBUG
      else
      {
        Refresh();
      }
#endif

      base.OnMouseMove(e);
    }

    /// <summary>
    /// Raises the <see cref="Control.MouseUp"/> event.
    /// </summary>
    /// <param name="e">An <see cref="MouseEventArgs"/> that contains the event data.</param>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Reviewed.")]
    protected override void OnMouseUp(MouseEventArgs e)
    {
      if (isItemMoved)
      {
        lastY = -1;

        if (SelectedColor != null)
        {
          MoveItem(e.Location);
        }

        // end moving
        isItemMoved = false;

        Refresh();
      }

      base.OnMouseUp(e);
    }

    /// <summary>
    /// Raises the <see cref="Control.MouseWheel" /> event.
    /// </summary>
    /// <param name="e">An <see cref="MouseEventArgs"/> that contains the event data.</param>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Reviewed.")]
    protected override void OnMouseWheel(MouseEventArgs e)
    {
      if (AllowToHandleMouseWheel && SelectedColor != null)
      {
        if (e.Delta > 0)
        {
          SelectedColor.Position += 0.01f;
        }
        else
        {
          SelectedColor.Position -= 0.01f;
        }
      }

      base.OnMouseWheel(e);
    }

    /// <summary>
    /// Raises the <see cref="Control.PaddingChanged"/> event.
    /// </summary>
    /// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected override void OnPaddingChanged(EventArgs e)
    {
      OnResize(EventArgs.Empty);
      base.OnPaddingChanged(e);
    }

    /// <summary>
    /// Raises the <see cref="Control.Paint"/> event.
    /// </summary>
    /// <param name="e">An <see cref="PaintEventArgs"/> that contains the event data.</param>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    [SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Reviewed.")]
    protected override void OnPaint(PaintEventArgs e)
    {
      using (var bitmap = new Bitmap(ClientSize.Width, ClientSize.Height))
      {
        using (var g = Graphics.FromImage(bitmap))
        {
          g.SmoothingMode = SmoothingMode.AntiAlias;

          DrawGradient(g);
          DrawAddBox(g);
          DrawColors(g);

#if DEBUG
          DrawPosition(g);
#endif
        }

        var g2 = e.Graphics;

        if (!Enabled && GrayscaleWhenDisabled)
        {
          using (var attributes = new ImageAttributes())
          {
            // grayscale
            var matrix = new ColorMatrix(new float[][]
            {
               new float[] { 0.3f, 0.3f, 0.3f, 0, 0 },
               new float[] { 0.59f, 0.59f, 0.59f, 0, 0 },
               new float[] { 0.11f, 0.11f, 0.11f, 0, 0 },
               new float[] { 0, 0, 0, 1, 0 },
               new float[] { 0, 0, 0, 0, 1 }
            });

            attributes.SetColorMatrix(matrix);

            g2.DrawImage(bitmap, ClientRectangle, 0, 0, ClientRectangle.Width, ClientRectangle.Height, GraphicsUnit.Pixel, attributes);
          }
        }
        else
        {
          g2.DrawImage(bitmap, ClientRectangle);
        }
      }

      base.OnPaint(e);
    }

    /// <summary>
    /// Raises the <see cref="Control.Resize"/> event.
    /// </summary>
    /// <param name="e">An <see cref="EventArgs"/> that contains the event data.</param>
    [EditorBrowsable(EditorBrowsableState.Advanced)]
    protected override void OnResize(EventArgs e)
    {
      int width = ClientSize.Width - Padding.Size.Width;
      int height = ColorItemHeight = ClientSize.Height - Padding.Size.Height;

      // calculate the size of rectangles
      if (GradientLayout == GradientColorPickerLayout.Percent)
      {
        int gradientHeight = (int)(height * GradientLayoutSize / 100.0);

        GradientRectangle = new Rectangle(Padding.Left, Padding.Top, width, gradientHeight);
        ColorRectangle = new Rectangle(Padding.Left, GradientRectangle.Top + GradientRectangle.Height + 1, width, height - GradientRectangle.Height);
      }
      else if (GradientLayout == GradientColorPickerLayout.FixedSize)
      {
        GradientRectangle = new Rectangle(Padding.Left, Padding.Top, width, GradientLayoutSize);
        ColorRectangle = new Rectangle(Padding.Left, GradientRectangle.Top + GradientRectangle.Height + 1, width, height - GradientRectangle.Height);
      }
      else if (GradientLayout == GradientColorPickerLayout.Background)
      {
        GradientRectangle = ColorRectangle = new Rectangle(Padding.Left, Padding.Top, width, height);
      }
      else
      {
        GradientRectangle = Rectangle.Empty;
        ColorRectangle = new Rectangle(Padding.Left, Padding.Top, width, height);
      }

      // adjust the size of the colors
      foreach (var item in Colors)
      {
        item.Width = ColorItemWidth;
        item.Height = height;
        item.OnPositionChanged(false);
      }

      // redraw
      Refresh();

      // default handler
      base.OnResize(e);
    }

    /// <summary>
    /// Processes a command key.
    /// </summary>
    /// <param name="msg">A <see cref="Message"/>, passed by reference, that represents the window message to process.</param>
    /// <param name="keyData">One of the <see cref="Keys"/> values that represents the key to process.</param>
    /// <returns><c>true</c> if the character was processed by the control; otherwise, <c>false</c>.</returns>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed.")]
    protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
    {
      if (keyData == Keys.Right || keyData == Keys.Left || keyData == Keys.Up || keyData == Keys.Down || keyData == Keys.Tab)
      {
        OnKeyDown(new KeyEventArgs(keyData));
        return true;
      }
      else
      {
        return base.ProcessCmdKey(ref msg, keyData);
      }
    }

    /// <summary>
    /// Adds color to list.
    /// </summary>
    /// <param name="x">The The color position along the X axis.</param>
    /// <param name="color">The <see cref="Color"/> to add.</param>
    /// <returns>Returns <c>true</c> if the color has been added to the list; otherwise, <c>false</c>.</returns>
    [SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", Justification = "Reviewed.")]
    [SuppressMessage("Microsoft.Globalization", "CA1300:SpecifyMessageBoxOptions", Justification = "Reviewed.")]
    [SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", Justification = "Reviewed.")]
    private bool AddColor(int x, Color color)
    {
      // there is no item at the specified coordinates
      if (!CanAddColor)
      {
        if (NotifyOfExceedingLimit)
        {
          MessageBox.Show($"You can use no more than {MaximumColorCount} points.", "Limit exceeding", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        LimitExceeded?.Invoke(this, EventArgs.Empty);

        return false;
      }

      // create color
      var item = new GradientColorPickerItem(this, color);

      item.SetPosition(x);

      // add color
      Colors.Add(item);

      // select item
      SelectedColor = item;

      // invoke ColorAdded handler
      ColorAdded?.Invoke(this, EventArgs.Empty);

      Refresh();

      return true;
    }

    /// <summary>
    /// Adds random colors to the list.
    /// </summary>
    /// <param name="count">The count of colors to add.</param>
    private void AddRandomColors(int count)
    {
      var rnd = new Random(DateTime.Now.Millisecond);

      for (int i = 0; i < count; i++)
      {
        if (!AddColor(rnd.Next(ColorRectangle.X, ColorRectangle.Width), Color.FromArgb(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255))))
        {
          break;
        }
      }
    }

    /// <summary>
    /// Copies <see cref="SelectedColor"/> to clipboard.
    /// </summary>
    private void CopySelectedColorToClipboard()
    {
      if (SelectedColor != null)
      {
        Clipboard.Clear();

        Clipboard.SetData("Color", SelectedColor.Color.ToArgb());
      }
    }

    /// <summary>
    /// Cut <see cref="SelectedColor"/> from list to clipboard.
    /// </summary>
    private void CutSelectedColorToClipboard()
    {
      if (SelectedColor != null)
      {
        var next = GetNextColor(SelectedColor);

        Clipboard.Clear();
        Clipboard.SetData("Color", SelectedColor.Color.ToArgb());

        Colors.Remove(SelectedColor);

        if ((SelectedColor = next) == null)
        {
          SelectNextColor();
        }
      }
    }

    /// <summary>
    /// Draws empty state.
    /// </summary>
    /// <param name="g">The <see cref="Graphics"/> instance.</param>
    [SuppressMessage("Microsoft.Reliability", "CA2000:DisposeObjectsBeforeLosingScope", Justification = "Reviewed.")]
    private void DrawAddBox(Graphics g)
    {
      if (Colors.Count <= 0 || AlwaysShowAddBox)
      {
        var pen = new Pen(Color.DarkGray);

        // canvas size and start position
        // int width = ColorRectangle.Width;
        int height = ColorRectangle.Height;
        int x = ColorRectangle.X + ((ColorRectangle.Width - ColorItemWidth) / 2);
        int y = ColorRectangle.Y;

        // arrow size
        int arrowWidth = ColorItemWidth;
        int arrowHeight = (int)(height * ColorItemArrowSize);

        // item size
        float itemWidth = ColorItemWidth;
        float itemHeight = (height * ColorItemBlockSize) - 2;

        // arrow
        var arrowRect = new RectangleF(x, y, arrowWidth, arrowHeight);

        float halfWidth = arrowRect.Width / 2;

        var points = new PointF[]
        {
          new PointF(arrowRect.Left + halfWidth, arrowRect.Top),
          new PointF(arrowRect.Left, arrowRect.Bottom),
          new PointF(arrowRect.Right, arrowRect.Bottom)
        };

        // rectangle
        var blockRect = new RectangleF(arrowRect.X, arrowRect.Y + arrowRect.Height, itemWidth, itemHeight);

        // draw arrow
        g.DrawPolygon(pen, points);

        // draw rectangle
        g.DrawRectangle
        (
          pen,
          blockRect.X,
          blockRect.Y,
          blockRect.Width,
          blockRect.Height
        );

        pen.Dispose();
      }
    }

    /// <summary>
    /// Draws single color item.
    /// </summary>
    /// <param name="g">The <see cref="Graphics"/> instance.</param>
    /// <param name="item">The <see cref="GradientColorPickerItem"/> to draw.</param>
    /// <param name="x">The color position along the X axis.</param>
    /// <param name="y">The color position along the Y axis.</param>
    /// <param name="arrowWidth">The width of array.</param>
    /// <param name="arrowHeight">The height of array.</param>
    /// <param name="itemWidth">The color item width.</param>
    /// <param name="itemHeight">The color item height.</param>
    [SuppressMessage("Microsoft.Reliability", "CA2000:DisposeObjectsBeforeLosingScope", Justification = "Reviewed.")]
    private void DrawColor(Graphics g, GradientColorPickerItem item, int x, int y, int arrowWidth, int arrowHeight, float itemWidth, float itemHeight)
    {
      // arrow
      var arrowRect = new RectangleF(x + item.X, y + item.Y, arrowWidth, arrowHeight);

      float halfWidth = arrowRect.Width / 2;

      var points = new PointF[]
      {
        new PointF(arrowRect.Left + halfWidth, arrowRect.Top),
        new PointF(arrowRect.Left, arrowRect.Bottom),
        new PointF(arrowRect.Right, arrowRect.Bottom)
      };

      // rectangle
      var blockRect = new Rectangle((int)arrowRect.X, Convert.ToInt32(arrowRect.Y + arrowRect.Height), (int)itemWidth, Convert.ToInt32(Math.Ceiling(itemHeight)));
      var blockRectF = new RectangleF(arrowRect.X, arrowRect.Y + arrowRect.Height, itemWidth, itemHeight);

      // draw arrow
      if (item.Active)
      {
        g.FillPolygon(Brushes.Black, points);
      }
      else
      {
        g.FillPolygon(Brushes.White, points);
      }

      g.DrawPolygon(Pens.Black, points);

      // draw color
      if (EE2 < 10)
      {
        var b = new SolidBrush(Color.FromArgb(item.Color.R, item.Color.G, item.Color.B));
        g.FillRectangle(b, blockRectF);
        b.Dispose();

        if (ColorItemBorderStyle == BorderStyle.Fixed3D)
        {
          ControlPaint.DrawBorder3D(g, blockRect, Border3DStyle.Raised);
        }
        else if (ColorItemBorderStyle == BorderStyle.FixedSingle)
        {
          ControlPaint.DrawBorder3D(g, blockRect, Border3DStyle.Flat);
        }

        g.DrawRectangle
        (
          Pens.Black,
          blockRectF.X,
          blockRectF.Y,
          blockRectF.Width,
          blockRectF.Height
        );
      }
      else
      {
        var b = new SolidBrush(Color.FromArgb(item.Color.R, item.Color.G, item.Color.B));
        g.FillEllipse(b, blockRectF);
        b.Dispose();

        g.DrawEllipse
        (
          Pens.Black,
          blockRectF.X,
          blockRectF.Y,
          blockRectF.Width,
          blockRectF.Height
        );
      }
    }

    /// <summary>
    /// Draws colors list.
    /// </summary>
    /// <param name="g">The <see cref="Graphics"/> instance.</param>
    private void DrawColors(Graphics g)
    {
      if (Colors.Count > 0)
      {
        // canvas size and start position
        // int width = ColorRectangle.Width;
        int height = ColorRectangle.Height;
        int x = ColorRectangle.X;
        int y = ColorRectangle.Y;

        // arrow size
        int arrowWidth = ColorItemWidth;
        int arrowHeight = (int)(height * ColorItemArrowSize);

        // item size
        float itemWidth = ColorItemWidth;
        float itemHeight = height * ColorItemBlockSize;

        if (GradientLayout == GradientColorPickerLayout.FixedSize || GradientLayout == GradientColorPickerLayout.Percent)
        {
          itemHeight -= 2;
        }

        // all colors except selected
        foreach (GradientColorPickerItem color in Colors)
        {
          if (color != SelectedColor)
          {
            DrawColor(g, color, x, y, arrowWidth, arrowHeight, itemWidth, itemHeight);
          }
        }

        // selected color to top
        if (SelectedColor != null)
        {
          DrawColor(g, SelectedColor, x, y, arrowWidth, arrowHeight, itemWidth, itemHeight);
        }
      }
    }

    /// <summary>
    /// Draws gradient.
    /// </summary>
    /// <param name="g">The <see cref="Graphics"/> instance.</param>
    [SuppressMessage("Microsoft.Reliability", "CA2000:DisposeObjectsBeforeLosingScope", Justification = "Reviewed.")]
    private void DrawGradient(Graphics g)
    {
      if (GradientLayout != GradientColorPickerLayout.None)
      {
        // canvas size
        int width = GradientRectangle.Width;
        int height = GradientRectangle.Height;

        // transparent background
        if (ShowTransparentBackground)
        {
          int cells = 0;
          int cellWidth = TransparentBackgroundSize;
          int cellHeight = TransparentBackgroundSize;
          int firstCellType = -1;
          int lastCellType = 0;

          var brush1 = new SolidBrush(TransparentBackgroundColor1);
          var brush2 = new SolidBrush(TransparentBackgroundColor2);

          int xstart = GradientRectangle.Left;
          int ystart = GradientRectangle.Top;
          int xend = width + xstart;
          int yend = height + ystart;

          for (int x = xstart; x < xend; x += cellWidth)
          {
            firstCellType = -1;
            cellHeight = cellWidth;

            if (x != xend && x + cellWidth > xend)
            {
              cellWidth = xend - x;
            }

            for (int y = ystart; y < yend; y += cellHeight)
            {
              if (y != yend && y + cellHeight > yend)
              {
                cellHeight = yend - y;
              }

              lastCellType = cells % 2;

              var b = lastCellType == 0 ? brush1 : brush2;

              g.FillRectangle(b, x, y, cellWidth, cellHeight);

              cells++;

              if (firstCellType == -1)
              {
                firstCellType = lastCellType;
              }
            }

            if (firstCellType != lastCellType)
            {
              cells++;
            }
          }

          brush1.Dispose();
          brush2.Dispose();
        }

        // gradient
        if (Colors.Count > 0)
        {
          var r = new Rectangle(Padding.Left, Padding.Top, width, height);

          using (var brush = new LinearGradientBrush(r, Color.White, Color.Black, LinearGradientMode.Horizontal))
          {
            brush.GammaCorrection = true;
            brush.InterpolationColors = Colors.GetColorBlend();

            g.FillRectangle(brush, r);
          }
        }
      }
    }

#if DEBUG

    /// <summary>
    /// Draws the positions info for debugging.
    /// </summary>
    /// <param name="g">The <see cref="Graphics"/> instance.</param>
    [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1115:ParameterMustFollowComma", Justification = "Reviewed.")]
    [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1117:ParametersMustBeOnSameLineOrSeparateLines", Justification = "Reviewed.")]
    [SuppressMessage("Microsoft.Reliability", "CA2000:DisposeObjectsBeforeLosingScope", Justification = "Reviewed.")]
    [SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", Justification = "Reviewed.")]
    [SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", Justification = "Reviewed.")]
    private void DrawPosition(Graphics g)
    {
      if (DrawColorPosition)
      {
        if (SelectedColor != null)
        {
          // position
          if (GradientLayout == GradientColorPickerLayout.FixedSize || GradientLayout == GradientColorPickerLayout.Percent)
          {
            var text = $"{Math.Round(SelectedColor.Position * 100, 2)}% / {SelectedColor.X}px";

            for (int i = 14; i > 6; i--)
            {
              var font = new Font(base.Font.FontFamily, i, FontStyle.Regular, GraphicsUnit.Pixel);
              var textSize = g.MeasureString(text, font);

              if (textSize.Width <= GradientRectangle.Width && textSize.Height <= GradientRectangle.Height)
              {
                g.DrawString
                (
                  text,
                  font,
                  Brushes.White,
                  GradientRectangle.X + ((GradientRectangle.Width - textSize.Width) / 2),
                  GradientRectangle.Y + ((GradientRectangle.Height - textSize.Height) / 2)
                );

                break;
              }

              font.Dispose();
            }
          }

          // offset point
          if (SelectedColor.Width > 4 && SelectedColor.Height > 12)
          {
            // canvas size and start position
            int arrowHeight = (int)(ColorRectangle.Height * ColorItemArrowSize);
            int y = SelectedColor.Y + SelectedColor.OffsetY;

            if (y <= ColorRectangle.Y + arrowHeight)
            {
              y = ColorRectangle.Y + arrowHeight + 2;
            }

            // draw offset point
            var b = new SolidBrush(Color.FromArgb(SelectedColor.Color.ToArgb() ^ 0xffffff));
            g.FillEllipse(b, SelectedColor.X + SelectedColor.OffsetX, y, 4, 4);
            b.Dispose();
          }
        }
        else if (SelectedColor == null && (GradientLayout == GradientColorPickerLayout.FixedSize || GradientLayout == GradientColorPickerLayout.Percent))
        {
          var text = $"{Math.Round((float)((lastMouseLocation.X * 100.0) / ColorRectangle.Width / 100.0), 2)}% / {lastMouseLocation.X}px";

          for (int i = 14; i > 6; i--)
          {
            var font = new Font(base.Font.FontFamily, i, FontStyle.Regular, GraphicsUnit.Pixel);
            var textSize = g.MeasureString(text, font);

            if (textSize.Width <= GradientRectangle.Width && textSize.Height <= GradientRectangle.Height)
            {
              g.DrawString
              (
                text,
                font,
                Brushes.White,
                GradientRectangle.X + ((GradientRectangle.Width - textSize.Width) / 2),
                GradientRectangle.Y + ((GradientRectangle.Height - textSize.Height) / 2)
              );
              break;
            }
          }
        }
      }
    }

#endif

    /// <summary>
    /// Moves the <see cref="SelectedColor"/>.
    /// </summary>
    /// <param name="location">The <see cref="Point"/> to move.</param>
    private void MoveItem(Point location)
    {
      if (SelectedColor == null)
      {
        if (CanAddColor && AllowAddColorByClick)
        {
          AddColor(location.X, lastColor);
        }
      }
      else
      {
        SelectedColor.SetPosition(location.X);
      }

      ColorMoved?.Invoke(this, EventArgs.Empty);
    }

    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Reviewed.")]
    private void OnColorChanged(object sender, EventArgs e)
    {
      var item = sender as GradientColorPickerItem;

      if (item != null)
      {
        lastColor = item.Color;

        ColorChanged?.Invoke(this, EventArgs.Empty);

        Refresh();
      }
    }

    /// <summary>
    /// Inserts color from clipboard into the list.
    /// </summary>
    /// <returns>In case of success, it returns <c>true</c>; otherwise, <c>false</c>.</returns>
    [SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "Reviewed.")]
    private bool PasteColorFromClipboard()
    {
      if (Clipboard.ContainsData("Color"))
      {
        var data = Clipboard.GetData("Color");

        if (data.GetType() == typeof(int))
        {
          try
          {
            AddColor(lastMouseLocation.X, Color.FromArgb((int)data));
            return true;
          }
          catch
          {
          }
        }
      }

      return false;
    }

    /// <summary>
    /// Selects next color.
    /// </summary>
    /// <param name="backward">Reverse mode.</param>
    private void SelectNextColor(bool backward)
    {
      if (SelectedColor != null)
      {
        GradientColorPickerItem next = null;

        if (backward)
        {
          // backward
          next = Colors.OrderByDescending(item => item.Position).FirstOrDefault(item => item.Position <= SelectedColor.Position && !item.Equals(SelectedColor));

          if (next == null)
          {
            next = Colors.OrderByDescending(item => item.Position).First();
          }
        }
        else
        {
          // forward
          next = Colors.OrderBy(item => item.Position).FirstOrDefault(item => item.Position >= SelectedColor.Position && !item.Equals(SelectedColor));

          if (next == null)
          {
            next = Colors.OrderBy(item => item.Position).First();
          }
        }

        SelectedColor = next;
      }
      else if (SelectedColor == null && Colors.Count > 0)
      {
        SelectedColor = Colors.OrderBy(item => item.Position).First();
      }
    }

    /// <summary>
    /// Shows color dialog for the <see cref="SelectedColor"/>.
    /// </summary>
    /// <remarks>
    /// <see cref="SelectedColor"/> cannot be <c>null</c>.
    /// </remarks>
    private void ShowColorDialog()
    {
      if (ColorDialog.ShowDialog() == DialogResult.OK)
      {
        SelectedColor.Color = ColorDialog.Color;

        ColorChanged?.Invoke(SelectedColor, EventArgs.Empty);
      }
    }

    /// <summary>
    /// Plays color list.
    /// </summary>
    private void TryPlay()
    {
      if (EES.Equals("PLAY", StringComparison.OrdinalIgnoreCase))
      {
        var orderedColors = Colors.OrderBy(item => item.Position).ToList();

        for (int i = 0; i < orderedColors.Count; i++)
        {
          var item = orderedColors[i];
          int f = (int)((item.Color.R * item.Color.G) / (item.Color.B * 0.01));

          if (f < 300)
          {
            f = 300;
          }
          else if (f > 3400)
          {
            f = 3400;
          }

          Console.Beep(f, 250);

          if (i > 0)
          {
            Threading.Thread.Sleep((int)((item.Position - orderedColors[i - 1].Position) * 1000));
          }
        }

        EE = false;
        EES = string.Empty;
      }
    }
  }
}
