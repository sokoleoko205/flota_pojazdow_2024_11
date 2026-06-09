namespace flotapojazdow1;

public partial class PrzykladGenerowanie : ContentPage
{
	public PrzykladGenerowanie()
	{
		InitializeComponent();
	}
    public static readonly BindableProperty IdProperty = BindableProperty.Create(nameof(Id), typeof(string), typeof(PrzykladGenerowanie), "");

    public string Id
    {
        get { return (string)GetValue(IdProperty); }
        set { SetValue(IdProperty, value); }
    }

    private void OnButtonClicked(object sender, EventArgs e)
    {
        // W przypadku klikniêcia przycisku, mo¿esz u¿yæ Id lub innych w³aœciwoœci zwi¹zanych z t¹ stron¹
        // Na przyk³ad, mo¿esz u¿yæ Id w CommandParameter przycisku
        var button = sender as Button;
        if (button != null)
        {
            // Poni¿ej przekazujemy CommandParameter, które zawiera "Page" + Id do metody obs³ugi zdarzenia w MainPage.xaml.cs
            button.CommandParameter = $"Page{Id}";
        }
    }
}