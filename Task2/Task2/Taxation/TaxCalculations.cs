using System.Text;
using Task2.Data.Interfaces;
using Task2.Interfaces;

namespace Task2.Taxation
{
    internal class TaxCalculations : ITaxCalculation
    {
        private readonly IDbContext _db;
        private readonly IReadAndPrint _readAndPrint;

        private const string NoTaxationMsg = "There is no taxation, net income is {0} IDR";
        private const string AmountOfTaxMsg = "{0} is {1} IDR";
        private const string TotalTaxMsg = "The total tax is {0} IDR and the net income is {1} IDR";

        public TaxCalculations(IDbContext db, IReadAndPrint readAndPrint)
        {
            _db = db;
            _readAndPrint = readAndPrint;
        }

        public void PrintTaxes(double taxAmount)
        {
            var taxesForPrinting = CalculateTaxes(taxAmount);

            _readAndPrint.WriteLine(taxesForPrinting.ToString() + Environment.NewLine);
        }

        private StringBuilder CalculateTaxes(double taxAmount)
        {
            var allTaxesFromDb = this._db.GetAllTaxes();
            var isTaxRecordWithZeroPercentTax = allTaxesFromDb.FirstOrDefault(x => x.TaxRate == 0);
            var withoutTaxAmount = isTaxRecordWithZeroPercentTax == null ? 0 : isTaxRecordWithZeroPercentTax.UpperTaxLimit;

            var sb = new StringBuilder();
            if (taxAmount <= withoutTaxAmount)
            {
                var msg = string.Format(NoTaxationMsg, taxAmount.ToString("f2"));

                sb.Append(msg);
            }
            else
            {
                double totalTax = 0;
                foreach (var taxes in allTaxesFromDb)
                {
                    if (taxes.TaxRate == 0)
                    {
                        continue;
                    }

                    var taxName = taxes.TaxName;
                    var taxRate = taxes.TaxRate;
                    var upperTaxLimit = taxes.UpperTaxLimit;

                    if (upperTaxLimit == 0)
                    {
                        var tax = (taxAmount - withoutTaxAmount) * (taxRate / 100d);
                        totalTax += tax;
                        sb.Append(string.Format(AmountOfTaxMsg, taxName, tax.ToString("f2")));
                        sb.Append(Environment.NewLine);
                    }
                    else
                    {
                        var amountForTaxing = taxAmount >= upperTaxLimit ? upperTaxLimit - withoutTaxAmount : taxAmount - withoutTaxAmount;

                        var tax = amountForTaxing * (taxRate / 100d);
                        totalTax += tax;
                        sb.Append(string.Format(AmountOfTaxMsg, taxName, tax.ToString("f2")));
                        sb.Append(Environment.NewLine);
                    }
                }

                sb.Append(string.Format(TotalTaxMsg, totalTax.ToString("f2"), (taxAmount - totalTax).ToString("f2")));
            }

            return sb;
        }
    }
}
