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

            var vaultDiscovery = new VaultDiscoveryService();
            var noteProvider = new NoteProviderService();
            var jumpListManager = new JumpListManager(obsidianPath, isRussian);

            if (string.IsNullOrEmpty(obsidianPath))
            {

                Shutdown();
                return;
            }

            var vaults = vaultDiscovery.GetVaults();
            var recentNotes = noteProvider.GetRecentNotes(vaults).ToList();

            jumpListManager.UpdateJumpList(recentNotes);

            // Запуск Obsidian

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Критическая ошибка: {ex.Message}");
        }
        finally
        {
            Shutdown();
        }
    }
}
