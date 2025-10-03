// Bestand: Grocery.Core.Services/ClientService.cs
using Grocery.Core.Interfaces.Repositories;
using Grocery.Core.Interfaces.Services;
using Grocery.Core.Models;
using System.Collections.Generic;

namespace Grocery.Core.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        private Client? _currentClient; // <<< Zorg dat dit veld bestaat!

        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        // <<< Zorg dat deze methoden bestaan!
        public Client? GetCurrentClient()
        {
            return _currentClient;
        }

        public void SetCurrentClient(Client client)
        {
            _currentClient = client;
        }
        // <<< Einde van de methoden

        public Client? Get(string email)
        {
            return _clientRepository.Get(email);
        }

        public Client? Get(int id)
        {
            return _clientRepository.Get(id);
        }

        public List<Client> GetAll()
        {
            List<Client> clients = _clientRepository.GetAll();
            return clients;
        }

        // Extra: Je login methode moet SetCurrentClient aanroepen!
        // Dit is een voorbeeld. Je hebt waarschijnlijk al een Login methode in een AuthService of ergens anders.
        // Zorg ervoor dat na een succesvolle login, SetCurrentClient wordt aangeroepen.
        /*
        public Client? Login(string email, string password)
        {
            Client? client = _clientRepository.Get(email);
            // Je wachtwoord hash/salt/verificatie logica hier
            if (client != null && VerifyPassword(password, client.Password)) // Vervang VerifyPassword door je eigen logica
            {
                SetCurrentClient(client); // Belangrijk: stel de huidige client in!
                return client;
            }
            SetCurrentClient(null); // Stel in op null als login mislukt
            return null;
        }
        */
    }
}