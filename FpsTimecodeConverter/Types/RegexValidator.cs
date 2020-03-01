#region Title Header

// Name: Phillip Smith
// 
// Solution: FpsTimecodeConverter
// Project: FpsTimecodeConverter
// File Name: RegexValidator.cs
// 
// Current Data:
// 2020-03-01 1:36 PM
// 
// Creation Date:
// 2020-03-01 1:24 PM

#endregion

using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace FpsTimecodeConverter.Types
{
  public class RegexValidator : ValidationRule
  {
    public string Expression { get; set; }

    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
      return new Regex(Expression).IsMatch(value?.ToString() ?? throw new InvalidOperationException())
        ? ValidationResult.ValidResult
        : new ValidationResult(false, "Invalid input format");
    }
  }
}