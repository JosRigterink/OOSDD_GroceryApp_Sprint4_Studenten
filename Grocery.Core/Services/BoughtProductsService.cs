// Bestand: Grocery.Core.Services/BoughtProductsService.cs
using Grocery.Core.Interfaces.Repositories;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;
using System.Linq; // Nodig voor LINQ methoden zoals Where, Select, FirstOrDefault

namespace Grocery.Core.Services
{
    public class BoughtProductsService : IBoughtProductsService
    {
        private readonly IGroceryListItemsRepository _groceryListItemsRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IProductRepository _productRepository;
        private readonly IGroceryListRepository _groceryListRepository;

        public BoughtProductsService(IGroceryListItemsRepository groceryListItemsRepository, IGroceryListRepository groceryListRepository, IClientRepository clientRepository, IProductRepository productRepository)
        {
            _groceryListItemsRepository = groceryListItemsRepository;
            _groceryListRepository = groceryListRepository;
            _clientRepository = clientRepository;
            _productRepository = productRepository;
        }

        public List<BoughtProducts> Get(int? productId)
        {
            List<BoughtProducts> boughtProductsList = new List<BoughtProducts>();

            // Haal alle boodschappenlijst items op
            var allGroceryListItems = _groceryListItemsRepository.GetAll();

            // Filter de items op het meegegeven productId, indien aanwezig
            var filteredGroceryListItems = productId.HasValue
                ? allGroceryListItems.Where(item => item.ProductId == productId.Value)
                : allGroceryListItems; // Als productId null is, neem alle items

            foreach (var item in filteredGroceryListItems)
            {
                // Haal de bijbehorende Product, GroceryList en Client op
                Product? product = _productRepository.Get(item.ProductId);
                GroceryList? groceryList = _groceryListRepository.Get(item.GroceryListId);
                Client? client = groceryList != null ? _clientRepository.Get(groceryList.ClientId) : null; // Haal client op via de boodschappenlijst

                // Voeg alleen toe als alle benodigde onderdelen gevonden zijn
                if (client != null && groceryList != null && product != null)
                {
                    boughtProductsList.Add(new BoughtProducts(client,groceryList,product)
                    {
                        Client = client,
                        GroceryList = groceryList,
                        Product = product
                    });
                }
            }

            return boughtProductsList;
        }
    }
}