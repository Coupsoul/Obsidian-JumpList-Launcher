namespace Obsidian_JumpList_Launcher.Models;

/// <summary>
/// Информация о хранилище Obsidian.
/// </summary>
/// <param name="Name">Отображаемое имя хранилища.</param>
/// <param name="Path">Полный путь к папке хранилища на диске.</param>
public record VaultInfo(string Name, string Path);
