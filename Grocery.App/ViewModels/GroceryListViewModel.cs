// Bestand: Grocery.App.ViewModels/GroceryListViewModel.cs
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;
using System.Collections.ObjectModel;
using Grocery.Core; // Nodig voor de Role enum
using Grocery.App.Views;
using System.Diagnostics; // Nodig voor navigatie naar BoughtProductsView

namespace Grocery.App.ViewModels
{
    public partial class GroceryListViewModel : BaseViewModel
    {
        public ObservableCollection<GroceryList> GroceryLists { get; set; }
        private readonly IGroceryListService _groceryListService;
        private readonly IClientService _clientService;

        // Nieuwe property om de huidige client te binden in de View
        [ObservableProperty]
        Client? currentClient;

        public GroceryListViewModel(IGroceryListService groceryListService, IClientService clientService)
        {
            Title = "Boodschappenlijst";
            _groceryListService = groceryListService;
            _clientService = clientService;

            // Initialiseer de CurrentClient property bij het aanmaken van de ViewModel
            CurrentClient = _clientService.GetCurrentClient();

            GroceryLists = new(_groceryListService.GetAll());
        }

        [RelayCommand]
        public async Task SelectGroceryList(GroceryList groceryList)
        {
            Dictionary<string, object> paramater = new() { { nameof(GroceryList), groceryList } };
            await Shell.Current.GoToAsync($"{nameof(Views.GroceryListItemsView)}?Titel={groceryList.Name}", true, paramater);
        }

        [RelayCommand]
        public async Task ShowBoughtProducts()
        {
            // Gebruik de reeds ingestelde CurrentClient property
            if (CurrentClient != null && CurrentClient.Role == Role.Admin)
            {
                Console.WriteLine("doet die het?");
                await Shell.Current.GoToAsync(nameof(BoughtProductsView));
            }
        }

        public override void OnAppearing()
        {
            base.OnAppearing();
            GroceryLists = new(_groceryListService.GetAll());
            // Zorg ervoor dat de CurrentClient ook wordt ververst mocht deze veranderen (bijv. na logout/login)
            CurrentClient = _clientService.GetCurrentClient();
            //debug log om te kijken wie de current client is met welke role
            Debug.WriteLine($"Current Client: {CurrentClient?.Name ?? "N/A"}, Role: {CurrentClient?.Role.ToString() ?? "N/A"}");
        }

        public override void OnDisappearing()
        {
            base.OnDisappearing();
            GroceryLists.Clear();
        }
    }
}