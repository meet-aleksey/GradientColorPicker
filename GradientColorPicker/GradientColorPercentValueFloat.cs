//-----------------------------------------------------------------------
// <copyright file="GradientColorPercentValueFloat.cs">
//   Copyright (c) Aleksey. All rights reserved.
// </copyright>
// <author>Aleksey</author>
//-----------------------------------------------------------------------
namespace System.Windows.Forms
{
  using System.ComponentModel;
  using System.Diagnostics.CodeAnalysis;
  using System.Globalization;

  /// <summary>
  /// Defines the <see cref="GradientColorPercentValueFloat" />
  /// </summary>
  public class GradientColorPercentValueFloat : TypeConverter
  {
    /// <summary>
    /// Determines if this converter can convert an object in the given source type to the native type of the converter.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1611:ElementParametersMustBeDocumented", Justification = "Reviewed.")]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1615:ElementReturnValueMustBeDocumented", Justification = "Reviewed.")]
    public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
    {
      if (sourceType == typeof(string))
      {
        return true;
      }

      return base.CanConvertFrom(context, sourceType);
    }

    /// <summary>
    /// Converts the given object to the converter's native type.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1611:ElementParametersMustBeDocumented", Justification = "Reviewed.")]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1615:ElementReturnValueMustBeDocumented", Justification = "Reviewed.")]
    [SuppressMessage("Microsoft.Globalization", "CA1307:SpecifyStringComparison", Justification = "Reviewed.")]
    public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
    {
      string valueString = value as string;

      if (!string.IsNullOrEmpty(valueString))
      {
        string text = valueString.Replace('%', ' ').Trim();
        float val = float.Parse(text, CultureInfo.CurrentCulture);
        bool hasPercent = valueString.IndexOf("%") != -1;
        float percent = 1.0f;

        if (hasPercent && (val >= 0.0 && val <= 1.0))
        {
          val /= 100.0f;
          text = val.ToString(CultureInfo.CurrentCulture);
        }

        try
        {
          percent = (float)TypeDescriptor.GetConverter(typeof(float)).ConvertFrom(context, culture, text);

          if (percent > 1.0)
          {
            percent /= 100.0f;
          }
        }
        catch
        {
          throw new FormatException();
        }

        if (percent < 0.0 || percent > 1.0)
        {
          throw new FormatException();
        }

        return percent;
      }

      return base.ConvertFrom(context, culture, value);
    }

    /// <summary>
    /// Converts the given object to another type.  The most common types to convert
    /// are to and from a string object. The default implementation will make a call
    /// to ToString on the object if the object is valid and if the destination
    /// type is string.  If this cannot convert to the desitnation type, this will
    /// throw a <see cref="NotSupportedException"/>.
    /// </summary>
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1611:ElementParametersMustBeDocumented", Justification = "Reviewed.")]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1615:ElementReturnValueMustBeDocumented", Justification = "Reviewed.")]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1650:ElementDocumentationMustBeSpelledCorrectly", Justification = "Reviewed.")]
    [SuppressMessage("Microsoft.Globalization", "CA1305:SpecifyIFormatProvider", Justification = "Reviewed.")]
    public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
    {
      if (destinationType == null)
      {
        throw new ArgumentNullException("destinationType");
      }

      if (destinationType == typeof(string))
      {
        float percent = (float)Math.Round((float)value * 100.0f, 2);

        return $"{percent.ToString(CultureInfo.CurrentCulture)}%";
      }

      return base.ConvertTo(context, culture, value, destinationType);
    }
  }
}
