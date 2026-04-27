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
    public IEnumerable<RecentNote> GetRecentNotes(IEnumerable<VaultInfo> vaults) => [];
}
