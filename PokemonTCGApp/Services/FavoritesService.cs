using PokemonTCGApp.Models;
using System.Text.Json;

namespace PokemonTCGApp.Services
{
    public class FavoritesService
    {
        private const string StorageKey = "pokemon-favorite";  // Key for local storage
        private List<Card> _favorites = new List<Card>();  // List of favorite cards

        // Constructor: Initializes the service by loading favorites from local storage
        public FavoritesService()
        {
            LoadFavorites();  // Loads the favorites when the service is instantiated
        }

        // Returns the list of favorite cards
        public List<Card> GetFavorites() => _favorites;

        // Adds a card to favorites if it is not already added
        public void AddFavorite(Card card)
        {
            if (!_favorites.Any(f => f.Id == card.Id))  // Check if the card is not already in favorites
            {
                _favorites.Add(card);  // Add the card to the favorites list
                SaveFavorites();  // Save the updated list to local storage
            }
        }

        // Removes a card from favorites by its ID
        public void RemoveFavorite(string cardId)
        {
            _favorites.RemoveAll(f => f.Id == cardId);  // Remove the card with the matching ID
            SaveFavorites();  // Save the updated list to local storage
        }

        // Checks if a card is in the favorites list
        public bool IsFavorite(string cardId) => _favorites.Any(f => f.Id == cardId);

        // Loads the list of favorites from local storage
        private void LoadFavorites()
        {
            var json = localStorage.GetItem(StorageKey);  // Get the favorites list from local storage
            if (!string.IsNullOrEmpty(json))  // If the JSON data is not empty
            {
                _favorites = JsonSerializer.Deserialize<List<Card>>(json) ?? new List<Card>();  // Deserialize the JSON to a list of cards, default to an empty list if null
            }
        }

        // Saves the current list of favorites to local storage
        private void SaveFavorites()
        {
            var json = JsonSerializer.Serialize(_favorites);  // Serialize the list of favorites into JSON
            localStorage.SetItem(StorageKey, json);  // Store the JSON in local storage
        }

        // Initializes the favorites service (can be used if some setup is needed)
        public async Task InitializeAsync()
        {
            // Any initialization logic can be placed here, like loading data asynchronously if needed
            await Task.CompletedTask;
        }
    }
}
