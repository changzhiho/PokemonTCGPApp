namespace PokemonTCGApp.Models
{
    // Represénte la structure de la carte
    public class Card
    {
        // Id de la carte 
        public string Id {get; set; } = string.Empty;
        // Nom de la carte
        public string Name {get; set; } = string.Empty;
        // Images associés à la carte
        public Images Images {get; set; } = new Images();
        // Liste pour les différents types
        public List<string> Types {get; set; } = new List<string>();
    }

    // Représente les images des cartes
    public class Images
    {
        // URL en petit
        public string Small {get; set; } = string.Empty;
        // URL en grand
        public string Large {get; set; } = string.Empty;
    }
}