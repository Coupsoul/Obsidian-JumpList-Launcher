using System.IO;
using Microsoft.Win32;

namespace Obsidian_JumpList_Launcher.Services;

/// <summary>
/// Отвечает за поиск установленного экземпляра Obsidian в системе.
/// </summary>
public class ObsidianLocator
{
    /// <summary>
    /// Ищет путь к Obsidian.exe через реестр Windows.
    /// </summary>
    public string GetObsidianPath()
    {
        string? regValue = Registry.GetValue(@"HKEY_CLASSES_ROOT\obsidian\shell\open\command","", null) as string;
        if (string.IsNullOrWhiteSpace(regValue)) return string.Empty;

        int exeIndex = regValue.IndexOf(".exe", StringComparison.OrdinalIgnoreCase);
        if (exeIndex == -1) return string.Empty;

        int cutIndex = exeIndex + 4;
        string path = regValue[..cutIndex].Trim(' ', '\"');

        return File.Exists(path) ? path : string.Empty;
    }
}
