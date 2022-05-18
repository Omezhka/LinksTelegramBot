namespace LinksTelegramBot
{
    public class StoreLinkCommand : ICommand
    {
        public string Execute()
        {
            return "StoreLinkCommandExecute";
        }

        public void ExecuteNext()
        {
            throw new NotImplementedException();
        }
    }
}
