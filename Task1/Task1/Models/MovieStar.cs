using Newtonsoft.Json;

namespace Task1.Models
{
    internal class MovieStar
    {
        [JsonProperty("dateOfBirth")]
        public DateTime DateOfBirth { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Sex { get; set; }

        public string Nationality { get; set; }
    }
}
