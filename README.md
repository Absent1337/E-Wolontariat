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

### **4. Uruchomienie aplikacji lokalnie**

Uruchom aplikację:

```bash
dotnet run
```

Aplikacja będzie dostępna pod adresem:

```
http://localhost:5000
http://localhost:5001 (HTTPS)
```

### **5. Uruchomienie aplikacji w Dockerze**

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

- **Controllers**: Logika kontrolerów MVC.
- **Models**: Modele danych (np. `Action`, `Application`, `User`).
- **Views**: Widoki Razor (np. `Index.cshtml`, `Details.cshtml`).
- **Services**: Logika biznesowa i integracje.
- **Migrations**: Migracje bazy danych.

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

---

## Wkład w projekt

Zapraszamy do zgłaszania błędów i propozycji nowych funkcjonalności za pomocą [Issues](https://github.com/user/e-wolontariat/issues) w repozytorium.

---

## Licencja

Projekt jest dostępny na licencji GPL-3. Szczegóły w pliku `LICENSE`.

