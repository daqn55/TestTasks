namespace Task2.Data.Models
{
    internal class TaxModel
    {
        public int Id { get; set; }

        public string TaxName { get; set; }

        public int TaxRate { get; set; }

        public double UpperTaxLimit { get; set; }
    }
}
