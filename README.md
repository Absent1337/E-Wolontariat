# E-Wolontariat

E-Wolontariat to aplikacja webowa stworzona w ASP.NET Core MVC, która umo¿liwia organizacjom zarz¹dzanie akcjami wolontariackimi oraz pozwala wolontariuszom na przegl¹danie i zapisywanie siê na dostêpne akcje. Aplikacja posiada ró¿ne poziomy dostêpu dla administratorów, organizacji i wolontariuszy.

---

## Funkcjonalnoœci

### **Publiczne**
- Przegl¹danie dostêpnych akcji (równie¿ dla anonimowych u¿ytkowników).

### **Wolontariusze**
- Rejestracja i logowanie.
- Przegl¹danie listy dostêpnych akcji.
- Zapisywanie siê na wybrane akcje.
- Przegl¹danie historii swoich zapisów wraz ze statusem zg³oszenia (zaakceptowane/odrzucone).

### **Organizacje**
- Dodawanie nowych akcji.
- Edytowanie i usuwanie swoich akcji.
- Przegl¹danie zg³oszeñ wolontariuszy na swoje akcje.
- Akceptowanie lub odrzucanie zg³oszeñ wolontariuszy.

### **Administratorzy**
- Zarz¹dzanie wszystkimi akcjami.
- Tworzenie u¿ytkowników i przypisywanie im ról.

---

## Technologie u¿yte w projekcie

- **Backend**: ASP.NET Core 7.0 MVC
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

Zamieñ `YourStrongPassword` na has³o do swojej instancji SQL Server.

### **3. Migracje bazy danych**

Wykonaj migracje, aby utworzyæ schemat bazy danych:

```bash
dotnet ef database update
```

### **4. Uruchomienie aplikacji lokalnie**

Uruchom aplikacjê:

```bash
dotnet run
```

Aplikacja bêdzie dostêpna pod adresem:

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

Aplikacja bêdzie dostêpna pod adresem:

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

## Role u¿ytkowników

### **Admin**
- Ma dostêp do wszystkich funkcjonalnoœci aplikacji.
- Mo¿e tworzyæ u¿ytkowników i przypisywaæ im role.

### **Organization**
- Mo¿e zarz¹dzaæ swoimi akcjami.
- Ma dostêp do zg³oszeñ wolontariuszy.

### **Volunteer**
- Mo¿e przegl¹daæ i zapisywaæ siê na akcje.
- Widzi historiê swoich zg³oszeñ.

---

## Funkcjonalnoœci AJAX

- **Wyszukiwanie akcji**: Dynamiczne filtrowanie akcji po tytule, dacie i lokalizacji z obs³ug¹ AJAX.
- **Resetowanie filtrów**: Mo¿liwoœæ resetowania kryteriów wyszukiwania bez prze³adowania strony.

---

## Przyk³adowe konto

- **Admin**:
  - Email: `admin@admin.com`
  - Has³o: `Admin123!`


---

## TODO

- Dodanie paginacji wyników wyszukiwania.
- Obs³uga powiadomieñ e-mail dla akceptacji/odrzucenia zg³oszeñ.
- Zwiêkszenie pokrycia testami jednostkowymi.

---

## Wk³ad w projekt

Zapraszamy do zg³aszania b³êdów i propozycji nowych funkcjonalnoœci za pomoc¹ [Issues](https://github.com/user/e-wolontariat/issues) w repozytorium.

---

## Licencja

Projekt jest dostêpny na licencji MIT. Szczegó³y w pliku `LICENSE`.

