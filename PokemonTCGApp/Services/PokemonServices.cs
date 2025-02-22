using System.Net.Http.Json;
using PokemonTCGApp.Models;

namespace PokemonTCGApp.Services
{
    public class PokemonService
    {
        private readonly HttpClient _httpClient;  // HttpClient for making API calls
        private const string ApiBaseUrl = "https://api.pokemontcg.io/v2/cards";  // Base URL for the Pokémon TCG API

        // Constructor: Initializes the PokemonService with the provided HttpClient
        public PokemonService(HttpClient httpClient)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));  // Ensure the HttpClient is not null
        }

        // Searches for Pokémon cards by name and returns a list of cards
        public async Task<List<Card>> SearchCardsAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))  // Ensure the name is not empty or null
                throw new ArgumentException("The card name cannot be empty.", nameof(name));  // Throw exception if name is invalid

            var url = $"{ApiBaseUrl}?q=name:{name}";  // Build the URL for the search query

            try
            {
                // Make the API request and parse the response as ApiResponse
                var response = await _httpClient.GetFromJsonAsync<ApiResponse>(url);
                return response?.Data ?? new List<Card>();  // Return the list of cards or an empty list if no data is found
            }
            catch (HttpRequestException ex)  // Catch any HTTP request exceptions
            {
                Console.Error.WriteLine($"API Error: {ex.Message}");  // Log the error
                return new List<Card>();  // Return an empty list on error
            }
        }
    }

    // Response model for the API response containing the list of cards
    public class ApiResponse
    {
        public List<Card> Data { get; set; } = new List<Card>();  // List of cards from the API
    }
}
