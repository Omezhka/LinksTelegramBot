namespace LinksTelegramBot
{
    class Program
    {
       static void Main()
        {
            Entry entry = new();
            entry.Run();
            Console.ReadLine();
            entry.Stop();
        }
    }
}
