Структура проекта

Assets/
├── Data/ - данные уровней и настройки приложения
├── Entities/ - папка с контентом и префабами
│   ├── Context/
│   ├── Level/
│   ├── Screens/
│   └── Services/
├── Scenes/ - сцены
├── Scripts/
│   ├── Context/ - контексты
│   │   ├── EntryPoint.cs - точка входа в приложение
│   │   ├── GameSessionContextInstaller.cs
│   │   ├── ProjectContextInstaller.cs
│   │   └── SceneContextInstaller.cs
│   ├── GameCore/ - игровая механика
│   │   ├── GameSession/ - игровая сессия 
│   │   ├── LevelControl/ - контроль уровнем, существует в контексте одного уровня
│   │   ├── LevelGeneration/ - генерация уровней 
│   │   ├── Models/ 
│   ├── Services/ - бизнесовые сервисы 
│   └── UI/
├── submodule_appCore/ - субмобуль для быстрого старта пэт-проектов. Пока в нем только сервис по управлению экранами и звуком
