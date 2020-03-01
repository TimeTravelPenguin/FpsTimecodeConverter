#region Title Header

// Name: Phillip Smith
// 
// Solution: FpsTimecodeConverter
// Project: FpsTimecodeConverter
// File Name: DecimalPrecision.cs
// 
// Current Data:
// 2020-03-01 1:36 PM
// 
// Creation Date:
// 2020-03-01 1:24 PM

#endregion

namespace FpsTimecodeConverter.Models
{
  internal class DecimalPrecision
  {
    public int PrecisionValue { get; set; }

    public DecimalPrecision(int precision)
    {
      PrecisionValue = precision;
    }

    public override string ToString()
    {
      return $"{PrecisionValue} d.p.";
    }

    public static explicit operator int(DecimalPrecision dp)
    {
      return dp.PrecisionValue;
    }
  }
}