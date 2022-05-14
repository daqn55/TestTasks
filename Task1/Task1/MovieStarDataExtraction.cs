using Task1.Interfaces;
using Task1.Models;

namespace Task1
{
    internal class MovieStarDataExtraction : IMovieStarDataExtraction
    {
        private readonly ICollection<MovieStar> MovieStarsRawData;

        public MovieStarDataExtraction(ICollection<MovieStar> movieStarsRawData)
        {
            this.MovieStarsRawData = movieStarsRawData;
        }

        private string IterationMovieStar()
        {
            if (this.MovieStarsRawData == null)
            {
                throw new Exception("Invalid input!");
            }

            var currentMovieStarInformation = string.Empty;
            foreach (var movieStarData in this.MovieStarsRawData)
            {
                currentMovieStarInformation += string.Concat(movieStarData.Name, " ", movieStarData.Surname, Environment.NewLine);
                currentMovieStarInformation += string.Concat(movieStarData.Sex, Environment.NewLine);
                currentMovieStarInformation += string.Concat(movieStarData.Nationality, Environment.NewLine);

                var age = CalculateMovieStarAge(movieStarData.DateOfBirth);

                currentMovieStarInformation += string.Concat(age, Environment.NewLine, Environment.NewLine);
            }

            return currentMovieStarInformation;
        }

        private string CalculateMovieStarAge(DateTime birthday)
        {
            var today = DateTime.Today;
            var age = today.Year - birthday.Year;

            if (birthday.Month > today.Month)
            {
                age--;
            }
            else if (birthday.Day > today.Day)
            {
                age--;
            }
            return $"{age} years old";
        }

        public void PrintMovieStarData()
        {
            var dataToPrint = IterationMovieStar();

            Console.WriteLine(dataToPrint);
        }
    }
}
