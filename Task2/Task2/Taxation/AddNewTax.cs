using Task2.Data.Interfaces;
using Task2.Data.Models;
using Task2.Interfaces;

namespace Task2.Taxation
{
    internal class AddNewTax : IAddNewTax
    {
        private readonly IDbContext _db;
        private readonly IReadAndPrint _readAndPrint;

        private const string ErrorMsg = "Something went wrong while adding new tax!";
        private const string SuccessMsg = "New Tax has added.";
        private const string ErrorMsgName = "Please enter a valid name!";
        private const string ErrorMsgTaxRate = "Please enter a valid tax rate!";
        private const string ErrorMsgTaxLimit = "Please higher value than no taxation amount!";

        private const string TaxNameMsg = "Tax name: ";
        private const string TaxRateMsg = "Tax rate: ";
        private const string UpperTaxLimitMsg = "Upper Tax Limit: ";


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

                return SuccessMsg;
            }
            catch (Exception)
            {
                throw new Exception(ErrorMsg);
            }
        }

        private TaxModel GetInputForModel()
        {
            _readAndPrint.Write(TaxNameMsg);
            var taxName = _readAndPrint.ReadLine().Trim();

            _readAndPrint.Write(TaxRateMsg);
            int taxRate;
            var isValidtaxRate = int.TryParse(_readAndPrint.ReadLine().Trim(), out taxRate);

            _readAndPrint.Write(UpperTaxLimitMsg);
            double upperTaxLimit;
            var isValidUpperTaxLimit = double.TryParse(_readAndPrint.ReadLine(), out upperTaxLimit);
            var amountWithoutTaxingModel = _db.GetAllTaxes().FirstOrDefault(x => x.TaxRate == 0);
            var amountWithoutTaxing = amountWithoutTaxingModel != null ? amountWithoutTaxingModel.UpperTaxLimit : 0;

            while (true)
            {
                if (string.IsNullOrEmpty(taxName))
                {
                    _readAndPrint.WriteLine(ErrorMsgName);
                    _readAndPrint.Write(TaxNameMsg);
                    taxName = _readAndPrint.ReadLine().Trim();
                }
                else if (!isValidtaxRate)
                {
                    _readAndPrint.WriteLine(ErrorMsgTaxRate);
                    _readAndPrint.Write(TaxRateMsg);
                    isValidtaxRate = int.TryParse(_readAndPrint.ReadLine().Trim(), out taxRate);
                }
                else if (isValidUpperTaxLimit)
                {
                    if (upperTaxLimit > 0 && upperTaxLimit < amountWithoutTaxing)
                    {
                        _readAndPrint.WriteLine(ErrorMsgTaxLimit);
                        _readAndPrint.Write(UpperTaxLimitMsg);
                        isValidUpperTaxLimit = double.TryParse(_readAndPrint.ReadLine(), out upperTaxLimit);
                        continue;
                    }

                    break;
                }
                else
                {
                    break;
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
