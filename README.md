# 🚀 AdvertisingPlatforms 
## 📦 Требования
- **.NET SDK 9.0** или более поздней версии
👉 Скачать отсюда: [https://dotnet.microsoft.com/en-us/download/dotnet/9.0](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
- **Visual Studio 2022** (рекомендуется версия 17.10 или более поздняя) со следующими требованиями:
- ASP.NET и веб-разработка
- Установленный .NET 9 SDK
- **Git** для клонирования репозитория

---

## Самый быстрый способ — клонировать репозиторий прямо через Visual Studio

Запустите Visual Studio 2022.

На стартовом экране выберите Клонировать репозиторий.

Вставьте ссылку:

https://github.com/Mahdi-Abunemer/AdvertisingPlatforms.git


Дождитесь клонирования и откройте решение AdvertisingPlatforms.sln.

Запустите проект (F5 или Ctrl+F5).

✅ Visual Studio автоматически настроит проект, и Swagger будет доступен по адресу:
https://localhost:7182/swagger


## ⚡ Быстрый старт (TL;DR)
```bash
git clone https://github.com/Mahdi-Abunemer/AdvertisingPlatforms.git
cd AdvertisingPlatforms/AdvertisingPlatforms.API
dotnet run
👉 Открыть: https://localhost:7182/swagger


##🖥 Запуск в Visual Studio

Откройте AdvertisingPlatforms.sln в Visual Studio 2022.

Убедитесь, что AdvertisingPlatforms.API выбран в качестве стартового проекта.

Убедитесь, что в файле Properties/launchSettings.json выбрана среда разработки.

Нажмите F5 (отладка) или Ctrl+F5 (запуск без отладки).

✅ Интерфейс Swagger будет доступен по адресу:

https://localhost:7182/swagger


##🔧 Запуск с помощью .NET CLI

Перейдите в папку проекта API:

cd AdvertisingPlatforms/AdvertisingPlatforms.API

Восстановите зависимости:

dotnet restore

Запустите проект:

dotnet run

✅ Вы должны увидеть следующий лог:

Сейчас прослушивается: https://localhost:7182

👉 Откройте https://localhost:7182/swagger
в браузере.


##🧪 Запуск тестов

Запустите их с помощью:

dotnet test

Или запустите в Visual Studio с помощью обозревателя тестов.

##Устранение неполадок

Если браузер не открывается автоматически в Visual Studio:
→ Вручную перейдите по URL-адресу Swagger, указанному в журнале консоли.

Если вы видите предупреждение StaticFileMiddleware о wwwroot:
→ Можно игнорировать (статические файлы не требуются для этого проекта).

Проверьте, установлен ли .NET 9 SDK:

dotnet --list-sdks

Вы должны увидеть что-то вроде:

9.0.xxx 
