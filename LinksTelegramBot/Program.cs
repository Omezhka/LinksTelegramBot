namespace LinksTelegramBot
{
    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Entry entry = new();
            entry.Run();
            Console.ReadLine();
            entry.Stop();
        }
    }
}
