namespace LinksTelegramBot
{
    public class StoreLinkCommand : ICommand
    {
        public async Task<string> Execute(NewChatMessageEventArgs newChatMessageEventArgs)
        {
            return "StoreLinkCommandExecute";
        }

        public string ExecuteNext(NewChatMessageEventArgs newChatMessageEventArgs)
        {
            throw new NotImplementedException();
        }
    }
}
