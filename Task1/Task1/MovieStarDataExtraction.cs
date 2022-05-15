using System.Text;
using Task1.Interfaces;
using Task1.Models;

namespace Task1
{
    internal class MovieStarDataExtraction : IMovieStarDataExtraction
    {
        private readonly ICollection<MovieStar> _movieStarsRawData;

        private const string ERROR_MSG = "Invalid input!";
        private const string AGE_MSG = "{0} years old";

        public MovieStarDataExtraction(ICollection<MovieStar> movieStarsRawData)
        {
            _movieStarsRawData = movieStarsRawData;

            if (_movieStarsRawData == null)
            {
                throw new Exception(ERROR_MSG);
            }
        }

        private string IterationOfMovieStars()
        {
            var movieStarOutput = string.Empty;
            foreach (var movieStarData in _movieStarsRawData)
            {
               movieStarOutput += CreateMovieStar(movieStarData);
            }

            return movieStarOutput;
        }

        private string CreateMovieStar(MovieStar movieStarData)
        {
            var movieStar = new StringBuilder();

            movieStar.AppendLine(movieStarData.Name + " " + movieStarData.Surname);
            movieStar.AppendLine(movieStarData.Sex);
            movieStar.AppendLine(movieStarData.Nationality);

            var age = CalculateMovieStarAge(movieStarData.DateOfBirth);

            movieStar.AppendLine(age);
            movieStar.AppendLine();

            return movieStar.ToString();
        }

        private string CalculateMovieStarAge(DateTime birthday)
        {
            var today = DateTime.Today;
            var age = today.Year - birthday.Year;

            if (birthday.Month > today.Month || birthday.Day > today.Day)
            {
                age--;
            }

            return string.Format(AGE_MSG, age);
        }

        public void PrintMovieStarData()
        {
            var dataToPrint = IterationOfMovieStars();

            Console.WriteLine(dataToPrint);
        }
    }
}
