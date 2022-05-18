namespace LinksTelegramBot
{
    public interface ICommand
    {
        string Execute();
        void ExecuteNext();

    }
}
