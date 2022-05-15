namespace Task2.Interfaces
{
    public interface IReadAndPrint
    {
        public void Write(string data);
        public void WriteLine(string data);

        public string? ReadLine();
    }
}
