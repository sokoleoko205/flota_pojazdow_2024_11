Fleet Management App (Flota Pojazdów)

Aplikacja desktopowa do zarządzania flotą pojazdów, stworzona w ramach projektu zespołowego i praktyk.

Technologie
- Frontend / UI: .NET MAUI (C#), własnoręcznie zaprojektowana grafika.
- Baza danych: MySQL (XAMPP / Apache).

Funkcjonalności
Aplikacja wspiera podstawowe operacje na bazie danych oraz dodatkowe funkcje:
1. Logowanie: Autoryzacja użytkowników, pobierająca informacje z tabeli 'dane_logowania'. Przykłady danych logowania:
   - admin / 123
   - admin / admin
   - root (bez hasła)
   - wlasciciel / 123
2. Strona główna: Wyświetla najważniejsze informacje z bazy i zawiera przyciski operacyjne:
   - I. Aktualizuj: Zmiana wpisów dotyczących daty przeglądu, daty następnego przeglądu lub przebiegu po zaznaczeniu odpowiednich CheckBoxów.
   - II. Usuń: Usunięcie wybranego wiersza na podstawie zaznaczonego CheckBoxa obok ID.
   - III. Raport: Sprawdzanie liczby samochodów w bazie oraz terminów przeglądów.
   - IV. Opis: Przejście do podstrony szczegółowych informacji o wybranym samochodzie.
   - V. Dodaj: Wprowadzenie nowego samochodu do bazy danych.
   - VI. Wyczyść: Wyczyszczenie pól przeznaczonych do dodawania nowych aut.
3. Opisy: Automatycznie generowane podstrony dla pojazdów umożliwiające dodawanie notatek opatrzone automatycznym stemplem czasowym (data i godzina).

Znane problemy i ograniczenia
- Pobieranie raportów: Z powodu ograniczeń frameworka, mechanizm zapisu pozwala jedynie na nadpisywanie istniejących plików, nie wyświetla nowego okna dialogowego. Aby zapisać nowy raport, należy ręcznie utworzyć pusty plik tekstowy .txt i kliknąć przycisk otwarcia, aby go wskazać.

Uwaga autora
Aplikacja jest efektem nauki nowego języka programowania i frameworka od podstaw. Kod jest wynikiem prób, błędów oraz nabytej wiedzy.
