using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows;
using FramerateTimestampConverter.WPF.Models;
using Microsoft.Expression.Interactivity.Core;

namespace FramerateTimestampConverter.WPF.ViewModels
{
  internal class MainAppViewModel
  {
    public FpsTimeCode FpsTime { get; } = new FpsTimeCode(60, 0);
    public ActionCommand OpenGitHub { get; } = new ActionCommand(LaunchGitHub);

    private static void LaunchGitHub()
    {
      var url = @"https://www.google.com";

      try
      {
        Process.Start(url);
      }
      catch
      {
        // hack because of this: https://github.com/dotnet/corefx/issues/10361
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
          url = url.Replace("&", "^&", StringComparison.InvariantCulture);
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
          MessageBox.Show("Check out the GitHub:" + Environment.NewLine + url, "Unable to automatically launch web browser");
        }
      }
    }
  }
}