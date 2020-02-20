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