using System.IO;
using System.Text.Json;
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
    public IEnumerable<VaultInfo> GetVaults()
    {
        string appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        string configPath = Path.Combine(appDataPath, "obsidian", "obsidian.json");
        if (!File.Exists(configPath)) return [];

        string jsonContent = File.ReadAllText(configPath);

        var config = JsonSerializer.Deserialize<ObsidianConfig>(jsonContent);
        if (config == null) return [];

        var vaultsInfo = new List<VaultInfo>();

        foreach (var vault in config.vaults)
        {
            string vaultPath = vault.Value.path;
            string vaultName = Path.GetFileName(vaultPath);

            vaultsInfo.Add(new VaultInfo(vaultName, vaultPath));
        }

        return vaultsInfo;
    }

    private record ObsidianVault(string path);

    private record ObsidianConfig(Dictionary<string, ObsidianVault> vaults);
}
