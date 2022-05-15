using Task2.Data;
using Task2.Data.Interfaces;
using Task2.Interfaces;
using Task2.IO;
using Task2.Taxation;

IDbContext db = new DbContext();
IReadAndPrint readAndPrint = new ReadAndPrint();
ITaxCalculation taxCalculation = new TaxCalculations(db, readAndPrint);

while (true)
{

    readAndPrint.WriteLine(@"Please enter the gross value to calculate the net salary or type 'HELP' for more options: ");
    var input = readAndPrint.ReadLine()?.ToLower();

    switch (input)
    {
        case "addnewtax":
            IAddNewTax newTax = new AddNewTax(db, readAndPrint);
            newTax.AddingTax();
            break;
        case "exit":
            return;
        case "help":
            readAndPrint.WriteLine("");
            readAndPrint.WriteLine("AddNewTax   -> Adds new tax to database.");
            readAndPrint.WriteLine("Exit        -> Quits the program.");
            readAndPrint.WriteLine("");
            break;
    }

    var isTaxAmountInCorrectFormat = double.TryParse(input, out double taxAmount);

    if (isTaxAmountInCorrectFormat)
    {
        taxCalculation.PrintTaxes(taxAmount);
    }
}