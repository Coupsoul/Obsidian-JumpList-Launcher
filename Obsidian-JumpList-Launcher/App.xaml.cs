using System.Windows;
using Obsidian_JumpList_Launcher.Services;

namespace Obsidian_JumpList_Launcher;

/// <summary>
/// Главная точка входа приложения. 
/// Оркестрирует работу сервисов: поиск, сбор данных, обновление JumpList и запуск.
/// </summary>
public partial class App : Application
{
    public const string ObsidianAumid = "com.squirrel.obsidian.obsidian";

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        try
        {
            // 1. Создаем экземпляры сервисов (позже можно будет внедрить DI, если проект разрастется)
            var locator = new ObsidianLocator();
            var vaultDiscovery = new VaultDiscoveryService();
            var noteProvider = new NoteProviderService();
            var jumpListManager = new JumpListManager();

            // 2. Выполняем цепочку действий
            var obsidianPath = locator.GetObsidianPath();

            // Если Obsidian не найден, мы не можем продолжать
            if (string.IsNullOrEmpty(obsidianPath))
            {
                // Тут можно добавить логирование или тихое уведомление
                Shutdown();
                return;
            }

            // 3. Собираем данные
            var vaults = vaultDiscovery.GetVaults();
            var recentNotes = noteProvider.GetRecentNotes(vaults).ToList();

            // 4. Обновляем JumpList
            jumpListManager.UpdateJumpList(recentNotes);

            // 5. Запуск Obsidian
            // Если есть аргументы (например, URI из JumpList), передаем их. 
            // Иначе просто запускаем чистый процесс.
            // (Логику запуска вынесем в отдельный метод или сервис позже)
        }
        catch (Exception ex)
        {
            // В продакшене здесь должно быть логирование в файл или EventLog
            Console.WriteLine($"Критическая ошибка: {ex.Message}");
        }
        finally
        {
            // Лаунчер всегда должен закрываться мгновенно
            Shutdown();
        }
    }
}

