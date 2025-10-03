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
        }

        private void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (BindingContext is BoughtProductsViewModel viewModel)
            {
                System.Diagnostics.Debug.WriteLine($"Selected Product Changed: {viewModel.SelectedProduct?.Name}");
            }
        }
    }
}