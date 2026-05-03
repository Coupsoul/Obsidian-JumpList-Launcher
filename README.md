<div align="center">
  <img src="Obsidian-JumpList-Launcher/launcher-icon.ico" alt="Obsidian JumpList Launcher Logo" width="128">
  
  <h1>Obsidian JumpList Launcher</h1>
  
  <p>A smart taskbar launcher for Obsidian / Умный лаунчер для панели задач Windows</p>
</div>

<div align="center">

[EN](#english) | [RU](#русский)

</div>

---

## English

A lightweight Windows utility that replaces the standard Obsidian taskbar shortcut, adding a dynamic JumpList with your recent notes from **all** local vaults.

## Key Features

- **Unified Recent Files List**: Aggregates recently opened notes (.md) from all your vaults into a single list.
- **Quick Navigation**: Opens notes directly via the `obsidian://` URI scheme, ensuring instant switching within the app.
- **"Manage Vaults" Task**: Fast access to the Obsidian vault switcher.
- **Seamless Interface**: The utility has no window and acts as a "proxy" — it launches, updates the list, forwards arguments to Obsidian, and closes instantly.
- **Auto-Localization**: Automatically detects language (RU/EN) based on Windows system settings.

## System Requirements

- **OS**: Windows 10 (19041+) or Windows 11 (22000–26100).
- **Software**: Installed Obsidian.
- **Environment**: .NET 9 Desktop Runtime (required for the **Lite** version).

## Installation and Usage

1. Download the latest version from the [Releases](https://github.com/Coupsoul/Obsidian-JumpList-Launcher/releases) section.
   - **Lite version**: Requires .NET 9 Runtime installed.
   - **Self-contained**: Includes all necessary libraries (no .NET installation required).
2. Pin the launcher to your taskbar.
3. Launch it once to let the utility update the JumpList.
4. Now, by right-clicking on a shortcut, you can access your 12 most recent notes, which you can pin to keep them at the top, or instantly switch between storages using the "Manage Storage" task.

## How It Works

The application uses the AppUserModelID `com.squirrel.obsidian.obsidian`. This allows Windows to group launcher tasks together with the main Obsidian process.

Upon launch, the launcher:
 1. Finds the path to the installed Obsidian via the Windows Registry.
 2. Scans `%AppData%\obsidian\obsidian.json` to find all vault paths.
 3. Reads `.obsidian/workspace.json` in each vault to retrieve the last opened files.
 4. Sorts them by last modification date and generates the system JumpList.
 5. Starts Obsidian with the passed arguments (if any).

## Tech Stack and Architecture

- **Platform & UI**: .NET 9, WPF (native `System.Windows.Shell` for JumpList generation).
- **Language**: C# 13 (utilizing records, collection expressions, and file-scoped namespaces).
- **Architecture**: Zero-background (no memory footprint after launch), direct parsing of local JSON configs to minimize I/O.
- **Security**: Safe argument passing via `ArgumentList`, strict vault path validation (Path Traversal protection).

---

## Русский

Легковесная утилита для Windows, которая заменяет стандартный ярлык Obsidian на панели задач, добавляя динамический список переходов (JumpList) с вашими последними заметками из **всех** локальных хранилищ.

## Ключевые особенности

- **Объединённый список недавних файлов**: Собирает последние открытые заметки (.md) из всех ваших хранилищ (Vaults) в один список.
- **Быстрая навигация**: Открывает заметки напрямую через URI-схему `obsidian://`, что гарантирует мгновенное переключение в приложении.
- **Задача "Управление хранилищами"**: Быстрый доступ к окну взаимодействия с хранилищами.
- **Бесшовный интерфейс**: Утилита не имеет собственного окна и работает как "прокси" – запускается, обновляет список, пробрасывает аргументы в Obsidian и мгновенно закрывается.
- **Авто-локализация**: Автоматическое определение языка (RU/EN) на основе системных настроек Windows.

## Системные требования

- **ОС**: Windows 10 (версия 19041+) или Windows 11 (22000–26100).
- **ПО**: Установленный Obsidian.
- **Среда**: .NET 9 Desktop Runtime (требуется только для **Lite** версии).

## Установка и использование

1. Скачайте последнюю версию из раздела [Releases](https://github.com/Coupsoul/Obsidian-JumpList-Launcher/releases).
   - **Lite version**: требует установленного .NET 9 Runtime.
   - **Self-contained**: включает в себя все необходимые библиотеки (не требует установки .NET).
2. Закрепите лаунчер на панели задач.
3. Запустите, чтобы лаунчер обновил список переходов.
4. Теперь по правому клику на ярлык доступны 12 ваших последних заметок, которые можно закреплять, чтобы они всегда оставались в топе, или мгновенно переключаться между хранилищами через задачу «Управление хранилищами».

## Как это работает?

Приложение использует идентификатор модели пользователя (AppUserModelID) со значением `com.squirrel.obsidian.obsidian`. Это позволяет Windows группировать задачи лаунчера вместе с основным процессом Obsidian.

При запуске лаунчер:
 1. Ищет путь к установленному Obsidian через реестр Windows.
 2. Сканирует `%AppData%\obsidian\obsidian.json` для поиска всех путей к хранилищам.
 3. Читает `.obsidian/workspace.json` в каждом хранилище для получения списка последних файлов.
 4. Сортирует их по дате последнего изменения и формирует системный JumpList.
 5. Запускает Obsidian с переданными аргументами (если они есть).

## Технологический стек и архитектура

- **Платформа и UI:** .NET 9, WPF (нативная работа с `System.Windows.Shell` для генерации JumpList).
- **Язык:** C# 13 (использование `record`, collection expressions, file-scoped namespaces).
- **Архитектурный подход:** Zero-background (лаунчер не висит в памяти), прямое чтение локальных JSON-конфигов Obsidian для минимизации I/O операций.
- **Безопасность:** Защита от инъекций аргументов командной строки (использование `ProcessStartInfo.ArgumentList`), строгая валидация локальных путей (защита от Path Traversal).

---

### Сборка из исходников / Building from source

```bash
# Clone the repository
git clone [https://github.com/Coupsoul/Obsidian-JumpList-Launcher.git](https://github.com/Coupsoul/Obsidian-JumpList-Launcher.git)

# Publish as a single-file executable (Lite)
dotnet publish -c Release -r win-x64 --self-contained false /p:PublishSingleFile=true
```
