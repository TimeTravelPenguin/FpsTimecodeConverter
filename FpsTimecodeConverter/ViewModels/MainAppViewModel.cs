﻿#region Title Header

// Name: Phillip Smith
// 
// Solution: FpsTimecodeConverter
// Project: FpsTimecodeConverter
// File Name: MainAppViewModel.cs
// 
// Current Data:
// 2020-03-01 1:36 PM
// 
// Creation Date:
// 2020-03-01 1:24 PM

#endregion

using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows;
using FpsTimecodeConverter.Models;
using Microsoft.Expression.Interactivity.Core;

namespace FpsTimecodeConverter.ViewModels
{
  internal class MainAppViewModel
  {
    public FpsTimeCode FpsTime { get; } = new FpsTimeCode(60, 0);
    public ActionCommand OpenGitHub { get; } = new ActionCommand(LaunchGitHub);

    private static void LaunchGitHub()
    {
      var url = @"https://github.com/TimeTravelPenguin/FpsTimecodeConverter";

      try
      {
        Process.Start(url);
      }
      catch
      {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
          url = url.Replace("&", "^&");
          Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") {CreateNoWindow = true});
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
          Process.Start("xdg-open", url);
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
          Process.Start("open", url);
        }
        else
        {
          MessageBox.Show("Check out the GitHub:" + Environment.NewLine + url,
            "Unable to automatically launch web browser");
        }
      }
    }
  }
}