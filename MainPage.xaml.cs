namespace CalculateurAge
{
    using CalculateurAge.Views;
    public partial class MainPage : ContentPage
    {
       
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnCalculerClicked(object sender, EventArgs e)
        {
            // validation : on refuse un nom vide
            if (String.IsNullOrWhiteSpace(entryNom.Text))
            {
                DisplayAlert("Erreur", "Entrez un nom", "ok");
                return;
            }

            DateTime d = (DateTime)pickerDate.Date;
            int age = DateTime.Today.Year - d.Year;

            if (d.Date > DateTime.Today.AddYears(-age)) age--;

            await Shell.Current.GoToAsync($"{nameof(ResultatPage)}?nom={entryNom.Text}&age={age}");
        }
    }
}
