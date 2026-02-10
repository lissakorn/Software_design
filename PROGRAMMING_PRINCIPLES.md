# Programming Principles in the Project

The following software development principles are implemented in this project:

## 1. Separation of Concerns
The project is implemented based on the **MVC (Model-View-Controller)** pattern, which ensures a clear separation of logic, data, and interface. This allows changing the interface without affecting business logic, and vice versa.
- **Models**: Classes `client.cs`, `booking.cs` are responsible only for data.
- **Views**: The `Views/` folder contains only HTML markup for display.
- **Controllers**: The `HomeController.cs` class is responsible only for processing requests.

**Code References:**
- [Data Model (Models/client.cs)](https://github.com/lissakorn/Software_design/blob/main/DF_Perekhrestenko_IPZ-24-1/Models/client.cs#L7C1-L25)
- [Data Model (Models/booking.cs)](https://github.com/lissakorn/Software_design/blob/main/DF_Perekhrestenko_IPZ-24-1/Models/booking.cs#L15-L37)
- [View (Views/Home/Index.cshtml)](https://github.com/lissakorn/Software_design/blob/main/DF_Perekhrestenko_IPZ-24-1/Views/Home/Index.cshtml#L1-L34)
- [Controller (Controllers/HomeController.cs)](https://github.com/lissakorn/Software_design/blob/main/DF_Perekhrestenko_IPZ-24-1/Controllers/HomeController.cs#L12)

## 2. DRY (Don't Repeat Yourself)
To avoid code duplication in the interface (e.g., identical menu and site header on every page), a shared layout (`_Layout.cshtml`) is used. All pages inherit this layout, so the navigation code is written only once.

**Code References:**
- [Main Layout (Views/Shared/_Layout.cshtml)](https://github.com/lissakorn/Software_design/blob/main/DF_Perekhrestenko_IPZ-24-1/Views/Shared/_Layout.cshtml#L1)

## 3. Single Responsibility Principle
Each model class corresponds to only one domain entity. For example, the `client` class stores only client information and does not contain booking or payment logic. Booking logic is moved to a separate `booking` entity or special ViewModels.

**Code References:**
- [Client Class (Models/client.cs)](https://github.com/lissakorn/Software_design/blob/main/DF_Perekhrestenko_IPZ-24-1/Models/client.cs#L7C1-L25)
- [Booking Class (Models/booking.cs)](https://github.com/lissakorn/Software_design/blob/main/DF_Perekhrestenko_IPZ-24-1/Models/booking.cs#L15-L37)
