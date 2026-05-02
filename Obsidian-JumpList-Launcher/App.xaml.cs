using System.Diagnostics;
using System.Windows;
using Obsidian_JumpList_Launcher.Services;

namespace Obsidian_JumpList_Launcher;

/// <summary>
/// Главная точка входа приложения. 
/// Оркестрирует работу сервисов: поиск, сбор данных, обновление JumpList и запуск.
/// </summary>
public partial class App : Application
{
    public const string OBSIDIAN_AUMID = "com.squirrel.obsidian.obsidian";
    public const int MAX_RECENT_NOTES = 12;

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        bool isRussian = System.Globalization.CultureInfo.CurrentUICulture.TwoLetterISOLanguageName == "ru";

        try
        {
            var locator = new ObsidianLocator();
            var obsidianPath = locator.GetObsidianPath();

            if (string.IsNullOrEmpty(obsidianPath))
            {
                MessageBox.Show(
                    isRussian ?
                    "Не удалось найти установленный Obsidian." : "Could not find installed Obsidian.",
                    "Obsidian Launcher",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                Shutdown();
                return;
            }

            var vaultDiscovery = new VaultDiscoveryService();
            var noteProvider = new NoteProviderService();
            var jumpListManager = new JumpListManager(obsidianPath, isRussian);

            var vaults = vaultDiscovery.GetVaults();
            var recentNotes = noteProvider.GetRecentNotes(vaults).ToList();

            jumpListManager.UpdateJumpList(recentNotes);

            var startInfo = new ProcessStartInfo
            {
                FileName = obsidianPath,
                UseShellExecute = true
            };

            foreach (var arg in e.Args)
            {
                startInfo.ArgumentList.Add(arg);
            }

            Process.Start(startInfo);
        }
        catch (Exception ex)
        {
            MessageBox.Show(
                (isRussian ? "Критическая ошибка: " : "Critical error: ") + ex.Message,
                "Obsidian Launcher",
                MessageBoxButton.OK,
                MessageBoxImage.Stop
                );
        }
        finally
        {
            Shutdown();
        }
    }
}
