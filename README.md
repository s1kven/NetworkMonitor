# Network Monitor

##### Network Monitor - система мониторинга сети, позволяющая хранить историю подключений и израсходованного трафика.

Главная вкладка отображает текущие показатели сети.
Вкладка "Информация о подключении" отображает подробные параметры сети.
На вкладке "История" можно увидеть статистику подключений и трафик израсходованный каждым подключением за сутки.

### Используемые технологии
* Xamarin.Forms
* Xamarin.Essentials
* Android.Net
* Android службы
* SQLite
БД - хранит информацию подключениях и израсходованном трафике в определенный день.
1. Таблица с датами сетевой активности устройства.
2. Таблица с подключениями.
3. Таблица с израсходованным трафиком.