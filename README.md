# FPS Timecode Converter
Convert your FPS value into an HH:MM:SS:MS format

![Application View](/ReadmeImages/MainWindow.png)

## How to use the application?
Using the application is easy. First, select:
#### 1. FPS
FPS is the measure of how many times in one second you video preview updates.

#### 2. Precision
Next, select the level of precision you want the millisecond value to round to.
I have arbitrarily capped the rounding to 5 decimal places, because in theory, you don't need more.
If you need more, you can solve it easily yourself.

#### 3. Enter frame count or timecode
Finally, input either your frame count to calculate a timecode, or input a timecode in HH:MM:SS:MS format
to convert it to a frame count. For the latter, you want to be as precise as possible, because if you input
a framecount that isn't possible, it will round the calculation to the nearest frame.

For example, at 60fps, the timecode will increment in values of 1/60 seconds, or 0.01667 seconds (rounded to 5 d.p.).
So, between times 0.01667 and 0.03333, no valid 60fps time exists. In this case, if you do happen to enter
a non-valid timecode, it will operate as normal, calculate the framecount, and round to the NEAREST integer.

## Computer requirement
The necessary system requirements are:
- Windows 10
- [.NET Framework 4.7.2](https://dotnet.microsoft.com/download/dotnet-framework)

To find out more information regarding minimum OS versions, [you can find more information here](https://docs.microsoft.com/en-us/dotnet/core/install/dependencies)

## Download
[Click here to goto the download page](https://github.com/TimeTravelPenguin/FpsTimecodeConverter/releases)
