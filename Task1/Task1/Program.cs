using Task1;
using Task1.Interfaces;

IMovieStarDataInput movieStarDataInput = new MovieStarDataInput();

var data = movieStarDataInput.GetMovieStarRawData();

IMovieStarDataExtraction movieStarDataExtraction = new MovieStarDataExtraction(data);

movieStarDataExtraction.PrintMovieStarData();
