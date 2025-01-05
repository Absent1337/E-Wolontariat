# E-Wolontariat

E-Wolontariat to aplikacja webowa stworzona w ASP.NET Core MVC, która umożliwia organizacjom zarządzanie akcjami wolontariackimi oraz pozwala wolontariuszom na przeglądanie i zapisywanie się na dostępne akcje. Aplikacja posiada różne poziomy dostępu dla administratorów, organizacji i wolontariuszy.

---

## Funkcjonalności

### **Publiczne**
- Przeglądanie dostępnych akcji (również dla anonimowych użytkowników).

### **Wolontariusze**
- Rejestracja i logowanie.
- Przeglądanie listy dostępnych akcji.
- Zapisywanie się na wybrane akcje.
- Przeglądanie historii swoich zapisów wraz ze statusem zgłoszenia (zaakceptowane/odrzucone).

### **Organizacje**
- Dodawanie nowych akcji.
- Edytowanie i usuwanie swoich akcji.
- Przeglądanie zgłoszeń wolontariuszy na swoje akcje.
- Akceptowanie lub odrzucanie zgłoszeń wolontariuszy.

### **Administratorzy**
- Zarządzanie wszystkimi akcjami.
- Tworzenie użytkowników i przypisywanie im ról.

---

## Technologie użyte w projekcie

- **Backend**: ASP.NET Core 8.0 MVC
- **Frontend**: Razor Pages, Bootstrap 5
- **Baza danych**: Microsoft SQL Server
- **Autoryzacja i uwierzytelnianie**: ASP.NET Identity
- **Konteneryzacja**: Docker

---

## Instrukcja uruchomienia

### **1. Klonowanie repozytorium**

```bash
# Skopiuj repozytorium na lokalny komputer
git clone https://github.com/user/e-wolontariat.git
cd e-wolontariat
```

### **2. Konfiguracja bazy danych**

#### Edytuj plik `appsettings.json`:

```json
"ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=E-Wolontariat;User Id=sa;Password=YourStrongPassword;"
}
```

Zamień `YourStrongPassword` na hasło do swojej instancji SQL Server.

### **3. Migracje bazy danych**

Wykonaj migracje, aby utworzyć schemat bazy danych:

```bash
dotnet ef database update
```

### **4. Tworzenie pierwszego konta organizacji**

Domyślnie każde nowe konto jest tworzone jako wolontariusz. Pierwsza akcje można dodać na kilka możliwości:
1. Użyć wbudowanego konta organizacji i utworzyć nim akcje
2. Zalogować się na konto administratora, zmienić rolę wybranego wolontariusza na organizację i umożliwić mu dodanie akcji.
3. Alternatywnie, z poziomu panelu administratora można utworzyć konto z rolą organizacji i rozpocząć dodawanie akcji.

### **5. Uruchomienie aplikacji lokalnie**

Uruchom aplikację:

```bash
dotnet run
```

Aplikacja będzie dostępna pod adresem:

```
http://localhost:5180
```

### **6. Uruchomienie aplikacji w Dockerze**

#### Budowanie obrazu Dockerowego

```bash
docker build -t e-wolontariat .
```

#### Uruchomienie kontenera

```bash
docker run -d -p 8080:80 e-wolontariat
```

Aplikacja będzie dostępna pod adresem:

```
http://localhost:8080
```

---

## Struktura projektu

### **Controllers**
- `ActionsController.cs` - Obsługuje zarządzanie akcjami (CRUD), zapisy wolontariuszy oraz filtrowanie akcji.
- `AdminPanelController.cs` - Zarządza funkcjami dostępnymi dla administratora, w tym tworzeniem użytkowników i przypisywaniem ról.
- `HomeController.cs` - Obsługuje strony główne i statyczne, takie jak `Index` i `Privacy`.

### **Models**
- `Action.cs` - Reprezentuje akcje organizowane przez organizacje (tytuł, opis, data, lokalizacja, liczba uczestników).
- `ActionDto.cs` - Służy jako obiekt transferowy do walidacji danych dla akcji.
- `Application.cs` - Model zgłoszeń wolontariuszy na akcje (status, relacje do użytkownika i akcji).
- `CreateUserViewModel.cs` - Model używany do tworzenia nowych użytkowników przez administratora.
- `ErrorViewModel.cs` - Obsługuje komunikaty błędów.
- `ManageRolesViewModel.cs` - Model używany do zarządzania rolami użytkowników.
- `UserWithRolesViewModel.cs` - Reprezentuje użytkownika z przypisanymi rolami.
- `VolunteerAction.cs` - Model akcji przypisanych do wolontariuszy.

### **Views**
- **Actions**:
  - `_SearchResults.cshtml` - Widok częściowy do wyświetlania wyników wyszukiwania.
  - `Applications.cshtml` - Wyświetla zgłoszenia na akcje.
  - `Create.cshtml` - Formularz tworzenia nowej akcji.
  - `Details.cshtml` - Szczegóły akcji.
  - `Edit.cshtml` - Formularz edycji akcji.
  - `Index.cshtml` - Lista wszystkich akcji z funkcją wyszukiwania.
  - `MyActions.cshtml` - Lista akcji, na które zapisał się wolontariusz.
- **AdminPanel**:
  - `CreateUser.cshtml` - Formularz do tworzenia nowych użytkowników.
  - `Index.cshtml` - Panel administratora z listą użytkowników.
  - `ManageRoles.cshtml` - Zarządzanie rolami użytkowników.
- **Home**:
  - `Index.cshtml` - Strona główna.
  - `Privacy.cshtml` - Polityka prywatności.
- **Shared**:
  - `_ViewImports.cshtml` - Globalne dyrektywy Razor.
  - `_ViewStart.cshtml` - Konfiguracja widoków.

### **Services**
- `ApplicationDbContext.cs` - Klasa kontekstu bazy danych, zarządzająca modelami i ich relacjami.

### **Migrations**
- Folder zawierający migracje bazy danych generowane przez Entity Framework.

---

## Role użytkowników

### **Admin**
- Ma dostęp do wszystkich funkcjonalności aplikacji.
- Może tworzyć użytkowników i przypisywać im role.

### **Organization**
- Może zarządzać swoimi akcjami.
- Ma dostęp do zgłoszeń wolontariuszy.

### **Volunteer**
- Może przeglądać i zapisywać się na akcje.
- Widzi historię swoich zgłoszeń.

---

## Funkcjonalności AJAX

- **Wyszukiwanie akcji**: Dynamiczne filtrowanie akcji po tytule, dacie i lokalizacji z obsługą AJAX.
- **Resetowanie filtrów**: Możliwość resetowania kryteriów wyszukiwania bez przeładowania strony.

---

## Przykładowe konto

- **Admin**:
  - Email: `admin@admin.com`
  - Hasło: `Admin123!`
- **Wolontariusz**:
  - Email: `wolontariusz@email.com`
  - Hasło: `Wolontariat123!`
- **Organizacja**:
  - Email: `organizacja@email.com`
  - Hasło: `Organizacja123!`

---

## Wkład w projekt

Zapraszamy do zgłaszania błędów i propozycji nowych funkcjonalności za pomocą [Issues](https://github.com/Absent1337/E-Wolontariat/issues) w repozytorium.

---

## Licencja

Projekt jest dostępny na licencji GPL-3. Szczegóły w pliku `LICENSE`.
