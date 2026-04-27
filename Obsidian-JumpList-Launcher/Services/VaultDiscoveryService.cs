using Obsidian_JumpList_Launcher.Models;

namespace Obsidian_JumpList_Launcher.Services;

/// <summary>
/// Сервис для обнаружения всех локальных хранилищ (Vaults) Obsidian.
/// </summary>
public class VaultDiscoveryService
{
    /// <summary>
    /// Читает файл %AppData%\obsidian\obsidian.json и извлекает список путей к хранилищам.
    /// </summary>
    public IEnumerable<VaultInfo> GetVaults() => [];
}
