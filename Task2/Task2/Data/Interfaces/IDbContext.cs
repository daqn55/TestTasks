using Task2.Data.Models;

namespace Task2.Data.Interfaces
{
    public interface IDbContext
    {
        public void AddTaxesToDb(TaxModel model);

        public ICollection<TaxModel> GetAllTaxes();
    }
}
