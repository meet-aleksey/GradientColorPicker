//-----------------------------------------------------------------------
// <copyright file="GradientColorPickerItem.cs">
//   Copyright (c) Aleksey. All rights reserved.
// </copyright>
// <author>Aleksey</author>
//-----------------------------------------------------------------------
namespace System.Windows.Forms
{
  using System.ComponentModel;
  using System.Diagnostics.CodeAnalysis;
  using System.Drawing;

  /// <summary>
  /// Represents the color item.
  /// </summary>
  [ToolboxItem(false)]
  [DefaultProperty("Color")]
  [DefaultEvent("ColorChanged")]
  [DesignTimeVisible(false)]
  public class GradientColorPickerItem
  {
    /// <summary>
    /// Defines the DEFAULT_HEIGHT.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1310:FieldNamesMustNotContainUnderscore", Justification = "Reviewed.")]
    internal const int DEFAULT_HEIGHT = 18;

    /// <summary>
    /// Defines the DEFAULT_WIDTH.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.NamingRules", "SA1310:FieldNamesMustNotContainUnderscore", Justification = "Reviewed.")]
    internal const int DEFAULT_WIDTH = 12;

    /// <summary>
    /// Defines the color.
    /// </summary>
    private Color color = Color.Black;

    /// <summary>
    /// Defines the offsetX.
    /// </summary>
    private int offsetX = 0;

    /// <summary>
    /// Defines the offsetY.
    /// </summary>
    private int offsetY = 0;

    /// <summary>
    /// Defines the owner.
    /// </summary>
    private GradientColorPicker owner;

    /// <summary>
    /// Defines the position.
    /// </summary>
    private float position = 0;

    /// <summary>
    /// Initializes a new instance of the <see cref="GradientColorPickerItem"/> class.
    /// </summary>
    public GradientColorPickerItem()
    {
      Width = DEFAULT_WIDTH;
      Height = DEFAULT_HEIGHT;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="GradientColorPickerItem"/> class.
    /// </summary>
    /// <param name="owner">The <see cref="GradientColorPicker"/> instance.</param>
    /// <param name="color">The item <see cref="Color"/>.</param>
    public GradientColorPickerItem(GradientColorPicker owner, Color color) : this()
    {
      this.owner = owner ?? throw new ArgumentNullException("owner");

      this.color = color;

      Width = owner.ColorItemWidth;
      Height = owner.ColorItemHeight;
    }

    /// <summary>
    /// Occurs when the color of the item is changed.
    /// </summary>
    public event EventHandler ColorChanged;

    /// <summary>
    /// Occurs when the position of the item is changed.
    /// </summary>
    public event EventHandler PositionChanged;

    /// <summary>
    /// Gets or sets the color.
    /// </summary>
    [Category("Appearance")]
    [Description("Color of the item.")]
    [DefaultValue(typeof(Color), "Black")]
    [SuppressMessage("StyleCop.CSharp.LayoutRules", "SA1516:ElementsMustBeSeparatedByBlankLine", Justification = "Reviewed.")]
    [SuppressMessage("StyleCop.CSharp.LayoutRules", "SA1513:ClosingCurlyBracketMustBeFollowedByBlankLine", Justification = "Reviewed.")]
    public Color Color
    {
      get
      {
        return color;
      }
      set
      {
        Color old = color;

        color = value;

        if (old != color && ColorChanged != null)
        {
          ColorChanged(this, EventArgs.Empty);
        }
      }
    }

    /// <summary>
    /// Gets the owner of the item.
    /// </summary>
    [Browsable(false)]
    [SuppressMessage("StyleCop.CSharp.LayoutRules", "SA1516:ElementsMustBeSeparatedByBlankLine", Justification = "Reviewed.")]
    [SuppressMessage("StyleCop.CSharp.LayoutRules", "SA1513:ClosingCurlyBracketMustBeFollowedByBlankLine", Justification = "Reviewed.")]
    public GradientColorPicker Owner
    {
      get
      {
        return owner;
      }
      internal set
      {
        owner = value;

        Width = owner?.ColorItemWidth ?? Width;

        OnPositionChanged(false);
      }
    }

    /// <summary>
    /// Gets or sets the position. Value from 0 to 1.
    /// </summary>
    [Category("Behavior")]
    [Description("Position of the item. Value from 0 to 1 (0-100%).")]
    [DefaultValue(0)]
    [TypeConverter(typeof(GradientColorPercentValueFloat))]
    [SuppressMessage("StyleCop.CSharp.LayoutRules", "SA1516:ElementsMustBeSeparatedByBlankLine", Justification = "Reviewed.")]
    [SuppressMessage("StyleCop.CSharp.LayoutRules", "SA1513:ClosingCurlyBracketMustBeFollowedByBlankLine", Justification = "Reviewed.")]
    public float Position
    {
      get
      {
        return position;
      }
      set
      {
        if (value < 0)
        {
          value = 0;
        }
        else if (value > 1)
        {
          value = 1;
        }

        position = value;

        OnPositionChanged(true);
      }
    }

    /// <summary>
    /// Gets or sets the Tag
    /// </summary>
    [Category("Data")]
    [Description("Any data.")]
    [Localizable(false)]
    [DefaultValue(null)]
    [TypeConverter(typeof(StringConverter))]
    public object Tag { get; set; }

    /// <summary>
    /// Gets or sets a value indicating whether Active
    /// </summary>
    [Browsable(false)]
    internal bool Active { get; set; }

    /// <summary>
    /// Gets or sets the Height
    /// </summary>
    [Category("Appearance")]
    [Browsable(false)]
    [DefaultValue(18)]
    internal int Height { get; set; }

    /// <summary>
    /// Gets or sets the OffsetX
    /// </summary>
    [Browsable(false)]
    [SuppressMessage("StyleCop.CSharp.LayoutRules", "SA1516:ElementsMustBeSeparatedByBlankLine", Justification = "Reviewed.")]
    [SuppressMessage("StyleCop.CSharp.LayoutRules", "SA1513:ClosingCurlyBracketMustBeFollowedByBlankLine", Justification = "Reviewed.")]
    internal int OffsetX
    {
      get
      {
        return offsetX;
      }
      set
      {
        offsetX = Math.Abs(X - value);
      }
    }

    /// <summary>
    /// Gets or sets the OffsetY
    /// </summary>
    [Browsable(false)]
    [SuppressMessage("StyleCop.CSharp.LayoutRules", "SA1516:ElementsMustBeSeparatedByBlankLine", Justification = "Reviewed.")]
    [SuppressMessage("StyleCop.CSharp.LayoutRules", "SA1513:ClosingCurlyBracketMustBeFollowedByBlankLine", Justification = "Reviewed.")]
    internal int OffsetY
    {
      get
      {
        return offsetY;
      }
      set
      {
        offsetY = Math.Abs(Y - value);
      }
    }

    /// <summary>
    /// Gets or sets the Width
    /// </summary>
    [Browsable(false)]
    [Category("Appearance")]
    [DefaultValue(12)]
    internal int Width { get; set; }

    /// <summary>
    /// Gets the X.
    /// </summary>
    [Browsable(false)]
    internal int X { get; private set; }

    /// <summary>
    /// Gets the Y.
    /// </summary>
    [Browsable(false)]
    internal int Y { get; private set; }

    /// <summary>
    /// Sets the position by X location.
    /// </summary>
    /// <param name="x">The coordinates of point X.</param>
    [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Reviewed.")]
    public void SetPosition(int x)
    {
      SetPosition(x, Y);
    }

    /// <summary>
    /// Sets the position by X and Y location.
    /// </summary>
    /// <param name="x">The coordinates of point X.</param>
    /// <param name="y">The coordinates of point Y.</param>
    [SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly", Justification = "Reviewed.")]
    public void SetPosition(int x, int y)
    {
      Y = y;

      x -= OffsetX;

      if (x < 0)
      {
        Position = 0;
      }
      else if (x > Owner.ColorRectangle.Width - Width - 1)
      {
        Position = 1.0f;
      }
      else
      {
        Position = (float)((x * 100.0) / (Owner.ColorRectangle.Width - Width) / 100.0);
      }
    }

    /// <summary>
    /// Changes X and raises the <see cref="PositionChanged"/> event.
    /// </summary>
    /// <param name="invokeEvent">Indicates the need to call the <see cref="PositionChanged"/> event.</param>
    internal void OnPositionChanged(bool invokeEvent)
    {
      if (Owner != null)
      {
        int canvasWidth = Owner.ColorRectangle.Width;
        int newX = Convert.ToInt32((canvasWidth - Width) * position);

        if (newX >= canvasWidth - Width - 1)
        {
          newX = canvasWidth - Width - 1;
        }

        X = newX;

        Owner.Refresh();
      }

      if (invokeEvent)
      {
        PositionChanged?.Invoke(this, EventArgs.Empty);
      }
    }
  }
}
