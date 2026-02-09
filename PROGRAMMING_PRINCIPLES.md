# Принципи програмування в проєкті

У цьому проєкті реалізовано наступні принципи розробки програмного забезпечення:

## 1. Separation of Concerns (Розділення відповідальності)
Проєкт реалізовано на основі патерну **MVC (Model-View-Controller)**, що забезпечує чітке розмежування логіки, даних та інтерфейсу. Це дозволяє змінювати інтерфейс, не зачіпаючи бізнес-логіку, і навпаки.
- **Models**: Класи `client.cs`, `booking.cs` відповідають тільки за дані.
- **Views**: Папка `Views/` містить тільки HTML-розмітку для відображення.
- **Controllers**: Клас `HomeController.cs` відповідає тільки за обробку запитів.

**Посилання на код:**
- [Модель даних (Models/client.cs)](https://github.com/lissakorn/Software_design/blob/main/DF_Perekhrestenko_IPZ-24-1/Models/client.cs#L7C1-L25)
- [Модель даних (Models/booking.cs)](https://github.com/lissakorn/Software_design/blob/main/DF_Perekhrestenko_IPZ-24-1/Models/booking.cs#L15-L37)
- [Представлення (Views/Home/Index.cshtml)](https://github.com/lissakorn/Software_design/blob/main/DF_Perekhrestenko_IPZ-24-1/Views/Home/Index.cshtml#L1-L34)
- [Контролер (Controllers/HomeController.cs)](https://github.com/lissakorn/Software_design/blob/main/DF_Perekhrestenko_IPZ-24-1/Controllers/HomeController.cs#L12)
  
## 2. DRY (Don't Repeat Yourself — Не повторюй себе)
Щоб уникнути дублювання коду в інтерфейсі (наприклад, однакове меню та "шапка" сайту на кожній сторінці), використано спільний макет (Layout). Усі сторінки наслідують цей макет, тому код навігації написаний лише один раз.

**Посилання на код:**
- [Головний макет (Views/Shared/_Layout.cshtml)](https://github.com/lissakorn/Software_design/blob/main/DF_Perekhrestenko_IPZ-24-1/Views/Shared/_Layout.cshtml#L1)

## 3. Single Responsibility Principle (Принцип єдиної відповідальності)
Кожен клас моделі відповідає лише за одну сутність предметної області. Наприклад, клас `client` зберігає лише інформацію про клієнта і не містить логіки бронювання чи оплати. Логіка бронювання винесена в окрему сутність `booking` або спеціальні ViewModel.

**Посилання на код:**
- [Клас клієнта (Models/client.cs)](https://github.com/lissakorn/Software_design/blob/main/DF_Perekhrestenko_IPZ-24-1/Models/client.cs#L7C1-L25)
- [Клас бронювання (Models/booking.cs)](https://github.com/lissakorn/Software_design/blob/main/DF_Perekhrestenko_IPZ-24-1/Models/booking.cs#L15-L37)
