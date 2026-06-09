using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;
using Microsoft.EntityFrameworkCore;

namespace flotapojazdow1
{

    public class AppDbContext : DbContext
    {
        public DbSet<Samochod> samochody { get; set; }
        public DbSet<Opisy> opisy { get; set; }
        public DbSet<Plyny> plyny { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("Server=localhost;Database=flota_pojazdow;User=root;Password=;");
        }
    }
    public class Plyny
    {
        public int id { get; set; }
        public int id_samochodu { get; set; }
        public int olej_silnikowy_zmiana { get; set; }
        public int olej_silnikowy_dozmiany { get; set; }
        public int plyn_hamulcowy_zmiana { get; set; }
        public int plyn_hamulcowy_dozmiany { get; set; }
        public int plyn_chlodniczy_zmiana { get; set; }
        public int plyn_chlodniczy_dozmiany { get; set; }
        public int plyn_wspomagania_zmiana { get; set; }
        public int plyn_wspomagania_dozmiany { get; set; }
        public int plyn_skrzyni_zmiana { get; set; }
        public int plyn_skrzyni_dozmiany { get; set; }
    }
    public class Opisy
    {
        public int id { get; set; }
        public int id_samochodu { get; set; }
        public string opis { get; set; }
        public DateTime data { get; set; }
    }
    public class Samochod
    {
        public int id { get; set; }
        public bool Zaznaczony { get; set; }
        public string marka { get; set; }
        public string model { get; set; }
        public string numer_rejestracyjny { get; set; }
        public string vin { get; set; }
        public DateTime data_przegladu { get; set; }
        public DateTime data_nastepnego_przegladu { get; set; }
        public int rok_produkcji { get; set; }
        public int przebieg { get; set; }
    }
    public class NowySamochod
    {
        public int id { get; set; }
        public bool Zaznaczony { get; set; }
        public string marka { get; set; }
        public string model { get; set; }
        public string numer_rejestracyjny { get; set; }
        public string vin { get; set; }
        public DateTime data_przegladu { get; set; }
        public DateTime data_nastepnego_przegladu { get; set; }
        public int rok_produkcji { get; set; }
        public int przebieg { get; set; }
    }

    public partial class MainPage : ContentPage
    {
        private readonly MainPageViewModel _viewModel;

        public MainPage()
        {
            InitializeComponent();
            _viewModel = new MainPageViewModel();
            BindingContext = _viewModel;
            Device.StartTimer(TimeSpan.FromSeconds(1), () =>
            {
                // Aktualizuje tekst na podstawie bieżącego czasu
                dataigodzina.Text = "Godzina: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                return true;
            });
            MainPageViewModel viewModel = new MainPageViewModel();
            BindingContext = _viewModel;
            viewModel.Przypomnienie();

        }
        private void Plyny(object sender, EventArgs e)
        {
            var button = (ImageButton)sender;
            var samochod = (Samochod)button.BindingContext;

            if (samochod != null)
            {
                using (var dbContext = new AppDbContext())
                {
                    Debug.WriteLine($"Kliknięto przycisk o Id: {samochod.id}");
                    var newPage = new PlynyPage(samochod); // Przekazanie danych do nowej strony
                    Application.Current.MainPage.Navigation.PushAsync(newPage);
                }
            }
        }
        public class PlynyPage : ContentPage
        {
            private Samochod _samochod;
            private Entry olejEntry;
            private Entry olejEntrypo;
            private Entry hamulcowyEntry;
            private Entry hamulcowyEntrypo;
            private Entry chlodniczyEntry;
            private Entry chlodniczyEntrypo;
            private Entry wspomaganiaEntry;
            private Entry wspomaganiaEntrypo;
            private Entry skrzyniEntry;
            private Entry skrzyniEntrypo;

            public PlynyPage(Samochod samochod)
            {
                _samochod = samochod;

                Title = $"Plyny_{samochod.id}";
                var data = DateTime.Now.ToString("yyyy.MM.dd");

                using (var dbContext = new AppDbContext())
                {
                    var plyny = dbContext.plyny.FirstOrDefault(p => p.id_samochodu == samochod.id);

                    olejEntry = CreateEntry("Wprowadź przebieg oleju silnikowego", plyny?.olej_silnikowy_zmiana.ToString() ?? "");
                    olejEntrypo = CreateEntry("Wprowadź przebieg do zmiany oleju silnikowego", plyny?.olej_silnikowy_dozmiany.ToString() ?? "");
                    var zapiszolejButton = CreateButton("Zapisz olej silnikowy", ZapiszButton_Clicked);

                    hamulcowyEntry = CreateEntry("Wprowadź przebieg płynu hamulcowego", plyny?.plyn_hamulcowy_zmiana.ToString() ?? "");
                    hamulcowyEntrypo = CreateEntry("Wprowadź przebieg do zmiany płynu hamulcowego", plyny?.plyn_hamulcowy_dozmiany.ToString() ?? "");
                    var zapiszhamulcowyButton = CreateButton("Zapisz płyn hamulcowy", ZapiszButton_Clicked);

                    chlodniczyEntry = CreateEntry("Wprowadź przebieg płynu chłodniczego", plyny?.plyn_chlodniczy_zmiana.ToString() ?? "");
                    chlodniczyEntrypo = CreateEntry("Wprowadź przebieg do zmiany płynu chłodniczego", plyny?.plyn_chlodniczy_dozmiany.ToString() ?? "");
                    var zapiszchlodniczyButton = CreateButton("Zapisz płyn chłodniczy", ZapiszButton_Clicked);

                    wspomaganiaEntry = CreateEntry("Wprowadź przebieg płynu wspomagania", plyny?.plyn_wspomagania_zmiana.ToString() ?? "");
                    wspomaganiaEntrypo = CreateEntry("Wprowadź przebieg do zmiany płynu wspomagania", plyny?.plyn_wspomagania_dozmiany.ToString() ?? "");
                    var zapiszwspomaganiaButton = CreateButton("Zapisz płyn wspomagania", ZapiszButton_Clicked);

                    skrzyniEntry = CreateEntry("Wprowadź przebieg płynu do skrzyni biegów", plyny?.plyn_skrzyni_zmiana.ToString() ?? "");
                    skrzyniEntrypo = CreateEntry("Wprowadź przebieg do zmiany płynu do skrzyni biegów", plyny?.plyn_skrzyni_dozmiany.ToString() ?? "");
                    var zapiszskrzyniButton = CreateButton("Zapisz płyn do skrzyni biegów", ZapiszButton_Clicked);

                    var scrollView = new ScrollView
                    {
                        Content = new StackLayout
                        {
                            Children = { olejEntry, olejEntrypo, zapiszolejButton, hamulcowyEntry, hamulcowyEntrypo, zapiszhamulcowyButton, chlodniczyEntry, chlodniczyEntrypo, zapiszchlodniczyButton, wspomaganiaEntry, wspomaganiaEntrypo, zapiszwspomaganiaButton, skrzyniEntry, skrzyniEntrypo, zapiszskrzyniButton }
                        },
                        Orientation = ScrollOrientation.Both
                    };

                    Content = scrollView;
                }
            }

            private Entry CreateEntry(string placeholder, string text)
            {
                return new Entry
                {
                    Placeholder = placeholder,
                    FontSize = 16,
                    Margin = new Thickness(10, 0, 10, 0),
                    Text = text
                };
            }

            private Button CreateButton(string text, EventHandler clickedHandler)
            {
                var button = new Button
                {
                    Text = text,
                    FontSize = 20,
                    BackgroundColor = Color.FromHex("#4CAF50"),
                    TextColor = Color.FromHex("#FFFFFF"),
                    Margin = new Thickness(10, 5, 10, 10)
                };

                button.Clicked += clickedHandler;

                return button;
            }
            private void ZapiszButton_Clicked(object sender, EventArgs e)
            {
                using (var dbContext = new AppDbContext())
                {
                    var plyny = dbContext.plyny.FirstOrDefault(p => p.id_samochodu == _samochod.id);

                    if (plyny == null)
                    {
                        // Jeśli nie istnieje, utwórz nowy obiekt Plyny
                        plyny = new Plyny
                        {
                            id_samochodu = _samochod.id
                        };
                        dbContext.plyny.Add(plyny);
                    }

                    plyny.olej_silnikowy_zmiana = string.IsNullOrEmpty(olejEntry.Text) ? 0 : int.Parse(olejEntry.Text);
                    plyny.olej_silnikowy_dozmiany = string.IsNullOrEmpty(olejEntrypo.Text) ? 0 : int.Parse(olejEntrypo.Text);
                    plyny.plyn_hamulcowy_zmiana = string.IsNullOrEmpty(hamulcowyEntry.Text) ? 0 : int.Parse(hamulcowyEntry.Text);
                    plyny.plyn_hamulcowy_dozmiany = string.IsNullOrEmpty(hamulcowyEntrypo.Text) ? 0 : int.Parse(hamulcowyEntrypo.Text);
                    plyny.plyn_chlodniczy_zmiana = string.IsNullOrEmpty(chlodniczyEntry.Text) ? 0 : int.Parse(chlodniczyEntry.Text);
                    plyny.plyn_chlodniczy_dozmiany = string.IsNullOrEmpty(chlodniczyEntrypo.Text) ? 0 : int.Parse(chlodniczyEntrypo.Text);
                    plyny.plyn_wspomagania_zmiana = string.IsNullOrEmpty(wspomaganiaEntry.Text) ? 0 : int.Parse(wspomaganiaEntry.Text);
                    plyny.plyn_wspomagania_dozmiany = string.IsNullOrEmpty(wspomaganiaEntrypo.Text) ? 0 : int.Parse(wspomaganiaEntrypo.Text);
                    plyny.plyn_skrzyni_zmiana = string.IsNullOrEmpty(skrzyniEntry.Text) ? 0 : int.Parse(skrzyniEntry.Text);
                    plyny.plyn_skrzyni_dozmiany = string.IsNullOrEmpty(skrzyniEntrypo.Text) ? 0 : int.Parse(skrzyniEntrypo.Text);

                    dbContext.SaveChanges();
                }
            }
        }

        private void Opis(object sender, EventArgs e)
        {
            var button = (ImageButton)sender;
            var samochod = (Samochod)button.BindingContext;

            if (samochod != null)
            {
                using (var dbContext = new AppDbContext())
                {
                    Debug.WriteLine($"Kliknięto przycisk o Id: {samochod.id}");
                    var newPage = new OpisSamochoduPage(samochod); // Przekazanie danych do nowej strony
                    Application.Current.MainPage.Navigation.PushAsync(newPage);
                }
            }
        }
        public class OpisSamochoduPage : ContentPage
        {
            private Samochod _samochod; // Przechowuje referencję do samochodu
            private Entry opisEntry; // Pole do wprowadzania opisu
            private StackLayout opisyStackLayout; // Layout do wyświetlania opisów

            public OpisSamochoduPage(Samochod samochod)
            {
                _samochod = samochod;

                Title = $"Opis_{samochod.id}";
                var data = DateTime.Now.ToString("yyyy.MM.dd");

                opisEntry = new Entry
                {
                    Placeholder = "Wprowadź opis",
                    FontSize = 16,
                    Margin = new Thickness(10, 0, 10, 0)
                };

                var zapiszButton = new Button
                {
                    Text = "Zapisz Opis",
                    FontSize = 20,
                    BackgroundColor = Color.FromHex("#4CAF50"), // Kolor zielony
                    TextColor = Color.FromHex("#FFFFFF"),
                    Margin = new Thickness(10, 5, 10, 10)
                };

                zapiszButton.Clicked += ZapiszButton_Clicked; // Dodaj obsługę zdarzenia kliknięcia

                opisyStackLayout = new StackLayout
                {
                    Spacing = 10
                };

                var label = new Label
                {
                    Text = $"Data: {data} \nDane samochodu:\nID: {samochod.id}; Marka: {samochod.marka}; Model: {samochod.model}; \nVIN: {samochod.vin}; Nr. rejestracyjny: {samochod.numer_rejestracyjny}; \nRok produkcji: {samochod.rok_produkcji}; \nPrzebieg: {samochod.przebieg}; \nData przeglądu: {samochod.data_przegladu.ToString("yyyy.MM.dd")}; Data następnego przeglądu: {samochod.data_nastepnego_przegladu.ToString("yyyy.MM.dd")};",
                    FontSize = 30,
                    BackgroundColor = Color.FromHex("#FF0000"),
                    Margin = 10
                };

                var scrollView = new ScrollView
                {
                    Content = new StackLayout
                    {
                        Children = { label, opisEntry, zapiszButton, opisyStackLayout }
                    },
                    Orientation = ScrollOrientation.Both // Ustawienie przewijania w obu kierunkach
                };

                Content = scrollView;

                WczytajOpisyZBazyDanych(); // Dodane wczytywanie opisów przy otwieraniu strony
            }

            private void ZapiszButton_Clicked(object sender, EventArgs e)
            {
                string wprowadzonyOpis = opisEntry.Text;
                using (var dbContext = new AppDbContext())
                {
                    var nowyOpis = new Opisy
                    {
                        id_samochodu = _samochod.id,
                        opis = wprowadzonyOpis,
                        data = DateTime.Now
                    };

                    dbContext.opisy.Add(nowyOpis);
                    dbContext.SaveChanges();
                }

                OdswiezListeOpisow(); // Dodane wczytywanie opisów po zapisie
                DisplayAlert("Zapisano", "Opis został zapisany w bazie danych.", "OK");
            }
            private void OdswiezListeOpisow()
            {
                using (var dbContext = new AppDbContext())
                {
                    var noweOpisy = dbContext.opisy.Where(o => o.id_samochodu == _samochod.id).ToList();
                    opisyStackLayout.Children.Clear(); // Wyczyść istniejące etykiety przed dodaniem nowych

                    foreach (var opis in noweOpisy)
                    {
                        var opisLabel = new Label
                        {
                            Text = $"Opis: {opis.opis}\nData: {opis.data.ToString("yyyy.MM.dd HH:mm:ss")}",
                            FontSize = 16,
                        };

                        opisyStackLayout.Children.Add(opisLabel);
                    }
                }
            }
            private void WczytajOpisyZBazyDanych()
            {
                using (var dbContext = new AppDbContext())
                {
                    var opisyZBazy = dbContext.opisy.Where(o => o.id_samochodu == _samochod.id).ToList();

                    // Wczytaj opisy do interfejsu użytkownika
                    foreach (var opis in opisyZBazy)
                    {
                        var opisLabel = new Label
                        {
                            Text = $"Opis: {opis.opis}\nData: {opis.data.ToString("yyyy.MM.dd HH:mm:ss")}",
                            FontSize = 16
                        };

                        opisyStackLayout.Children.Add(opisLabel);
                    }
                }
            }
        }

        private void OnScrollViewScrolled(object sender, ScrolledEventArgs e)
        {
            double przypietyPosition = 0;

            if (e.ScrollY > przypietyPosition)
            {
                // Przypnij element, jeśli przewijany obszar jest powyżej pewnej pozycji
                przypiety1.TranslationY = e.ScrollY - przypietyPosition;
                przypiety2.TranslationY = e.ScrollY - przypietyPosition;
            }
            else
            {
                przypiety1.TranslationY = 0;
                przypiety2.TranslationY = 0;
            }
        }
        public class MainPageViewModel : INotifyPropertyChanged
        {



            public void Przypomnienie()
            {
                using (var dbContext = new AppDbContext())
                {
                    var daneDoWyswietlenia = PobierzNajblizszeDatyZBazy(); // Metoda, która pobiera dane z bazy danych
                    var daneDoWyswietlenia1 = PobierzPrzedawnioneDatyZBazy(); // Metoda, która pobiera dane z bazy danych
                    string trescAlertu = "Najbliższe daty przeglądów:\n";
                    string trescAlertu1 = "Przedawnione daty przeglądów:\n";

                    foreach (var data in daneDoWyswietlenia)
                    {
                        trescAlertu += $"{data.DataPrzegladu.ToString("yyyy-MM-dd")} - {data.Marka} {data.Model}\n";
                    }
                    foreach (var przedawnione in daneDoWyswietlenia1)
                    {
                        trescAlertu1 += $"{przedawnione.DataPrzegladu.ToString("yyyy-MM-dd")} - {przedawnione.Marka} {przedawnione.Model}\n";
                    }
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        await Application.Current.MainPage.DisplayAlert("Przypomnienie", $"{trescAlertu}\n{trescAlertu1}", "OK");
                    });
                }
            }
            public ICommand Raport => new Command(PokazNajblizszeDaty);


            private void PokazNajblizszeDaty()
            {
                using (var dbContext = new AppDbContext())
                {
                    var daneDoWyswietlenia = PobierzNajblizszeDatyZBazy(); // Metoda, która pobiera dane z bazy danych
                    var daneDoWyswietlenia1 = PobierzPrzedawnioneDatyZBazy(); // Metoda, która pobiera dane z bazy danych
                    string trescAlertu = "Najbliższe daty przeglądów:\n";
                    string trescAlertu1 = "Przedawnione daty przeglądów:\n";
                    int liczbaWierszy = samochody.Count();
                    var dzisisiejszadata = DateTime.Now.ToString("yyyy-MM-dd");

                    foreach (var data in daneDoWyswietlenia)
                    {
                        trescAlertu += $"{data.DataPrzegladu.ToString("yyyy-MM-dd")} - {data.Marka} {data.Model}\n";
                    }
                    foreach (var przedawnione in daneDoWyswietlenia1)
                    {
                        trescAlertu1 += $"{przedawnione.DataPrzegladu.ToString("yyyy-MM-dd")} - {przedawnione.Marka} {przedawnione.Model}\n";
                    }
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        var result = await Application.Current.MainPage.DisplayAlert("Raport o flocie pojazdów", $"Liczba samochodów w bazie danych: {liczbaWierszy}\n\n{trescAlertu}\n{trescAlertu1}", "OK", "Pobierz raport");
                        if (result)
                        {
                        }
                        else
                        {
                            var location = await FilePicker.PickAsync();

                            if (location != null)
                            {
                                string textToSave = $"Raport został wygenerowany: {dzisisiejszadata}\n\nLiczba samochodów w bazie danych: {liczbaWierszy}\n\n{trescAlertu}\n{trescAlertu1}";
                                string filePath = Path.Combine(location.FullPath);
                                await File.WriteAllTextAsync(filePath, textToSave);

                            }
                        }
                    });

                }
            }
            private List<DaneDoWyswietlenia> PobierzNajblizszeDatyZBazy()
            {
                using (var dbContext = new AppDbContext())
                {
                    // Pobierz daty przeglądów w ciągu trzech miesięcy od dzisiaj
                    var dzis = DateTime.Now;
                    var trzyMiesiaceWPrzod = dzis.AddMonths(3);

                    return dbContext.samochody
                        .Where(s => s.data_nastepnego_przegladu >= dzis && s.data_nastepnego_przegladu <= trzyMiesiaceWPrzod)
                        .Select(s => new DaneDoWyswietlenia
                        {
                            Marka = s.marka,
                            Model = s.model,
                            DataPrzegladu = s.data_nastepnego_przegladu,
                        })
                        .OrderBy(d => d.DataPrzegladu)
                        .ToList();
                }
            }

            private List<DaneDoWyswietlenia> PobierzPrzedawnioneDatyZBazy()
            {
                using (var dbContext = new AppDbContext())
                {
                    // Pobierz daty przeglądów przedawnione
                    var dzis = DateTime.Now;

                    return dbContext.samochody
                        .Where(s => s.data_nastepnego_przegladu < dzis)
                        .Select(s => new DaneDoWyswietlenia
                        {
                            Marka = s.marka,
                            Model = s.model,
                            DataPrzegladu = s.data_nastepnego_przegladu
                        })
                        .OrderBy(d => d.DataPrzegladu)
                        .ToList();
                }
            }
            public class DaneDoWyswietlenia
            {
                public string Marka { get; set; }
                public string Model { get; set; }
                public DateTime DataPrzegladu { get; set; }

            }


            public ICommand AktualizujZaznaczoneSamochodyCommand => new Command(AktualizujZaznaczoneSamochody);

            private void AktualizujZaznaczoneSamochody()
            {
                using (var dbContext = new AppDbContext())
                {
                    var zaznaczoneSamochody = samochody.Where(s => s.Zaznaczony).ToList();

                    foreach (var samochod in zaznaczoneSamochody)
                    {
                        var samochodToEdit = dbContext.samochody.FirstOrDefault(s => s.id == samochod.id);

                        if (samochodToEdit != null)
                        {
                            // Edycja
                            samochodToEdit.data_przegladu = samochod.data_przegladu;
                            samochodToEdit.data_nastepnego_przegladu = samochod.data_nastepnego_przegladu;
                            samochodToEdit.przebieg = samochod.przebieg;
                        }
                    }
                    dbContext.SaveChanges();

                    OdswiezListeSamochodow();
                }
            }

            private DateTime _dzisiejszaData = DateTime.Now;
            private DateTime _dataZaRok = DateTime.Now.AddYears(1);

            public DateTime DzisiejszaData
            {
                get => _dzisiejszaData;
                set
                {
                    _dzisiejszaData = value;
                    OnPropertyChanged(nameof(DzisiejszaData));
                    NowySamochod.data_przegladu = value;
                }
            }

            public DateTime DataZaRok
            {
                get => _dataZaRok;
                set
                {
                    _dataZaRok = value;
                    OnPropertyChanged(nameof(DataZaRok));
                    NowySamochod.data_nastepnego_przegladu = value;
                }
            }

            public ICommand ZmienDzisiejszaDateCommand => new Command(() => ZmienDate(DzisiejszaData, out DateTime result));

            public ICommand ZmienDateZaRokCommand => new Command(() => ZmienDate(DataZaRok, out DateTime result));

            private void ZmienDate(DateTime date, out DateTime result)
            {
                result = date;
                OnPropertyChanged("DzisiejszaData");
                OnPropertyChanged("DataZaRok");
            }

            public event PropertyChangedEventHandler PropertyChanged;

            private NowySamochod _nowySamochod;

            public NowySamochod NowySamochod
            {
                get => _nowySamochod;
                set
                {
                    _nowySamochod = value;
                    OnPropertyChanged(nameof(NowySamochod));
                }
            }
            private ObservableCollection<Samochod> _samochody;

            public ObservableCollection<Samochod> samochody
            {
                get => _samochody;
                set
                {
                    _samochody = value;
                    OnPropertyChanged(nameof(samochody));
                }
            }
            public Command DodajNowySamochodCommand { get; }

            public MainPageViewModel()
            {
                using (var dbContext = new AppDbContext())
                {
                    samochody = new ObservableCollection<Samochod>(dbContext.samochody.ToList());
                    NowySamochod = new NowySamochod();
                    DodajNowySamochodCommand = new Command(() => DodajNowySamochod(NowySamochod));
                }
            }
            public void DodajNowySamochod(NowySamochod nowySamochod)
            {
                using (var dbContext = new AppDbContext())
                {
                    var nowyRekord = new Samochod
                    {
                        marka = nowySamochod.marka,
                        model = nowySamochod.model,
                        numer_rejestracyjny = nowySamochod.numer_rejestracyjny,
                        vin = nowySamochod.vin,
                        data_przegladu = nowySamochod.data_przegladu.Date,
                        data_nastepnego_przegladu = nowySamochod.data_nastepnego_przegladu.Date,
                        rok_produkcji = nowySamochod.rok_produkcji,
                        przebieg = nowySamochod.przebieg,
                    };

                    dbContext.samochody.Add(nowyRekord);
                    dbContext.SaveChanges();

                    samochody.Clear();
                    foreach (var samochod in dbContext.samochody.ToList())
                    {
                        samochody.Add(samochod); // Dodaje zaktualizowane dane
                    }
                    NowySamochod = new NowySamochod();
                }
            }

            public ICommand WyczyscCommand => new Command(Wyczysc);

            private void Wyczysc()
            {
                NowySamochod = new NowySamochod();
            }

            public ICommand UsunZaznaczoneSamochodyCommand => new Command(UsunZaznaczoneSamochody);

            private void UsunZaznaczoneSamochody()
            {
                using (var dbContext = new AppDbContext())
                {
                    var zaznaczoneSamochody = samochody.Where(s => s.Zaznaczony).ToList();

                    foreach (var samochod in zaznaczoneSamochody)
                    {
                        var samochodToDelete = dbContext.samochody.FirstOrDefault(s => s.id == samochod.id);

                        if (samochodToDelete != null)
                        {
                            dbContext.samochody.Remove(samochodToDelete);
                        }
                    }

                    dbContext.SaveChanges();

                    OdswiezListeSamochodow();
                }
            }

            private void OdswiezListeSamochodow()
            {
                using (var dbContext = new AppDbContext())
                {
                    samochody = new ObservableCollection<Samochod>(dbContext.samochody.ToList());
                }
            }
            protected virtual void OnPropertyChanged(string propertyName)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

            }

        }
    }

}
