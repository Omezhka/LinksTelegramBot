namespace LinksTelegramBot
{
    public interface ICommand
    {
        void Execute(NewChatMessageEventArgs newChatMessageEventArgs, IChat chat);
        void ExecuteNext(NewChatMessageEventArgs newChatMessageEventArgs, IChat chat);

    }
}
