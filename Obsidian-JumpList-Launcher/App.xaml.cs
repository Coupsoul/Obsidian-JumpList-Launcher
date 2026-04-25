using System.Windows;

namespace Obsidian_JumpList_Launcher;

/// <summary>
/// Главная точка входа приложения. 
/// Мы наследуемся от Application, чтобы получить доступ к системным возможностям WPF (JumpList).
/// </summary>
public partial class App : Application
{
    // AppUserModelID (AUMID) Obsidian. Windows использует его для группировки окон.
    // Если наш AUMID совпадет с Obsidian, JumpList будет общим.
    public const string ObsidianAumid = "com.squirrel.obsidian.obsidian";

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        // Здесь будет:
        // 1. Поиск пути к Obsidian.exe
        // 2. Сбор последних открытых файлов из всех Vaults
        // 3. Формирование и обновление JumpList
        // 4. Запуск процесса Obsidian
        
        // Завершаем работу лаунчера сразу после выполнения задач
        Shutdown();
    }
}
