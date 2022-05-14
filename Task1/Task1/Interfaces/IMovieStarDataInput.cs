using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task1.Models;

namespace Task1.Interfaces
{
    internal interface IMovieStarDataInput
    {
        public ICollection<MovieStar> GetMovieStarRawData();
    }
}
