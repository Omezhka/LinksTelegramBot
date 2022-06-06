namespace LinksTelegramBot
{
    public interface ICommand
    {
        Task Execute(NewChatMessageEventArgs newChatMessageEventArgs, IChat chat);
        void ExecuteNext(NewChatMessageEventArgs newChatMessageEventArgs, IChat chat);

    }
}
