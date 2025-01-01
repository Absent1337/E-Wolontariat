# E-Wolontariat

E-Wolontariat to aplikacja webowa stworzona w ASP.NET Core MVC, kt�ra umo�liwia organizacjom zarz�dzanie akcjami wolontariackimi oraz pozwala wolontariuszom na przegl�danie i zapisywanie si� na dost�pne akcje. Aplikacja posiada r�ne poziomy dost�pu dla administrator�w, organizacji i wolontariuszy.

---

## Funkcjonalno�ci

### **Publiczne**
- Przegl�danie dost�pnych akcji (r�wnie� dla anonimowych u�ytkownik�w).

### **Wolontariusze**
- Rejestracja i logowanie.
- Przegl�danie listy dost�pnych akcji.
- Zapisywanie si� na wybrane akcje.
- Przegl�danie historii swoich zapis�w wraz ze statusem zg�oszenia (zaakceptowane/odrzucone).

### **Organizacje**
- Dodawanie nowych akcji.
- Edytowanie i usuwanie swoich akcji.
- Przegl�danie zg�osze� wolontariuszy na swoje akcje.
- Akceptowanie lub odrzucanie zg�osze� wolontariuszy.

### **Administratorzy**
- Zarz�dzanie wszystkimi akcjami.
- Tworzenie u�ytkownik�w i przypisywanie im r�l.

---

## Technologie u�yte w projekcie

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

Zamie� `YourStrongPassword` na has�o do swojej instancji SQL Server.

### **3. Migracje bazy danych**

Wykonaj migracje, aby utworzy� schemat bazy danych:

```bash
dotnet ef database update
```

### **4. Uruchomienie aplikacji lokalnie**

Uruchom aplikacj�:

```bash
dotnet run
```

Aplikacja b�dzie dost�pna pod adresem:

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

Aplikacja b�dzie dost�pna pod adresem:

```
http://localhost:8080
```

---

## Struktura projektu

- **Controllers**: Logika kontroler�w MVC.
- **Models**: Modele danych (np. `Action`, `Application`, `User`).
- **Views**: Widoki Razor (np. `Index.cshtml`, `Details.cshtml`).
- **Services**: Logika biznesowa i integracje.
- **Migrations**: Migracje bazy danych.

---

## Role u�ytkownik�w

### **Admin**
- Ma dost�p do wszystkich funkcjonalno�ci aplikacji.
- Mo�e tworzy� u�ytkownik�w i przypisywa� im role.

### **Organization**
- Mo�e zarz�dza� swoimi akcjami.
- Ma dost�p do zg�osze� wolontariuszy.

### **Volunteer**
- Mo�e przegl�da� i zapisywa� si� na akcje.
- Widzi histori� swoich zg�osze�.

---

## Funkcjonalno�ci AJAX

- **Wyszukiwanie akcji**: Dynamiczne filtrowanie akcji po tytule, dacie i lokalizacji z obs�ug� AJAX.
- **Resetowanie filtr�w**: Mo�liwo�� resetowania kryteri�w wyszukiwania bez prze�adowania strony.

---

## Przyk�adowe konto

- **Admin**:
  - Email: `admin@admin.com`
  - Has�o: `Admin123!`


---

## TODO

- Dodanie paginacji wynik�w wyszukiwania.
- Obs�uga powiadomie� e-mail dla akceptacji/odrzucenia zg�osze�.
- Zwi�kszenie pokrycia testami jednostkowymi.

---

## Wk�ad w projekt

Zapraszamy do zg�aszania b��d�w i propozycji nowych funkcjonalno�ci za pomoc� [Issues](https://github.com/user/e-wolontariat/issues) w repozytorium.

---

## Licencja

Projekt jest dost�pny na licencji MIT. Szczeg�y w pliku `LICENSE`.

