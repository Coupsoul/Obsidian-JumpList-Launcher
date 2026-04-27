using System;

namespace Obsidian_JumpList_Launcher.Models;

/// <summary>
/// Информация о конкретной заметке для отображения в JumpList.
/// </summary>
/// <param name="Title">Название заметки.</param>
/// <param name="FilePath">Полный путь к файлу .md.</param>
/// <param name="VaultName">Имя хранилища, которому принадлежит заметка.</param>
/// <param name="LastModified">Дата последнего изменения (для сортировки).</param>
public record RecentNote(string Title, string FilePath, string VaultName, DateTime LastModified);
