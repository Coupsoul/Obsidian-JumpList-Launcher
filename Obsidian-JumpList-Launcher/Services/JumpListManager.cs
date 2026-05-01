using System.IO;
using System.Windows;
using System.Windows.Shell;
using Obsidian_JumpList_Launcher.Models;

namespace Obsidian_JumpList_Launcher.Services;

/// <summary>
/// Отвечает за взаимодействие с Windows Shell для обновления списка переходов (JumpList).
/// </summary>
public class JumpListManager
{
    private readonly string _obsidianPath;
    private readonly bool _isRussian;

    public JumpListManager(string obsidianPath, bool isRussian)
    {
        _obsidianPath = obsidianPath;
        _isRussian = isRussian;
    }

    /// <summary>
    /// Принимает список заметок и формирует из них задачи (JumpTasks) для панели задач.
    /// </summary>
    public void UpdateJumpList(IEnumerable<RecentNote> notes)
    {
        var jumpList = new JumpList();

        var manageTask = new JumpTask();
        bool isRussian = System.Globalization.CultureInfo.CurrentUICulture.TwoLetterISOLanguageName == "ru";
        manageTask.Title = _isRussian ? "Управление хранилищами" : "Manage vaults";
        manageTask.ApplicationPath = _obsidianPath;
        manageTask.Arguments = "obsidian://choose-vault";
        manageTask.IconResourcePath = _obsidianPath;

        jumpList.JumpItems.Add(manageTask);

        foreach (var note in notes)
        {
            var recentTask = new JumpTask();
            recentTask.Title = Path.GetFileNameWithoutExtension(note.Title);
            recentTask.ApplicationPath = _obsidianPath;
            recentTask.IconResourcePath = _obsidianPath;
            recentTask.Arguments = $"obsidian://open?path={Uri.EscapeDataString(note.FilePath)}";
            recentTask.CustomCategory = _isRussian ? "Недавние" : "Recent";

            jumpList.JumpItems.Add(recentTask);
        }

        JumpList.SetJumpList(Application.Current, jumpList);
        jumpList.Apply();
    }
}
