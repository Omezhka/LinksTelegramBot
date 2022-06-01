namespace LinksTelegramBot
{
    public interface ICommand
    {
        Task<string> Execute(NewChatMessageEventArgs newChatMessageEventArgs);
        string ExecuteNext(NewChatMessageEventArgs newChatMessageEventArgs);

    }
}
