namespace LinksTelegramBot
{
    public class GetLinksCommand : ICommand
    {
        public string Execute()
        {
            return "GetLinksCommandExecute";
        }

       
        public void ExecuteNext()
        {
            throw new NotImplementedException();
        }
    }
}
