//-----------------------------------------------------------------------
// <copyright file="GradientColorPickerItemCollection.cs">
//   Copyright (c) Aleksey. All rights reserved.
// </copyright>
// <author>Aleksey</author>
//-----------------------------------------------------------------------
namespace System.Windows.Forms
{
  using System.Collections;
  using System.Collections.Generic;
  using System.ComponentModel;
  using System.Diagnostics.CodeAnalysis;
  using System.Drawing;
  using System.Drawing.Drawing2D;
  using System.Linq;

  /// <summary>
  /// Represents the list of color items.
  /// </summary>
  public class GradientColorPickerItemCollection : IEnumerable<GradientColorPickerItem>,
    IList<GradientColorPickerItem>, ICollection<GradientColorPickerItem>,
    IEnumerable, IList, ICollection
  {
    /// <summary>
    /// Defines color items.
    /// </summary>
    private List<GradientColorPickerItem> items = new List<GradientColorPickerItem>();

    /// <summary>
    /// Initializes a new instance of the <see cref="GradientColorPickerItemCollection"/> class.
    /// </summary>
    /// <param name="owner">The <see cref="GradientColorPicker"/></param>
    public GradientColorPickerItemCollection(GradientColorPicker owner)
    {
      Owner = owner ?? throw new ArgumentNullException("owner");
    }

    /// <summary>
    /// Occurs when the color of the item is changed.
    /// </summary>
    public event EventHandler ColorChanged;

    /// <summary>
    /// Gets the number of items.
    /// </summary>
    public int Count => items.Count;

    /// <summary>
    /// Gets a value indicating whether the list has a fixed size.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool IsFixedSize => false;

    /// <summary>
    /// Gets a value indicating whether the list is read-only.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool IsReadOnly => false;

    /// <summary>
    /// Gets a value indicating whether access to the collection is synchronized (thread safe).
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public bool IsSynchronized => false;

    /// <summary>
    /// Gets the owner of the list.
    /// </summary>
    public GradientColorPicker Owner { get; internal set; }

    /// <summary>
    /// Gets an object that can be used to synchronize access to the collection.
    /// </summary>
    [EditorBrowsable(EditorBrowsableState.Never)]
    public object SyncRoot => null;

    /// <summary>
    /// Gets or sets the element at the specified index.
    /// </summary>
    /// <param name="index">The zero-based index of the element to get or set.</param>
    /// <returns>The element at the specified index.</returns>
    [SuppressMessage("StyleCop.CSharp.LayoutRules", "SA1516:ElementsMustBeSeparatedByBlankLine", Justification = "Reviewed.")]
    [SuppressMessage("StyleCop.CSharp.LayoutRules", "SA1513:ClosingCurlyBracketMustBeFollowedByBlankLine", Justification = "Reviewed.")]
    object IList.this[int index]
    {
      get
      {
        return items[index];
      }
      set
      {
        if (!IsGradientColorPickerItem(value))
        {
          throw new ArgumentException("Expected type is GradientColorPickerItem.");
        }

        items[index] = value as GradientColorPickerItem;
      }
    }

    /// <summary>
    /// Gets or sets the element at the specified index.
    /// </summary>
    /// <param name="index">The zero-based index of the element to get or set.</param>
    /// <returns>The element at the specified index.</returns>
    [SuppressMessage("StyleCop.CSharp.LayoutRules", "SA1516:ElementsMustBeSeparatedByBlankLine", Justification = "Reviewed.")]
    [SuppressMessage("StyleCop.CSharp.LayoutRules", "SA1513:ClosingCurlyBracketMustBeFollowedByBlankLine", Justification = "Reviewed.")]
    public GradientColorPickerItem this[int index]
    {
      get
      {
        return items[index];
      }
      set
      {
        items[index] = value;
      }
    }

    /// <summary>
    /// Activates color by location.
    /// </summary>
    /// <param name="location">Location for color search.</param>
    /// <returns>The <see cref="GradientColorPickerItem"/> instance.</returns>
    public GradientColorPickerItem ActiveColorByLocation(Point location)
    {
      if (Count == 0)
      {
        return null;
      }

      GradientColorPickerItem result = null;
      int x = location.X - Owner.ColorRectangle.X;
      int y = location.Y - Owner.ColorRectangle.Y;
      bool actived = false;

      for (int i = Count - 1; i >= 0; i--)
      {
        var item = this[i];

        if (x >= item.X && x <= item.X + item.Width && y >= item.Y && y <= item.Y + item.Height)
        {
          item.Active = !actived;

          if (result == null)
          {
            result = item;
          }

          actived = true;
        }
        else
        {
          item.Active = false;
        }
      }

      Owner?.Refresh();

      return result;
    }

    /// <summary>
    /// Adds a color item to the list.
    /// </summary>
    /// <param name="item">The <see cref="GradientColorPickerItem"/> instance to add to the list.</param>
    public void Add(GradientColorPickerItem item)
    {
      if (item == null)
      {
        throw new ArgumentNullException("item");
      }

      // set owner
      item.Owner = item.Owner ?? Owner;

      // add color changed handler
      item.ColorChanged += ColorChanged;

      // add to collection
      items.Add(item);

      Owner?.Refresh();
    }

    /// <summary>
    /// Adds a color item to the list.
    /// </summary>
    /// <param name="value">The <see cref="GradientColorPickerItem"/> instance to add to the list.</param>
    /// <returns>The position into which the new element was inserted, or -1 to indicate that the item was not inserted into the collection.</returns>
    public int Add(object value)
    {
      if (!IsGradientColorPickerItem(value))
      {
        throw new ArgumentException("Expected type is GradientColorPickerItem.", "value");
      }

      Add(value as GradientColorPickerItem);

      return items.Count;
    }

    /// <summary>
    /// Removes all items from the list.
    /// </summary>
    public void Clear()
    {
      items.Clear();
      Owner?.Refresh();
    }

    /// <summary>
    /// Determines whether the list contains a specific color item.
    /// </summary>
    /// <param name="item">The <see cref="GradientColorPickerItem"/> instance to locate in the list.</param>
    /// <returns><c>true</c> if the <see cref="GradientColorPickerItem"/> is found in the list; otherwise, <c>false</c>.</returns>
    public bool Contains(GradientColorPickerItem item)
    {
      return items.Contains(item);
    }

    /// <summary>
    /// Determines whether the list contains a specific color item.
    /// </summary>
    /// <param name="value">The <see cref="GradientColorPickerItem"/> instance to locate in the list.</param>
    /// <returns><c>true</c> if the <see cref="GradientColorPickerItem"/> is found in the list; otherwise, <c>false</c>.</returns>
    public bool Contains(object value)
    {
      if (!IsGradientColorPickerItem(value))
      {
        throw new ArgumentException("Expected type is GradientColorPickerItem.", "value");
      }

      return Contains(value as GradientColorPickerItem);
    }

    /// <summary>
    /// Copies the colors of the list to an Array, starting at a particular Array index.
    /// </summary>
    /// <param name="array">The one-dimensional Array that is the destination of the elements copied from ICollection. The Array must have zero-based indexing.</param>
    /// <param name="arrayIndex">The zero-based index in array at which copying begins.</param>
    public void CopyTo(GradientColorPickerItem[] array, int arrayIndex)
    {
      items.CopyTo(array, arrayIndex);
    }

    /// <summary>
    /// Copies the colors of the list to an Array, starting at a particular Array index.
    /// </summary>
    /// <param name="array">The one-dimensional Array that is the destination of the elements copied from ICollection. The Array must have zero-based indexing.</param>
    /// <param name="index">The zero-based index in array at which copying begins.</param>
    public void CopyTo(Array array, int index)
    {
      CopyTo(array.Cast<GradientColorPickerItem>().ToArray(), index);
    }

    /// <summary>
    /// Returns <see cref="ColorBlend"/> of the current list.
    /// </summary>
    /// <returns>The <see cref="ColorBlend"/> instance.</returns>
    [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate", Justification = "Reviewed.")]
    public ColorBlend GetColorBlend()
    {
      var sortedItems = this.OrderBy(item => item.X);

      return new ColorBlend
      {
        Colors = GetColors(sortedItems),
        Positions = GetPositions(sortedItems)
      };
    }

    /// <summary>
    /// Returns array of <see cref="Color"/> of the current list.
    /// </summary>
    /// <returns>The <see cref="Color"/> array.</returns>
    public Color[] GetColors()
    {
      return GetColors(this.OrderBy(item => item.X));
    }

    /// <summary>
    /// Returns an enumerator that iterates through a collection.
    /// </summary>
    /// <returns>The <see cref="IEnumerator{GradientColorPickerItem}"/>.</returns>
    public IEnumerator<GradientColorPickerItem> GetEnumerator()
    {
      return items.GetEnumerator();
    }

    /// <summary>
    /// Returns an enumerator that iterates through a collection.
    /// </summary>
    /// <returns>The <see cref="IEnumerator"/>.</returns>
    IEnumerator IEnumerable.GetEnumerator()
    {
      return items.GetEnumerator();
    }

    /// <summary>
    /// Returns positions of colors.
    /// </summary>
    /// <returns>The <see cref="float"/> array.</returns>
    public float[] GetPositions()
    {
      return GetPositions(this.OrderBy(item => item.X));
    }

    /// <summary>
    /// Returns true if there is a color in the specified location.
    /// </summary>
    /// <param name="x1">The beginning of the range.</param>
    /// <param name="x2">End of range.</param>
    /// <returns>Returns <c>true</c> if there is at least one color in the specified range; otherwise, <c>false</c>.</returns>
    public bool HasColor(int x1, int x2)
    {
      foreach (GradientColorPickerItem color in this)
      {
        if ((x1 >= color.X && x1 <= color.X + color.Width) || (x2 >= color.X && x2 <= color.X + color.Width))
        {
          return true;
        }
      }

      return false;
    }

    /// <summary>
    /// Determines the index of a specific item in the list.
    /// </summary>
    /// <param name="item">The <see cref="GradientColorPickerItem"/> to locate in the list.</param>
    /// <returns>The index of value if found in the list; otherwise, -1.</returns>
    public int IndexOf(GradientColorPickerItem item)
    {
      return items.IndexOf(item);
    }

    /// <summary>
    /// Determines the index of a specific item in the list.
    /// </summary>
    /// <param name="value">The <see cref="GradientColorPickerItem"/> to locate in the list.</param>
    /// <returns>The index of value if found in the list; otherwise, -1.</returns>
    public int IndexOf(object value)
    {
      if (!IsGradientColorPickerItem(value))
      {
        throw new ArgumentException("Expected type is GradientColorPickerItem.", "value");
      }

      return IndexOf(value as GradientColorPickerItem);
    }

    /// <summary>
    /// Inserts an color to the list at the specified index.
    /// </summary>
    /// <param name="index">The zero-based index at which <paramref name="item"/> should be inserted.</param>
    /// <param name="item">The <see cref="GradientColorPickerItem"/> to insert into the list.</param>
    public void Insert(int index, GradientColorPickerItem item)
    {
      if (item == null)
      {
        throw new ArgumentNullException("item");
      }

      // set owner
      item.Owner = item.Owner ?? Owner;

      // add color changed handler
      item.ColorChanged += ColorChanged;

      // insert to collection
      items.Insert(index, item);

      Owner?.Refresh();
    }

    /// <summary>
    /// Inserts an color to the list at the specified index.
    /// </summary>
    /// <param name="index">The zero-based index at which <paramref name="value"/> should be inserted.</param>
    /// <param name="value">The <see cref="GradientColorPickerItem"/> to insert into the list.</param>
    public void Insert(int index, object value)
    {
      if (!IsGradientColorPickerItem(value))
      {
        throw new ArgumentException("Expected type is GradientColorPickerItem.", "value");
      }

      Insert(index, value as GradientColorPickerItem);
    }

    /// <summary>
    /// Removes the first occurrence of a specific object from the list.
    /// </summary>
    /// <param name="item">The <see cref="GradientColorPickerItem"/> instance to remove.</param>
    /// <returns><c>true</c> if color item is removed; otherwise, <c>false</c>.</returns>
    public bool Remove(GradientColorPickerItem item)
    {
      if (!items.Remove(item))
      {
        return false;
      }

      Owner?.Refresh();

      return true;
    }

    /// <summary>
    /// Removes the first occurrence of a specific object from the list.
    /// </summary>
    /// <param name="item">The <see cref="GradientColorPickerItem"/> instance to remove.</param>
    /// <returns><c>true</c> if color item is removed; otherwise, <c>false</c>.</returns>
    bool ICollection<GradientColorPickerItem>.Remove(GradientColorPickerItem item)
    {
      return Remove(item);
    }

    /// <summary>
    /// Removes the first occurrence of a specific object from the list.
    /// </summary>
    /// <param name="value">The <see cref="GradientColorPickerItem"/> instance to remove.</param>
    public void Remove(object value)
    {
      if (!IsGradientColorPickerItem(value))
      {
        throw new ArgumentException("Expected type is GradientColorPickerItem.", "value");
      }

      Remove(value as GradientColorPickerItem);
    }

    /// <summary>
    /// Removes the list item at the specified index.
    /// </summary>
    /// <param name="index">The zero-based index of the item to remove.</param>
    public void RemoveAt(int index)
    {
      items.RemoveAt(index);
      Owner?.Refresh();
    }

    /// <summary>
    /// Removes a range of elements from the list.
    /// </summary>
    /// <param name="index">The zero-based starting index of the range of elements to remove.</param>
    /// <param name="count">The number of elements to remove.</param>
    public void RemoveRange(int index, int count)
    {
      items.RemoveRange(index, count);
      Owner?.Refresh();
    }

    /// <summary>
    /// Sorts items in the list by location.
    /// </summary>
    public void Sort()
    {
      items.Sort((a, b) => a.X.CompareTo(b.X));
    }

    /// <summary>
    /// Sets active state of the color item.
    /// </summary>
    /// <param name="color">The <see cref="GradientColorPickerItem"/> instance to set the active state.</param>
    internal void ActiveColor(GradientColorPickerItem color)
    {
      if (color == null)
      {
        foreach (var item in items)
        {
          item.Active = false;
        }
      }
      else
      {
        for (int i = Count - 1; i >= 0; i--)
        {
          this[i].Active = this[i] == color;
        }
      }

      Owner?.Refresh();
    }

    /// <summary>
    /// Returns <see cref="Color"/> array of the current list.
    /// </summary>
    /// <param name="sortedItems">The sorted list.</param>
    /// <returns>The <see cref="Color"/> array of the current list.</returns>
    private static Color[] GetColors(IOrderedEnumerable<GradientColorPickerItem> sortedItems)
    {
      var result = new List<Color>();

      foreach (GradientColorPickerItem item in sortedItems)
      {
        result.Add(item.Color);
      }

      if (result.Count > 0)
      {
        result.Insert(0, result[0]);
        result.Add(result[result.Count - 1]);
      }

      return result.ToArray();
    }

    /// <summary>
    /// Returns positions of colors.
    /// </summary>
    /// <param name="sortedItems">Sorted colors.</param>
    /// <returns>Positions list.</returns>
    private static float[] GetPositions(IOrderedEnumerable<GradientColorPickerItem> sortedItems)
    {
      var result = new List<float>();

      foreach (GradientColorPickerItem item in sortedItems)
      {
        result.Add(item.Position);
      }

      if (result.Count > 0)
      {
        result.Insert(0, 0.0f);
        result.Add(1.0f);
      }

      return result.ToArray();
    }

    /// <summary>
    /// The IsGradientColorPickerItem
    /// </summary>
    /// <param name="value">The <see cref="object"/></param>
    /// <returns>The <see cref="bool"/></returns>
    private static bool IsGradientColorPickerItem(object value)
    {
      return value != null && value.GetType() == typeof(GradientColorPickerItem);
    }
  }
}
