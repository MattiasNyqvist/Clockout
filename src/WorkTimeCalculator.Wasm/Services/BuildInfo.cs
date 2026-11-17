using System.Reflection;

namespace WorkTimeCalculator.Wasm.Services;

/// <summary>
/// Versionsinformation som visas i appens sidfot.
/// Värdena sätts vid bygget i WorkTimeCalculator.Wasm.csproj och läses här från assemblyn.
/// </summary>
public static class BuildInfo
{
    /// <summary>Appens versionsnummer, t.ex. "1.0".</summary>
    public static string Version => ReadMetadata("AppVersion");

    /// <summary>Bygge i formatet "datum.löpnummer", t.ex. "20261117.12".</summary>
    public static string Build => ReadMetadata("BuildNumber");

    private static string ReadMetadata(string key) =>
        typeof(BuildInfo).Assembly
            .GetCustomAttributes<AssemblyMetadataAttribute>()
            .FirstOrDefault(attribute => attribute.Key == key)?.Value ?? "?";
}
