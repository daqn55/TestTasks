using Task2.Data.Interfaces;
using Task2.Data.Models;
using Task2.Interfaces;

namespace Task2.Taxation
{
    public class AddNewTax : IAddNewTax
    {
        private readonly IDbContext _db;
        private readonly IReadAndPrint _readAndPrint;

        private const string ERROR_MSG = "Something went wrong while adding new tax!";
        private const string SUCCESS_MSG = "New Tax has added.";
        private const string ERROR_MSG_NAME = "Please enter a valid name!";
        private const string ERROR_MSG_TAX_RATE = "Please enter a valid tax rate!";
        private const string ERROR_MSG_TAX_LIMIT = "Please enter higher value than no taxation amount!";

        private const string TAX_NAME_MSG = "Tax name: ";
        private const string TAX_RATE_MSG = "Tax rate: ";
        private const string UPPER_TAX_LIMIT_MSG = "Upper Tax Limit: ";


        public AddNewTax(IDbContext db, IReadAndPrint readAndPrint)
        {
            _db = db;
            _readAndPrint = readAndPrint;
        }

        public string AddingTax()
        {
            var model = GetInputForModel();

            try
            {
                this._db.AddTaxesToDb(model);

                return SUCCESS_MSG;
            }
            catch (Exception)
            {
                throw new Exception(ERROR_MSG);
            }
        }

        private TaxModel GetInputForModel()
        {
            var amountWithoutTaxingModel = _db.GetAllTaxes().FirstOrDefault(x => x.TaxRate == 0);
            var amountWithoutTaxing = amountWithoutTaxingModel != null ? amountWithoutTaxingModel.UpperTaxLimit : 0;

            var taxName = string.Empty;
            var IsTaxNameValid = string.IsNullOrEmpty(taxName);
            while (IsTaxNameValid)
            {
                _readAndPrint.Write(TAX_NAME_MSG);
                taxName = _readAndPrint.ReadLine()?.Trim();
                IsTaxNameValid = string.IsNullOrEmpty(taxName);

                if (IsTaxNameValid)
                {
                    _readAndPrint.WriteLine(ERROR_MSG_NAME);
                }
            }

            int taxRate = 0;
            var isValidtaxRate = false;

            while (!isValidtaxRate)
            {
                _readAndPrint.Write(TAX_RATE_MSG);
                isValidtaxRate = int.TryParse(_readAndPrint.ReadLine()?.Trim(), out taxRate);

                if (!isValidtaxRate)
                {
                    _readAndPrint.WriteLine(ERROR_MSG_TAX_RATE);
                }
            }

            double upperTaxLimit = 1;
            var isValidUpperTaxLimit = true;
            while (isValidUpperTaxLimit && (upperTaxLimit > 0 && upperTaxLimit < amountWithoutTaxing))
            {
                _readAndPrint.Write(UPPER_TAX_LIMIT_MSG);
                isValidUpperTaxLimit = double.TryParse(_readAndPrint.ReadLine()?.Trim(), out upperTaxLimit);

                if (upperTaxLimit > 0 && upperTaxLimit < amountWithoutTaxing)
                {
                    _readAndPrint.WriteLine(ERROR_MSG_TAX_LIMIT);
                }
            }

            var model = new TaxModel();

            model.TaxName = taxName;
            model.TaxRate = taxRate;
            model.UpperTaxLimit = upperTaxLimit;

            return model;
        }
    }
}
