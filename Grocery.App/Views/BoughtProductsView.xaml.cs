// Bestand: Grocery.App.Views/BoughtProductsView.xaml.cs
using Microsoft.Maui.Controls;
using Grocery.App.ViewModels;

namespace Grocery.App.Views
{
    public partial class BoughtProductsView : ContentPage
    {
        public BoughtProductsView()
        {
            InitializeComponent();
            // ViewModel instellen, idealiter via Dependency Injection
            // DataContext = App.Services.GetService<BoughtProductsViewModel>(); // voorbeeld
        }

        private void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Deze methode wordt aangeroepen wanneer de selectie in de Picker verandert.
            // De ViewModel's SelectedProduct property wordt automatisch bijgewerkt
            // door de TwoWay binding, dus hier hoeft in principe geen logica te staan
            // tenzij je specifieke UI-updates wilt doen die niet via bindingen gaan.
            // We kunnen hier optioneel debuggen om te zien of het werkt:
            if (BindingContext is BoughtProductsViewModel viewModel)
            {
                System.Diagnostics.Debug.WriteLine($"Selected Product Changed: {viewModel.SelectedProduct?.Name}");
            }
        }
    }
}