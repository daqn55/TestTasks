using Task1.Models;

namespace Task1.Interfaces
{
    internal interface IMovieStarDataInput
    {
        public ICollection<MovieStar> GetMovieStarRawData();
    }
}
