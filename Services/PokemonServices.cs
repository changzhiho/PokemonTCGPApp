using System.Net.Http.Json;
using PokemonTCGApp.Models;

namespace PokemonTCGApp.Services
{
    public class PokemonService
    {
        private readonly HttpClient _httpClient;
        private const string ApiBaseUrl = "https://api.pokemontcg.io/v2/cards";

        public PokemonService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        }

        public async Task<List<Card>> SearchCardsAsync(string name)
        {
            // Vérifie que le nom n'est pas vide ou null
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Le nom de la carte ne peut pas être vide.", nameof(name));

            // Construit l'URL de la requête avec le nom du Pokémon recherché
            var url = $"{ApiBaseUrl}?q=name:{name}";

            try
            {
                // Effectue la requête HTTP et récupère la réponse sous forme de ApiResponse
                var response = await _httpClient.GetFromJsonAsync<ApiResponse>(url);
                // Retourne la liste de cartes récupérée ou une liste vide si aucune donnée n'est trouvée
                return response?.Data ?? new List<Card>();
            }
            catch (HttpRequestException ex)
            {
                // Gestion des erreurs réseau : affiche un message et retourne une liste vide
                Console.Error.WriteLine($"Erreur API: {ex.Message}");
                return new List<Card>();
            }
        }
    }

    public class ApiResponse
    {
        public List<Card> Data { get; set; } = new List<Card>();
    }
}
