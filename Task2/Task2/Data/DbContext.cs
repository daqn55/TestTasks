using Dapper;
using System.Configuration;
using System.Data;
using System.Data.SQLite;
using Task2.Data.Interfaces;
using Task2.Data.Models;

namespace Task2.Data
{
    public class DbContext : IDbContext
    {
        private const string INSERTING_SQL_QUERY = "insert into Taxation (TaxName, TaxRate, UpperTaxLimit) values (@TaxName, @TaxRate, @UpperTaxLimit)";
        private const string SELECT_SQL_QUERY = "select * from Taxation";

        public void AddTaxesToDb (TaxModel model)
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();
                cnn.Execute(INSERTING_SQL_QUERY, model);
                cnn.Close();
            };
        }

        public ICollection<TaxModel> GetAllTaxes()
        {
            using (IDbConnection cnn = new SQLiteConnection(LoadConnectionString()))
            {
                cnn.Open();
                var allTaxes = cnn.Query<TaxModel>(SELECT_SQL_QUERY, new DynamicParameters());
                cnn.Close();

                return allTaxes.ToList();
            };
        }

        private string LoadConnectionString(string id = "Default")
        {
            return ConfigurationManager.ConnectionStrings[id].ConnectionString;
        }
    }
}
