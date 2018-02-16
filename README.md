# GradientColorPicker Control for Windows Forms
[![Visual Studio](https://img.shields.io/badge/Visual%20Studio-2010%20%2F%202017-68217a.svg?style=flat)](https://www.visualstudio.com/)
[![Windows Forms](https://img.shields.io/badge/Windows%20Forms--68217a.svg?style=flat)](https://docs.microsoft.com/en-us/dotnet/framework/winforms/)
[![.NET Framework](https://img.shields.io/badge/.NET-v3.5%20%2F%20v4.7-68217a.svg?style=flat)](https://www.microsoft.com/net/download)
[![License](https://img.shields.io/github/license/meet-aleksey/GradientColorPicker.svg?style=flat&label=License)](https://github.com/meet-aleksey/GradientColorPicker/blob/master/LICENSE)
[![NuGet](https://img.shields.io/nuget/dt/GradientColorPicker.svg?style=flat&label=Downloads)](https://www.nuget.org/packages/GradientColorPicker/)

This is control for Windows Forms, which allows to select colors to create gradients.

![Preview](preview.png)

## Install

To install GradientColorPicker control, run the following command in the Package Manager Console:

```
Install-Package GradientColorPicker
```

## How to use

Install the GradientColorPicker package in your Windows Forms project.

Or [download an archive file containing binary assemblies](https://github.com/meet-aleksey/GradientColorPicker/releases), 
unpack it and add to your project a reference to the assembly of the version of .NET Framework that you are using.

Now you can create an instance of the GradientColorPicker and add to the form:

```C#
// create a new instance of GradientColorPicker
var gradientColorPicker = new GradientColorPicker();

// for example, set the minimum number of items (colors)
gradientColorPicker.MinimumColorCount = 4;

// for example, randomize the postion and color of the items
gradientColorPicker1.Randomize(true, true);

// add the GradientColorPicker to the form
Controls.Add(gradientColorPicker);

// use the DrawLinearGradientToImage method to drawing linear gradient image
// for example, draw a gradient in the background image of the form

// create image 
BackgroundImage = new Bitmap(ClientSize.Width, ClientSize.Height);
// draw a linear gradient
gradientColorPicker1.DrawLinearGradientToImage(BackgroundImage);

// or DrawRadialGradientToImage method  to drawing radial gradient image

// we need to specify a central point
var centerPoint = new PointF(ClientSize.Width / 2, ClientSize.Height / 2);
// draw a radial gradient
gradientColorPicker1.DrawRadialGradientToImage(BackgroundImage, ClientRectangle, centerPoint);
```

If you can not find the control on the toolbox in designer mode, you need to add GradientColorPicker to the toolbox:

[![How to add a control to the Visual Studio toolbox](youtube.jpg)](https://www.youtube.com/watch?v=Zasmw8zfIbI)

1. Right-click on toolbox
2. Select "Choose Items"
3. Browse the GradientColorPicker assembly on your computer.
4. Add the item.
5. Enjoy!

## Requirements

* .NET Framework 3.5/4.0/4.5/4.6/4.7 or later;
* Windows Forms.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

Copyright © 2018, [@meet-aleksey](https://github.com/meet-aleksey)