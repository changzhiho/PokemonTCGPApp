using System.Text.Json; // Pour la sérialisation et la désérialisation JSON
using Microsoft.JSInterop; // Pour interagir avec JavaScript
using PokemonTCGApp.Models; // Importation des modèles de l'application

namespace PokemonTCGApp.Services
{
    public class FavoritesService
    {
        private const string StorageKey = "pokemon-favorite"; // Clé de stockage pour les favoris
        private List<Card> _favorites = new List<Card>(); // Liste des cartes favorites
        private readonly IJSRuntime _jsRuntime; // Interface pour l'exécution JavaScript

        public FavoritesService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime; // Initialisation de l'exécution JavaScript
        }

        public async Task InitializeAsync()
        {
            await LoadFavorites(); // Chargement des favoris à l'initialisation
        }

        public List<Card> GetFavorites() => _favorites; // Obtention de la liste des favoris

        public async Task AddFavorite(Card card)
        {
            if (!_favorites.Any(f => f.Id == card.Id))
            {
                _favorites.Add(card); // Ajout de la carte à la liste des favoris si elle n'est pas déjà présente
                await SaveFavorites(); // Sauvegarde des favoris après ajout
            }
        }

        public async Task RemoveFavorite(string cardId)
        {
            _favorites.RemoveAll(f => f.Id == cardId); // Suppression de la carte favorite avec l'ID correspondant
            await SaveFavorites(); // Sauvegarde des favoris après suppression
        }

        public bool IsFavorite(string cardId) => _favorites.Any(f => f.Id == cardId); // Vérification si une carte est favorite

        private async Task LoadFavorites()
        {
            var json = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", StorageKey); // Récupération des favoris depuis le stockage local
            if (!string.IsNullOrEmpty(json))
            {
                _favorites = JsonSerializer.Deserialize<List<Card>>(json) ?? new List<Card>(); // Désérialisation des favoris en une liste de cartes
            }
        }

        private async Task SaveFavorites()
        {
            var json = JsonSerializer.Serialize(_favorites); // Sérialisation des favoris en JSON
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", StorageKey, json); // Sauvegarde des favoris dans le stockage local
        }
    }
}
