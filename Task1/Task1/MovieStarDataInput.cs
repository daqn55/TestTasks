using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task1.Interfaces;
using Task1.Models;
using Newtonsoft.Json;

namespace Task1
{
    internal class MovieStarDataInput : IMovieStarDataInput
    {
        private const string FILE_LOCATION = @"InputFiles\input.txt";
        private const string ERROR_MSG = "Invalid input!";

        public ICollection<MovieStar> GetMovieStarRawData()
        {
            var inputData = ImportJsonDataFromTxtFile(FILE_LOCATION);

            try
            {
                var data = JsonConvert.DeserializeObject<ICollection<MovieStar>>(inputData);

                if (data == null)
                {
                    throw new Exception(ERROR_MSG);
                }

                return data;
            }
            catch (Exception)
            {
                throw new Exception(ERROR_MSG);
            }
        }

        private string ImportJsonDataFromTxtFile(string fileLocation)
        {
            var input = File.ReadAllText(fileLocation);

            return input;
        }
    }
}
