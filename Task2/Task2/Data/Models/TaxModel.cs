namespace Task2.Data.Models
{
    public class TaxModel
    {
        public int Id { get; set; }

        public string TaxName { get; set; }

        public int TaxRate { get; set; }

        public double UpperTaxLimit { get; set; }
    }
}
