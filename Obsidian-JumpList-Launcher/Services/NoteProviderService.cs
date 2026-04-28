using System.IO;
using System.Text.Json;
using Obsidian_JumpList_Launcher.Models;

namespace Obsidian_JumpList_Launcher.Services;

/// <summary>
/// Сервис для сбора последних открытых заметок из всех доступных хранилищ.
/// </summary>
public class NoteProviderService
{
    /// <summary>
    /// Собирает данные из .obsidian/workspace.json в каждом хранилище.
    /// </summary>
    public IEnumerable<RecentNote> GetRecentNotes(IEnumerable<VaultInfo> vaults)
    {
        var recentNotes = new List<RecentNote>();

        foreach (var v in vaults)
        {
            string workspacePath = Path.Combine(v.Path, ".obsidian", "workspace.json");
            if (!File.Exists(workspacePath)) continue;

            string jsonContent = File.ReadAllText(workspacePath);
            var relativePaths = JsonSerializer.Deserialize<NotesPaths>(jsonContent);
            if (relativePaths == null) continue;

            var recentForVault = new List<RecentNote>();

            foreach (var relativePath in relativePaths.lastOpenFiles)
            {
                if (recentForVault.Count == App.MAX_RECENT_NOTES) break;

                var noteFullPath = Path.Combine(v.Path, relativePath);
                if (!File.Exists(noteFullPath)) continue;

                recentForVault.Add(new RecentNote(Path.GetFileName(noteFullPath), noteFullPath, v.Name, File.GetLastWriteTime(noteFullPath)));
            }

            recentNotes.AddRange(recentForVault);
        }

        return recentNotes
            .OrderByDescending(n => n.LastModified)
            .Take(App.MAX_RECENT_NOTES)
            .ToList();
    }

    private record NotesPaths(IEnumerable<string> lastOpenFiles);
}
