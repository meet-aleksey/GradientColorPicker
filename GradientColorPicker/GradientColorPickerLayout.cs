//-----------------------------------------------------------------------
// <copyright file="GradientColorPickerLayout.cs">
//   Copyright (c) Aleksey. All rights reserved.
// </copyright>
// <author>Aleksey</author>
//-----------------------------------------------------------------------
namespace System.Windows.Forms
{
  /// <summary>
  /// List of mode for drawing a gradient.
  /// </summary>
  public enum GradientColorPickerLayout
  {
    /// <summary>
    /// Without drawing a gradient.
    /// </summary>
    None,

    /// <summary>
    /// Fill the background of the control.
    /// </summary>
    Background,

    /// <summary>
    /// The relative size of the gradient.
    /// </summary>
    Percent,

    /// <summary>
    /// The fixed size of the gradient.
    /// </summary>
    FixedSize
  }
}
