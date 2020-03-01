#region Title Header

// Name: Phillip Smith
// 
// Solution: FpsTimecodeConverter
// Project: FpsTimecodeConverter
// File Name: FpsTimeCode.cs
// 
// Current Data:
// 2020-03-01 1:36 PM
// 
// Creation Date:
// 2020-03-01 1:24 PM

#endregion

using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;
using FpsTimecodeConverter.Types;

namespace FpsTimecodeConverter.Models
{
  internal class FpsTimeCode : PropertyChangedBase
  {
    private ObservableCollection<DecimalPrecision> _decimalPlacesCollection =
      new ObservableCollection<DecimalPrecision>();

    private double _defaultFps;
    private double _fps;
    private long _frameCount;
    private TimeSpan _rawTime;
    private DecimalPrecision _selectedItem;

    public DecimalPrecision SelectedItem
    {
      get => _selectedItem;
      set
      {
        SetValue(ref _selectedItem, value);
        OnPropertyChanged(nameof(TimeCode));
      }
    }

    public ObservableCollection<DecimalPrecision> DecimalPlacesCollection
    {
      get => _decimalPlacesCollection;
      set => SetValue(ref _decimalPlacesCollection, value);
    }

    public double DefaultFps
    {
      get => _defaultFps;
      set
      {
        if (value <= 0)
        {
          throw new InvalidOperationException("Default FPS must be greater than zero");
        }

        SetValue(ref _defaultFps, value);
      }
    }

    public string TimeCode
    {
      get => ConvertToTimecode();
      set => SetTimeCode(value);
    }

    public double Fps
    {
      get => _fps;
      set
      {
        if (value <= 0)
        {
          MessageBox.Show("FPS must be a positive integer", "FPS cannot be less than zero");
          value = DefaultFps;
        }

        if (value > 10000)
        {
          MessageBox.Show("I doubt you need to go beyond 10k fps", "FPS cannot be greater than 10,000");
          value = DefaultFps;
        }

        SetValue(ref _fps, value);
        OnPropertyChanged(nameof(TimeCode));
      }
    }

    public long FrameCount
    {
      get => _frameCount;
      set
      {
        SetValue(ref _frameCount, value);
        OnPropertyChanged(nameof(TimeCode));
      }
    }

    public FpsTimeCode(int fps, long frameCount) : this(fps, frameCount, fps)
    {
    }

    public FpsTimeCode(int fps, long frames, int defaultFps)
    {
      Fps = fps;
      FrameCount = frames;
      DefaultFps = defaultFps;

      SetDecimalPlaceChoices();
    }

    private void SetTimeCode(string timeCode)
    {
      var round = SelectedItem.PrecisionValue;
      var format = "{0:D" + $"{round}" + "}";

      var regSplit = Regex.Split(timeCode, ":");
      regSplit[3] = "0." + string.Format(CultureInfo.InvariantCulture, format, regSplit[3]);

      var numbers = Array.ConvertAll(regSplit, double.Parse);

      var hour = numbers[0];
      var min = numbers[1];
      var sec = numbers[2];
      var ms = numbers[3];

      var totalSec = 3600 * hour + 60 * min + sec + ms;

      FrameCount = (long) Math.Round(totalSec * Fps, MidpointRounding.AwayFromZero);
    }

    private string ConvertToTimecode()
    {
      var seconds = FrameCount / Fps;

      var maxFrames = Fps * TimeSpan.FromHours(10000).TotalSeconds;

      if (FrameCount > maxFrames)
      {
        MessageBox.Show("If you are exceeding 10,000 hours," + Environment.NewLine +
                        "you're a pretty bad speedrunner...", "World Record! (for the worst time)!",
          MessageBoxButton.OK, MessageBoxImage.Error, MessageBoxResult.OK);

        FrameCount = (long) maxFrames;
        return "Very slow";
      }

      _rawTime = TimeSpan.FromSeconds(seconds);

      var round = SelectedItem.PrecisionValue;
      var format = "{3:D" + $"{round}" + "}";

      return string.Format(CultureInfo.InvariantCulture, "{0:D2}:{1:D2}:{2:D2}:" + format, (int) _rawTime.TotalHours,
        _rawTime.Minutes, _rawTime.Seconds, (int) MilliComponent(seconds, round));
    }

    private static double MilliComponent(double seconds, int round)
    {
      var ms = seconds - Math.Truncate(seconds);
      return Math.Round(ms * Math.Pow(10, round), MidpointRounding.AwayFromZero);
    }

    private void SetDecimalPlaceChoices()
    {
      DecimalPlacesCollection.Clear();

      for (var i = 1; i < 6; i++)
      {
        DecimalPlacesCollection.Add(new DecimalPrecision(i));
      }

      SelectedItem = DecimalPlacesCollection[3];
    }
  }
}