// Bestand: Grocery.App.ViewModels/BoughtProductsViewModel.cs
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;
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

            Debug.WriteLine("vullen");
            if (Products.Any())
            {
                SelectedProduct = Products.First();
            }
            else
            {
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