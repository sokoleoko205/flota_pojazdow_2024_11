using Microsoft.EntityFrameworkCore;

namespace flotapojazdow1
{
    public class LogowanieContext : DbContext
    {
        public DbSet<Uzytkownik> dane_logowania { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("Server=localhost;Database=flota_pojazdow;User=root;Password=;");
        }
    }

    public class Uzytkownik
    {
        public int id { get; set; }
        public string login { get; set; }
        public string haslo { get; set; }
    }

    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void OnLoginButtonClicked(object sender, EventArgs e)
        {
            string username = UsernameEntry.Text;
            string password = PasswordEntry.Text;

            if (IsValidUser(username, password))
            {
                Navigation.PushAsync(new MainPage());
            }
            else
            {
                DisplayAlert("Logowanie nie powiod³o siê!", "Z³y login lub has³o.", "Spróbuj ponownie.");
            }
        }

        private bool IsValidUser(string username, string password)
        {
            using (var dbContext = new LogowanieContext())
            {
                // SprawdŸ, czy istnieje u¿ytkownik o podanym loginie i haœle
                return dbContext.dane_logowania.Any(u => u.login == username && u.haslo == password);
            }
        }
    }
}
