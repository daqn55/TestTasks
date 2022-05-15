using Task2.Interfaces;

namespace Task2.IO
{
    public class ReadAndPrint : IReadAndPrint
    {
        public void WriteLine(string data)
        {
            Console.WriteLine(data);
        }

        public string? ReadLine()
        {
           return Console.ReadLine();
        }

        public void Write(string data)
        {
            Console.Write(data);
        }
    }
}
