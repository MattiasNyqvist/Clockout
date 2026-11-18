using System.Globalization;

namespace WorkTimeCalculator.Wasm.Services;

public static class TimeFormat
{
    public static string Example(TimeSystem system) =>
        system == TimeSystem.Decimal ? "ex. 7.7 eller 7,7" : "ex. 7.30 eller 7:30";

    /// <summary>Tolkar arbetstid enligt valt system till minuter. Returnerar false vid ogiltigt värde.</summary>
    public static bool TryParse(string input, TimeSystem system, out int minutes)
    {
        minutes = 0;
        string text = (input ?? "").Trim().Replace(',', '.').Replace(':', '.');

        if (system == TimeSystem.Decimal)
        {
            if (!double.TryParse(text, NumberStyles.Float, CultureInfo.InvariantCulture, out double hours) || hours < 0)
                return false;
            minutes = (int)Math.Round(hours * 60);
            return true;
        }

        var parts = text.Split('.');
        if (parts.Length > 2
            || !int.TryParse(parts[0], out int h) || h < 0
            || !int.TryParse(parts.Length == 2 ? parts[1].PadRight(2, '0') : "0", out int m)
            || m is < 0 or > 59)
            return false;
        minutes = h * 60 + m;
        return true;
    }

    /// <summary>Formaterar minuter enligt valt system, t.ex. 462 -> "7,7" (decimal) eller "7:42" (klocka).</summary>
    public static string Format(int minutes, TimeSystem system)
    {
        if (system == TimeSystem.Decimal)
            return Math.Round(minutes / 60.0, 2).ToString("0.##", CultureInfo.GetCultureInfo("sv-SE"));
        return $"{minutes / 60}:{minutes % 60:00}";
    }

    /// <summary>Veckonummer: ISO-8601 för måndag som veckostart, annars söndag (USA-stil).</summary>
    public static int WeekNumber(DateTime date, WeekStart start) =>
        start == WeekStart.Monday
            ? ISOWeek.GetWeekOfYear(date)
            : CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(date, CalendarWeekRule.FirstDay, DayOfWeek.Sunday);
}
