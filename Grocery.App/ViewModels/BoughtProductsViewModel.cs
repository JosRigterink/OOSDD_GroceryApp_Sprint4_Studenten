// Bestand: Grocery.App.ViewModels/BoughtProductsViewModel.cs
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;
using System.Collections.ObjectModel;
using System.Linq;

namespace Grocery.App.ViewModels
{
    public partial class BoughtProductsViewModel : BaseViewModel
    {
        private readonly IBoughtProductsService _boughtProductsService;

        [ObservableProperty]
        Product selectedProduct;
        public ObservableCollection<BoughtProducts> BoughtProductsList { get; set; } = [];
        public ObservableCollection<Product> Products { get; set; }

        public BoughtProductsViewModel(IBoughtProductsService boughtProductsService, IProductService productService)
        {
            _boughtProductsService = boughtProductsService;
            Products = new(productService.GetAll());

            // Vervang comments met functionele code om de lijst bij het opstarten te vullen.
            // Dit zorgt ervoor dat de lijst direct gevuld is, zelfs voordat de gebruiker een selectie maakt.
            // De OnSelectedProductChanged methode wordt hier effectief getriggerd door de initiële toewijzing.
            // Als er producten zijn, selecteer het eerste product om de lijst te initialiseren.
            if (Products.Any())
            {
                SelectedProduct = Products.First();
            }
            else
            {
                // Als er geen producten zijn, initialiseer de lijst met alle gekochte producten (geen filter)
                // Dit is de fallback als de 'Products' lijst leeg is.
                var allBoughtProducts = _boughtProductsService.Get(null); // Roep aan zonder productId filter
                foreach (var boughtProduct in allBoughtProducts)
                {
                    BoughtProductsList.Add(boughtProduct);
                }
            }
        }

        partial void OnSelectedProductChanged(Product? oldValue, Product newValue)
        {
            // Zorg dat de lijst BoughtProductsList met de gegevens die passen bij het geselecteerde product.
            BoughtProductsList.Clear(); // Leeg de huidige lijst

            if (newValue != null)
            {
                // Haal de gekochte producten op voor het nieuwe geselecteerde product
                var boughtProducts = _boughtProductsService.Get(newValue.Id);
                foreach (var boughtProduct in boughtProducts)
                {
                    BoughtProductsList.Add(boughtProduct);
                }
            }
            else
            {
                // Als newValue null is (bijv. als er geen product geselecteerd is),
                // kun je er ook voor kiezen om alle gekochte producten te tonen
                // of de lijst leeg te laten. Ik kies hier voor alle producten.
                var allBoughtProducts = _boughtProductsService.Get(null);
                foreach (var boughtProduct in allBoughtProducts)
                {
                    BoughtProductsList.Add(boughtProduct);
                }
            }
        }

        [RelayCommand]
        public void NewSelectedProduct(Product product)
        {
            SelectedProduct = product;
        }
    }
}