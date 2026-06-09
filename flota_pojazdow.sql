-- phpMyAdmin SQL Dump
-- version 5.1.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Czas generowania: 11 Gru 2023, 19:49
-- Wersja serwera: 10.4.22-MariaDB
-- Wersja PHP: 8.1.2

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Baza danych: `flota_pojazdow`
--

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `dane_logowania`
--

CREATE TABLE `dane_logowania` (
  `id` int(11) NOT NULL,
  `login` text NOT NULL,
  `haslo` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Zrzut danych tabeli `dane_logowania`
--

INSERT INTO `dane_logowania` (`id`, `login`, `haslo`) VALUES
(1, 'admin', '123'),
(2, 'admin', 'admin'),
(3, 'root', ''),
(4, 'wlasciciel', '123');

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `opisy`
--

CREATE TABLE `opisy` (
  `id` int(11) NOT NULL,
  `id_samochodu` int(11) NOT NULL,
  `opis` text NOT NULL,
  `data` datetime NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Zrzut danych tabeli `opisy`
--

INSERT INTO `opisy` (`id`, `id_samochodu`, `opis`, `data`) VALUES
(1, 1, 'abc', '0001-01-01 00:00:00'),
(2, 1, 'abc1', '0001-01-01 00:00:00'),
(3, 2, 'abc', '2023-11-20 00:00:00'),
(4, 1, 'abc', '2023-11-20 00:00:00'),
(5, 1, 'opis dla samochodu', '2023-11-20 17:32:59'),
(6, 1, 'opis dla toyoty paseo', '2023-11-20 17:37:16'),
(7, 42, '123', '2023-11-20 17:37:26'),
(8, 42, 'abvc', '2023-11-20 17:40:36'),
(9, 42, '321321312', '2023-11-20 17:42:01'),
(10, 17, 'OPEL ASTRA', '2023-11-20 17:43:18'),
(11, 1, 'xyz', '2023-12-10 00:38:09');

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `plyny`
--

CREATE TABLE `plyny` (
  `id` int(11) NOT NULL,
  `id_samochodu` int(11) NOT NULL,
  `olej_silnikowy_zmiana` int(11) NOT NULL,
  `olej_silnikowy_dozmiany` int(11) NOT NULL,
  `plyn_hamulcowy_zmiana` int(11) NOT NULL,
  `plyn_hamulcowy_dozmiany` int(11) NOT NULL,
  `plyn_chlodniczy_zmiana` int(11) NOT NULL,
  `plyn_chlodniczy_dozmiany` int(11) NOT NULL,
  `plyn_wspomagania_zmiana` int(11) NOT NULL,
  `plyn_wspomagania_dozmiany` int(11) NOT NULL,
  `plyn_skrzyni_zmiana` int(11) NOT NULL,
  `plyn_skrzyni_dozmiany` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Zrzut danych tabeli `plyny`
--

INSERT INTO `plyny` (`id`, `id_samochodu`, `olej_silnikowy_zmiana`, `olej_silnikowy_dozmiany`, `plyn_hamulcowy_zmiana`, `plyn_hamulcowy_dozmiany`, `plyn_chlodniczy_zmiana`, `plyn_chlodniczy_dozmiany`, `plyn_wspomagania_zmiana`, `plyn_wspomagania_dozmiany`, `plyn_skrzyni_zmiana`, `plyn_skrzyni_dozmiany`) VALUES
(1, 1, 21, 2137, 2100, 31, 2000, 4000, 2123, 5123, 1500, 77777),
(2, 42, 32, 0, 0, 0, 0, 0, 43, 0, 0, 0);

-- --------------------------------------------------------

--
-- Struktura tabeli dla tabeli `samochody`
--

CREATE TABLE `samochody` (
  `id` int(11) NOT NULL,
  `marka` text NOT NULL,
  `model` text NOT NULL,
  `numer_rejestracyjny` text NOT NULL,
  `vin` text NOT NULL,
  `data_przegladu` date NOT NULL,
  `data_nastepnego_przegladu` date NOT NULL,
  `rok_produkcji` int(11) NOT NULL,
  `przebieg` int(11) DEFAULT NULL,
  `Zaznaczony` tinyint(1) DEFAULT 0
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Zrzut danych tabeli `samochody`
--

INSERT INTO `samochody` (`id`, `marka`, `model`, `numer_rejestracyjny`, `vin`, `data_przegladu`, `data_nastepnego_przegladu`, `rok_produkcji`, `przebieg`, `Zaznaczony`) VALUES
(1, 'Toyota', 'Paseo', 'LZA 3667A', '4A32B2FF5AE017680', '2022-11-17', '2023-11-17', 1996, 320007, 0),
(2, 'Mercedes', 'w203', 'LZA 5312A', '4T1BF1FK1EU794637', '2023-08-18', '2024-08-18', 2002, 210007, 0),
(17, 'Opel', 'Astra', 'LTM astra', '2G1WB5EN1A1215679', '2023-10-12', '2023-01-12', 2004, 140007, 0),
(42, 'Ford', 'Mondeo', 'DPL 30906', '1GC1KYE8XBF153996', '2023-02-11', '2024-02-11', 2009, 3000007, 0),
(43, 'VW', 'Polo', 'DPL 12314', '3FAHP0HA0BR183050', '2023-07-08', '2023-12-08', 1997, 2700213, 0);

--
-- Indeksy dla zrzutów tabel
--

--
-- Indeksy dla tabeli `dane_logowania`
--
ALTER TABLE `dane_logowania`
  ADD PRIMARY KEY (`id`);

--
-- Indeksy dla tabeli `opisy`
--
ALTER TABLE `opisy`
  ADD PRIMARY KEY (`id`),
  ADD KEY `samochod_opis` (`id_samochodu`);

--
-- Indeksy dla tabeli `plyny`
--
ALTER TABLE `plyny`
  ADD PRIMARY KEY (`id`),
  ADD KEY `id_samochodu` (`id_samochodu`);

--
-- Indeksy dla tabeli `samochody`
--
ALTER TABLE `samochody`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT dla zrzuconych tabel
--

--
-- AUTO_INCREMENT dla tabeli `dane_logowania`
--
ALTER TABLE `dane_logowania`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT dla tabeli `opisy`
--
ALTER TABLE `opisy`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=12;

--
-- AUTO_INCREMENT dla tabeli `plyny`
--
ALTER TABLE `plyny`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT dla tabeli `samochody`
--
ALTER TABLE `samochody`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=48;

--
-- Ograniczenia dla zrzutów tabel
--

--
-- Ograniczenia dla tabeli `opisy`
--
ALTER TABLE `opisy`
  ADD CONSTRAINT `samochod_opis` FOREIGN KEY (`id_samochodu`) REFERENCES `samochody` (`id`);

--
-- Ograniczenia dla tabeli `plyny`
--
ALTER TABLE `plyny`
  ADD CONSTRAINT `plyny_ibfk_1` FOREIGN KEY (`id_samochodu`) REFERENCES `samochody` (`id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
