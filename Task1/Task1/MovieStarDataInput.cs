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
        private readonly string FileLocation = @"InputFiles\input.txt";
        private readonly string ErrorMsg = "Invalid input!";

        public MovieStarDataInput() { }

        public ICollection<MovieStar> GetMovieStarRawData()
        {
            var inputData = ImportJsonDataFromTxtFile(this.FileLocation);

            try
            {
                var data = JsonConvert.DeserializeObject<ICollection<MovieStar>>(inputData);

                if (data == null)
                {
                    throw new Exception(ErrorMsg);
                }

                return data;
            }
            catch (Exception)
            {
                throw new Exception(ErrorMsg);
            }
        }

        private string ImportJsonDataFromTxtFile(string fileLocation)
        {
            var input = File.ReadAllText(fileLocation);

            return input;
        }
    }
}
