namespace Task2.Interfaces
{
    internal interface IReadAndPrint
    {
        public void Write(string data);
        public void WriteLine(string data);

        public string ReadLine();
    }
}
